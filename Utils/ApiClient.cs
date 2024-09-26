using Newtonsoft.Json.Linq;
using RestSharp;

namespace RestAPISampleAutoTests.Utils
{
    public static class ApiClient
    {
        public static RestResponse SendRequest(string url, RestRequest request)
        {
            return new RestClient(url).Execute(request);
        }

        public static JObject ParseResponseContent(RestResponse response)
        {
            return JObject.Parse(response.Content);
        }
    }
}
