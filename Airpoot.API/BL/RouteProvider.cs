using Airpoot.API.DTO_s;
using Airpoot.API.HUB;
using Microsoft.AspNetCore.SignalR;
using System.Text.Json;

namespace Airpoot.API.BL
{
    public class RouteProvider
    {
        Route Arrival = new Route();
        Route Departure = new Route();
        public Route? GetArrivalRoute
        {
            get
            {
                if (ArrivalCount > 3)
                    return null;
                Interlocked.Increment(ref ArrivalCount);
                return Arrival;
            }
        }
        public Route? GetDepartureRoute
        {
            get
            {
                if (DepartureCount > 3)
                    return null;
                Interlocked.Increment(ref DepartureCount);
                return Departure;
            }
        }

        public int ArrivalCount;
        public int DepartureCount;
        internal void ReleseArrival()
        {
            Interlocked.Decrement(ref ArrivalCount);
        }
        internal void ReleseDeparture()
        {
            Interlocked.Decrement(ref DepartureCount);
        }
        private List<Station> AllStations { get; } = new();

        public RouteProvider(IHubContext<AirportHub> hub)
        {
            var path = Environment.CurrentDirectory + "/StationsInit.json";
            var data = File.ReadAllText(path);
            if (data == null)
            {
                FirstInit();
                return;
            }
            else
            {
                var fromJson = JsonSerializer.Deserialize<JsonModels>(data);
                AllStations.AddRange(fromJson!.Stations!.Select(x => new Station(hub) { Id = x.Id, WaitTime = TimeSpan.FromSeconds(x.WaitTime) }));
                foreach (var e in fromJson!.Edges!)
                {
                    Station from = AllStations.First(x => x.Id == e.From);
                    Station to = AllStations.First(x => x.Id == e.To);

                    if (e.IsDeparture == true)
                    {
                        Departure.Add(from);
                        Departure.Add(to);
                        Departure.ConnectToStation(from, to);
                        //Departure.FirstStation = station_11;
                        Departure.isDeparture = true;
                    }
                    else if (e.IsDeparture == false)
                    {
                        Arrival.Add(from);
                        Arrival.Add(to);
                        Arrival.ConnectToStation(from, to);
                        //Arrival.FirstStation = station_0;
                        Arrival.isDeparture = false;
                    }
                }
            }

            void FirstInit()
            {

                Station station_0 = new Station(hub) { Id = 0, WaitTime = TimeSpan.FromSeconds(1) };
                Station station_1 = new Station(hub) { Id = 1, WaitTime = TimeSpan.FromSeconds(1) };
                Station station_2 = new Station(hub) { Id = 2, WaitTime = TimeSpan.FromSeconds(1) };
                Station station_3 = new Station(hub) { Id = 3, WaitTime = TimeSpan.FromSeconds(2) };
                Station station_4 = new Station(hub) { Id = 4, WaitTime = TimeSpan.FromSeconds(3) };
                Station station_5 = new Station(hub) { Id = 5, WaitTime = TimeSpan.FromSeconds(2) };
                Station station_6 = new Station(hub) { Id = 6, WaitTime = TimeSpan.FromSeconds(2) };
                Station station_7 = new Station(hub) { Id = 7, WaitTime = TimeSpan.FromSeconds(2) };
                Station station_8 = new Station(hub) { Id = 8, WaitTime = TimeSpan.FromSeconds(2) };
                Station station_9 = new Station(hub) { Id = 9, WaitTime = TimeSpan.FromSeconds(2) };
                Station station_10 = new Station(hub) { Id = 10, WaitTime = TimeSpan.FromSeconds(0) };
                Station station_11 = new Station(hub) { Id = 11, WaitTime = TimeSpan.FromSeconds(0) };

                //AllStations.AddRange(new[] { station_1, station_1, station_2, station_3, station_4, station_5, station_6, station_7, station_8, station_9 });

                //Arrival.FirstStation = station_0;
                //Arrival.isDeparture = false;

                Arrival.Add(station_0);
                Arrival.Add(station_1);
                Arrival.Add(station_2);
                Arrival.Add(station_3);
                Arrival.Add(station_4);
                Arrival.Add(station_5);
                Arrival.Add(station_6);
                Arrival.Add(station_7);
                Arrival.Add(station_10);

                Arrival.ConnectToStation(station_0, station_1);
                Arrival.ConnectToStation(station_1, station_2);
                Arrival.ConnectToStation(station_2, station_3);
                Arrival.ConnectToStation(station_3, station_4);
                Arrival.ConnectToStation(station_4, station_5);
                Arrival.ConnectToStation(station_5, station_6);
                Arrival.ConnectToStation(station_5, station_7);
                Arrival.ConnectToStation(station_6, station_10);
                Arrival.ConnectToStation(station_7, station_10);


                Departure.FirstStation = station_11;
                Departure.isDeparture = true;

                Departure.Add(station_11);
                //Departute.Add(station_6);
                Departure.Add(station_7);
                Departure.Add(station_8);
                Departure.Add(station_4);
                Departure.Add(station_9);
                Departure.Add(station_10);


                //Departute.ConnectToStation(station_10, station_6);
                Departure.ConnectToStation(station_11, station_7);
                //Departute.ConnectToStation(station_6, station_8);
                Departure.ConnectToStation(station_7, station_8);
                Departure.ConnectToStation(station_8, station_4);
                Departure.ConnectToStation(station_4, station_9);
                Departure.ConnectToStation(station_9, station_10);
            }
        }


    }
}
