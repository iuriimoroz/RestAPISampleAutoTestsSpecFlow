using Newtonsoft.Json.Linq;
using RestSharp;
using NUnit.Framework;
using TechTalk.SpecFlow;
using RestAPISampleAutoTests.Utils;
using RestAPISampleAutoTests.Consts;
using System;

[Binding, Scope(Tag = "SingleUser")]
public class SingleUserEndpointSteps
{
    private RestResponse _apiResponse;
    private JObject _userData;
    private string _baseUrl;

    [Given(@"I have the base URL of the API")]
    public void GivenIHaveTheBaseURLOfTheAPI()
    {
        _baseUrl = "https://reqres.in";
    }

    [When(@"I send a GET request to ""(.*)"" with user ID (.*)")]
    public void WhenISendAGETRequestToWithUserID(string endpointKey, int userId)
    {
        string endpointPath = UserEndpoints.SingleUserEndpoint.Replace("{userId}", userId.ToString());

        var request = new RestRequest(endpointPath, Method.Get);
        _apiResponse = ApiClient.SendRequest(_baseUrl, request);

        if ((int)_apiResponse.StatusCode != 404)
        {
            _userData = ApiClient.ParseResponseContent(_apiResponse);
        }
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

    [Then(@"the user's email in the response should be ""(.*)""")]
    public void ThenTheUsersEmailInTheResponseShouldBe(string expectedEmail)
    {
        Assert.That((string)_userData["data"]["email"], Is.EqualTo(expectedEmail),
            $"Expected email to be {expectedEmail}, but got {(string)_userData["data"]["email"]}.");
    }

    [Then(@"the user's first name in the response should be ""(.*)""")]
    public void ThenTheUsersFirstNameInTheResponseShouldBe(string expectedFirstName)
    {
        Assert.That((string)_userData["data"]["first_name"], Is.EqualTo(expectedFirstName),
            $"Expected first name to be {expectedFirstName}, but got {(string)_userData["data"]["first_name"]}.");
    }

    [Then(@"the user's last name in the response should be ""(.*)""")]
    public void ThenTheUsersLastNameInTheResponseShouldBe(string expectedLastName)
    {
        Assert.That((string)_userData["data"]["last_name"], Is.EqualTo(expectedLastName),
            $"Expected last name to be {expectedLastName}, but got {(string)_userData["data"]["last_name"]}.");
    }

    [Then(@"the user's avatar in the response should be ""(.*)""")]
    public void ThenTheUsersAvatarInTheResponseShouldBe(string expectedAvatarUrl)
    {
        Assert.That((string)_userData["data"]["avatar"], Is.EqualTo(expectedAvatarUrl),
            $"Expected avatar URL to be {expectedAvatarUrl}, but got {(string)_userData["data"]["avatar"]}.");
    }

    [Then(@"the user ID in the response should be (.*)")]
    public void ThenTheUserIDInTheResponseShouldBe(int expectedId)
    {
        Assert.That((int)_userData["data"]["id"], Is.EqualTo(expectedId),
            $"Expected user ID to be {expectedId}, but got {(int)_userData["data"]["id"]}.");
    }

    [Then(@"the response content should indicate ""(.*)""")]
    public void ThenTheResponseContentShouldIndicate(string expectedMessage)
    {
        Assert.That(_apiResponse.Content, Does.Contain(expectedMessage),
            $"Expected the response content to contain '{expectedMessage}', but got {_apiResponse.Content}.");
    }
}