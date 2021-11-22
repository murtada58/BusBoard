using System;
using System.Collections.Generic;
using System.Diagnostics;
using BusBoard;
using Frontend.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace Frontend.Controllers
{
    public class BusesController : Controller
    {
        private readonly ILogger<BusesController> _logger;

        public BusesController(ILogger<BusesController> logger)
        {
            _logger = logger;
        }
        
        [HttpGet("buses/arrivals/{postcode}")]
        public IActionResult Arrivals([FromRoute] string postcode)
        { 
            var tflRequestHandler = new TflRequestHandler();
            var busStops = new List<TflBusStopResponse>();
            try
            {
                busStops = tflRequestHandler.GetNextBusArrivalsNearPostcode(postcode, 2, 5);
            }
            catch (Exception e)
            {
                //Console.WriteLine(e);
                return View(new List<TflBusStopResponse>());
            }
            return View(busStops);
        }
        
        [HttpGet("buses/arrivals")]
        public IActionResult GetArrivals()
        {
            return View();
        }
        
        [HttpPost("buses/arrivals")]
        public void GetArrivals(string postcode)
        {
            Response.Redirect("arrivals/" + postcode);
        }
        
        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel {RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier});
        }
    }
}