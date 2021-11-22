using System;
using System.Collections.Generic;
using System.Diagnostics;
using BusBoard;
using Frontend.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Formatters;
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
        
        [HttpGet("buses/arrivals")]
        public IActionResult Arrivals([FromQuery] PostcodeForm form)
        {
            var tflRequestHandler = new TflRequestHandler();
            try
            {
                var busStopsResponse = tflRequestHandler.GetNextBusArrivalsNearPostcode(form.Postcode, 2, 5);
                busStopsResponse.valid = true;
                return View(busStopsResponse);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                var busStopsResponse = new TflBusStopsResponse();
                busStopsResponse.valid = false;
                busStopsResponse.Postcode = form.Postcode;
                return View(busStopsResponse);
            }
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel {RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier});
        }
    }
}