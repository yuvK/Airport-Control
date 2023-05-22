using Airpoot.API.BL;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Text.Json;
using System.Text.Json.Serialization;
using Route = Airpoot.API.BL.Route;

namespace Airpoot.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AirportController : ControllerBase
    {
        private readonly AirportLogic _logic;
        private readonly BL.RouteProvider _routeProvider;

        public AirportController(AirportLogic logic, BL.RouteProvider route)
        {
            _logic = logic;
            _routeProvider = route;
        }


        [HttpGet(Name = "Get")]
        public string Get()
        {
            //_logic.AddAirplane("LY388", false);
            //_logic.AddAirplane("TS476", true);
            //_logic.AddAirplane("BA760", false);
            //_simulator.StartTimer();

            return $"controller is working";
        }
        //[HttpGet("AddAirplane/{airplane}")]
        //public IActionResult AddAirplane(int airplane)
        //{
        //    Console.WriteLine($"Airplane: {airplane}");
        //    var a = _logic.AddAirplane();
        //    if (a == null)
        //    {
        //        return BadRequest("Airplane Rejected");
        //    }
        //    else
        //    {
        //        return Ok($"Airplane: {airplane} accepted!");
        //    }
        //}

        [HttpGet("AddArrival/{Airplane}")]
        public async Task<IActionResult> AddArrival(int airplane)
        {
            Console.WriteLine($"new airplane: {airplane}");
            var route = _routeProvider.GetArrivalRoute;
            if (route == null)
            {
                Console.WriteLine($"airplame {airplane} rejected");
                return BadRequest($"flight: {airplane} rejected");
            }

            _logic.AddAirplane(airplane, route); //Fire and forget
            _routeProvider.ReleseArrival();
            return Ok($"{airplane} is waiting to start Arrival!");
        }


        [HttpGet("AddDeparture/{Airplane}")]
        public async Task<IActionResult> AddDeparture(int airplane)
        {
            Console.WriteLine($"new airplane: {airplane}");
            var route = _routeProvider.GetDepartureRoute;
            if (route == null)
            {
                Console.WriteLine($"airplame {airplane} rejected");
                return BadRequest($"flight: {airplane} rejected");
            }

            _logic.AddAirplane(airplane, route); //Fire and forget
            _routeProvider.ReleseDeparture();
            return Ok($"{airplane} is waiting to start Departure!");
        }


        //[HttpGet("Departure/{airplane}")]
        //public async Task<IActionResult> AddDeparture(int airplane)
        //{
        //    Console.WriteLine($"airplane no.{airplane} is asking to enter airport! ");
        //    var route = _route.DepartureRoute;
        //    if (route == null)
        //        return BadRequest($"airplane {airplane} rejected!");

        //    _logic.AddAirplane(airplane, true); //fire and forget
        //    //_route.ReleseDeparture();
        //    return Ok($"airplane {airplane} approved to enter airport!");
        //}
        //[HttpGet("Arrival/{airplane}")]
        //public async Task<IActionResult> AddArrival(int airplane)
        //{
        //    Console.WriteLine($"airplane no.{airplane} is asking to enter airport! ");
        //    var route = _route.ArrivalRoute;
        //    if (route == null)
        //        return BadRequest($"airplane {airplane} rejected!");

        //    _logic.AddAirplane(airplane, false); //fire and forget
        //    //_route.ReleseArrival();
        //    return Ok($"airplane {airplane} approved to enter airport!");
        //}
        //[HttpGet("status")]
        //public string Status()
        //{
        //    var stat = _logic.GetStatus();
        //    return stat; //serlize
        //}

        [HttpGet("StationsStatus")]
        public string StationStatus()
        {
            var stations = _logic.AllStations;
            var data = stations.Select(x => new { stationId = x.Id, aairplaneId = x.Airplane?.Id });
            return JsonSerializer.Serialize(data);
        }
    }
}
