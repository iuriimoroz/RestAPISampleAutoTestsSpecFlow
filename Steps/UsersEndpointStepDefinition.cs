using Newtonsoft.Json.Linq;
using RestSharp;
using NUnit.Framework;
using TechTalk.SpecFlow;
using RestAPISampleAutoTests.Utils;
using RestAPISampleAutoTests.Consts;
using System;
using Newtonsoft.Json.Schema;

[Binding, Scope(Tag = "UsersList")]
public class UsersEndpointSteps
{
    private RestResponse _apiResponse;
    private JObject _responseData;
    private string _baseUrl;

    [Given(@"I have the base URL of the API")]
    public void GivenIHaveTheBaseURLOfTheAPI()
    {
        _baseUrl = "https://reqres.in";
    }

    [When(@"I send a GET request to ""(.*)""")]
    public void WhenISendAGETRequestTo(string endpointKey)
    {
        string endpointPath = endpointKey switch
        {
            "ListUsersEndpoint" => UserEndpoints.ListUsersEndpoint,
            _ => throw new ArgumentException($"Invalid endpoint key: {endpointKey}")
        };

        var request = new RestRequest(endpointPath, Method.Get);
        _apiResponse = ApiClient.SendRequest(_baseUrl, request);
        _responseData = ApiClient.ParseResponseContent(_apiResponse);
    }

    [When(@"I send a GET request to ""(.*)"" for page (.*)")]
    public void WhenISendAGETRequestToForPage(string endpointKey, int pageNumber)
    {
        string endpointPath = endpointKey switch
        {
            "ListUsersByPageEndpoint" => UserEndpoints.ListUsersByPageEndpoint.Replace("{page}", pageNumber.ToString()),
            _ => throw new ArgumentException($"Invalid endpoint key: {endpointKey}")
        };

        var request = new RestRequest(endpointPath, Method.Get);
        _apiResponse = ApiClient.SendRequest(_baseUrl, request);
        _responseData = ApiClient.ParseResponseContent(_apiResponse);
    }

    [Then(@"the response status code should be (.*)")]
    public void ThenTheResponseStatusCodeShouldBe(int expectedStatusCode)
    {
        Assert.That((int)_apiResponse.StatusCode, Is.EqualTo(expectedStatusCode),
            $"Expected status code {expectedStatusCode}, but got {(int)_apiResponse.StatusCode}.");
    }

    [Then(@"the response content should not be empty")]
    public void ThenTheResponseContentShouldNotBeEmpty()
    {
        Assert.That(_apiResponse.Content, Is.Not.Null.And.Not.Empty,
            "The response content is empty, but it was expected to have content.");
    }

    [Then(@"the current page in the response should be (.*)")]
    public void ThenTheCurrentPageInTheResponseShouldBe(int expectedPage)
    {
        Assert.That((int)_responseData["page"], Is.EqualTo(expectedPage),
            $"Expected page to be {expectedPage}, but got {(int)_responseData["page"]}.");
    }

    [Then(@"the response should match the expected JSON schema for users")]
    public void ThenTheResponseShouldMatchTheExpectedJSONSchemaForUsers()
    {
        Assert.That(_responseData.IsValid(JsonSchemas.UsersJsonSchema()), "Response does not match the expected schema.");
    }
}