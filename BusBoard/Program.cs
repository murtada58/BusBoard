namespace BusBoard
{
    class Program
    {
        static void Main(string[] args)
        {
            TflRequestHandler tflRequestHandler = new TflRequestHandler();
            //string stopCode = Global.GetUserInput("Please enter a stop code: ");
            //tflRequestHandler.PrintNextBusArrivals(stopCode, 5);
            
            string postcode = Global.GetUserInput("Please enter a postcode: ");
            tflRequestHandler.PrintNextBusArrivalsNearPostcode(postcode, 2, 5);
        }
    }
}