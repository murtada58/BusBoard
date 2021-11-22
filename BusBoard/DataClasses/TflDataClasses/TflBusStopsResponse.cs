using System.Collections.Generic;

namespace BusBoard
{
    public class TflBusStopsResponse
    {
        public bool valid;
        public string Postcode;
        public List<TflBusStopResponse> StopPoints;
    }
}