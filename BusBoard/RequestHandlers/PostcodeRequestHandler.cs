using Newtonsoft.Json;

namespace BusBoard
{
    public class PostcodeRequestHandler
    {
        private readonly ApiHandler _apiHandler = new ApiHandler("https://api.postcodes.io/postcodes/");

        public PostcodeResponseResult GetPostcodeResponse(string postcode)
        {
            string rawResponse = _apiHandler.MakeRequest(postcode);
            PostcodeResponse postcodeResponse = JsonConvert.DeserializeObject<PostcodeResponse>(rawResponse);
            if (postcodeResponse == null) { return new PostcodeResponse().Result; }
            return postcodeResponse.Result;
        }
    }
}