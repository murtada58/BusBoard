using System.Collections.Generic;

namespace BusBoard
{
    public class TflBusStopsResponse
    {
        public List<TflBusStopResponse> StopPoints;
    }

    public class TflBusStopResponse
    {
        public string Id;
        public string CommonName;
        public float Distance;
    }
}