

using Airpoot.API.DAL;
using Airpoot.API.DTO_s;
using Airpoot.API.HUB;
using Airpoot.API.HUB.Clients;
using Microsoft.AspNetCore.SignalR;
using System.Collections.Concurrent;
using System.Text.Json;

namespace Airpoot.API.BL
{
    public class AirportLogic
    {
        //private readonly Route? _route;
        private readonly IHubContext<AirportHub> _hub;
        private IRepository<AirplaneHistory> _repository;

        public ConcurrentBag<Airplane> airplanesOnRun = new ConcurrentBag<Airplane>();
        public AirportLogic(RouteProvider route, IHubContext<AirportHub> hub, IRepository<AirplaneHistory> repo)
        {
            //this._route= route;
            this._hub = hub;
            this._repository = repo;
        }

        public List<Station> AllStations { get; set; }

        //int count = 0;

        public async Task AddAirplane(int id, Route route)
        {
            bool isDeparture = false;
            if (route.isDeparture == true)
                isDeparture = true;


            var a = new Airplane(_hub) { Id = id, IsDeparture = isDeparture };

            airplanesOnRun.Add(a);
            //add to history - route started

            var activeAirplanes = airplanesOnRun.Select(x => new AirplaneDTO(x.Id, x.Code, x.IsDeparture));
            var data = JsonSerializer.Serialize(activeAirplanes);
            await _hub.Clients.All.SendAsync("AirplainsUpdate", data);

            Console.WriteLine($"{a.Code} is added to airport!");

            var ah = new AirplaneHistory()
            {
                Code = a.Code,
                IsDeparted = a.IsDeparture,
                Start = DateTime.Now
            };

            await a.Run(route);

            airplanesOnRun.TryTake(out a);
            //add to history - route ended

            Console.WriteLine($"{a.Code} Finished route!");
            ah.Finish = DateTime.Now;
            _repository.Add(ah);
            //await _hub.Clients.All.SendAsync("AirplainsUpdate", JsonSerializer.Serialize(airplanesOnRun));
        }

        internal object GetStatus()
        {
            throw new NotImplementedException();
        }

        //public async Task<Airplane> AddAirplane(/*string code, bool isDeparture*/)
        //{
        //    var a = new Airplane() { };  /*{ Code = code, IsDeparture = isDeparture };*/
        //    airplanes.Add(a);
        //    //if (airplanes.Where(a => a.IsDeparture).Count() > 2 || airplanes.Where(a => !a.IsDeparture).Count() > 3)
        //    //{
        //    //    return null!;
        //    //}
        //    //if (count > 2)
        //    //    return null!;
        //    if (a.IsDeparture)
        //    {
        //        airplanes.Add(a);
        //        Console.WriteLine($"flight {a.Code} ask to start Departure process");
        //        //Interlocked.Increment(ref count);
        //        await a.Run(route!.DepartureRoute);
        //    }
        //    else
        //    {
        //        airplanes.Add(a);
        //        Console.WriteLine($"flight {a.Code} ask to start Arrival process");
        //        //Interlocked.Increment(ref count);
        //        await a.Run(route!.ArrivalRoute);
        //    }
        //    airplanes.Remove(a);
        //    Console.WriteLine($"{a.Code} Finished route!");
        //    //Interlocked.Decrement(ref count);
        //    return a;
        //}
    }
}
