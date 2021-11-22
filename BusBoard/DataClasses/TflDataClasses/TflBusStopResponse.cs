using System.Collections.Generic;

namespace BusBoard
{
    public class TflBusStopResponse
    {
        public string Id;
        public string CommonName;
        public float Distance;
        public List<TflArrivalResponse> Arrivals;
    }
}