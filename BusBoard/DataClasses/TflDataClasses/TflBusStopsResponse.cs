using System.Collections.Generic;

namespace BusBoard
{
    public class TflBusStopsResponse
    {
        public bool valid;
        public int NumberOfBusStops;
        public int NumberOfBuses;
        public string Postcode;
        public List<TflBusStopResponse> StopPoints;
    }
}