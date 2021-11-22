using System;
using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json;


namespace BusBoard
{
    public class TflRequestHandler
    {
        private readonly ApiHandler _apiHandler = new ApiHandler("https://api.tfl.gov.uk");
        private readonly PostcodeRequestHandler _postcodeRequestHandler = new PostcodeRequestHandler();
        
        private List<TflArrivalResponse> GetNextBusArrivals(string busStop, int numberOfBuses)
        {
            List<TflArrivalResponse> arrivals = JsonConvert.DeserializeObject<List<TflArrivalResponse>>(_apiHandler.MakeRequest($"StopPoint/{busStop}/Arrivals"));
            if (arrivals == null) { return new List<TflArrivalResponse>(); }
            return arrivals.GetRange(0, Math.Min(numberOfBuses, arrivals.Count));
        }
        
        public void PrintNextBusArrivals(string busStop, int numberOfBuses, string busStopName = "")
        {
            string stop = busStopName == "" ? busStop : busStopName;
            Console.WriteLine($"Buses arriving at {stop}");
            List<TflArrivalResponse> arrivals = GetNextBusArrivals(busStop, numberOfBuses);
            foreach (TflArrivalResponse arrival in arrivals.OrderBy(arrival => arrival.ExpectedArrival))
            {
                Console.WriteLine($"    {arrival.LineId} to {arrival.DestinationName} arriving in {Math.Round((arrival.ExpectedArrival - DateTime.Now).TotalMinutes)} mins");
            }
        }

        public List<TflBusStopResponse> GetNearestBusStops(float longitude, float latitude, int numberOfBusStops)
        {
            string rawResponse = _apiHandler.MakeRequest($"StopPoint/?lat={latitude}&lon={longitude}&stopTypes=NaptanPublicBusCoachTram");
            TflBusStopsResponse busStopsResponse = JsonConvert.DeserializeObject<TflBusStopsResponse>(rawResponse);
            if (busStopsResponse == null) { return new List<TflBusStopResponse>(); } 
            return busStopsResponse.StopPoints.GetRange(0, Math.Min(numberOfBusStops, busStopsResponse.StopPoints.Count));
        }

        public List<TflBusStopResponse> GetNextBusArrivalsNearPostcode(string postcode, int numberOfBusStops, int numberOfBuses)
        {
            PostcodeResponseResult postcodeResponse = _postcodeRequestHandler.GetPostcodeResponse(postcode);
            List<TflBusStopResponse> busStops = GetNearestBusStops(postcodeResponse.Longitude, postcodeResponse.Latitude, numberOfBusStops);
            foreach (TflBusStopResponse busStop in busStops)
            {
                busStop.Arrivals = GetNextBusArrivals(busStop.Id, numberOfBuses);
            }

            return busStops;
        }
        
        public void PrintNextBusArrivalsNearPostcode(string postcode, int numberOfBusStops, int numberOfBuses)
        {
            List<TflBusStopResponse> busStops = GetNextBusArrivalsNearPostcode(postcode, numberOfBusStops, numberOfBuses);
            foreach (TflBusStopResponse busStop in busStops)
            {
                PrintNextBusArrivals(busStop.Id, numberOfBuses, busStop.CommonName);
            }
        }
    }
}