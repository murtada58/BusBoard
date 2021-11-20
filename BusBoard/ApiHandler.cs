using RestSharp;

namespace BusBoard
{
    public class ApiHandler
    {
        private IRestClient _client;

        public ApiHandler(string baseUrl = "")
        {
            _client = new RestClient(baseUrl);
        }

        public void SetBaseUrl(string baseUrl)
        {
            _client = new RestClient(baseUrl);
        }

        public string MakeRequest(string requestString)
        {
            RestRequest request = new RestRequest(requestString, DataFormat.Json);
            request.OnBeforeDeserialization = resp => { resp.ContentType = "application/json"; };
            IRestResponse response = _client.Get(request);
            return response.Content;
        }
    }
}
