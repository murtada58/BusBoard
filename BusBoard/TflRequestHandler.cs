using System;
using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json;


namespace BusBoard
{
    public class TflRequestHandler
    {
        private readonly ApiHandler _apiHandler = new ApiHandler("https://api.tfl.gov.uk");
        
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
    }
}