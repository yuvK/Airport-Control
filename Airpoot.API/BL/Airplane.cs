using Microsoft.AspNetCore.Routing.Internal;
using System.Text;
using System;
using static System.Collections.Specialized.BitVector32;
using Airpoot.API.HUB;
using Microsoft.AspNetCore.SignalR;

namespace Airpoot.API.BL
{
    public class Airplane
    {

        private string? _code;
        public int Id { get; set; }
        public Station? CurrentStation { get; set; }
        public string Code
        {
            get
            {
                if (string.IsNullOrEmpty(_code))
                {
                    _code = GenerateAirplaneName();
                }
                return _code;
            }
            set
            {
                _code = value;
            }
        }
        public bool IsDeparture { get; set; }

        private readonly IHubContext<AirportHub> _hub;
        public Airplane(IHubContext<AirportHub> hub)
        {
            this._hub = hub;
        }

        public async Task Run(Route route)
        {

            CurrentStation = route.FirstOrDefault();
            await Task.Run(async () =>
            {
                Station? nextStation;
                List<Station>? nextStations;

                do
                {
                    nextStations = route.GetNext(CurrentStation!);
                    if (nextStations.Count == 0)
                    {
                        CurrentStation?.Exit(/*CurrentStation*/);
                        CurrentStation = null;
                        return;
                    }
                    nextStation = await GetFirstAvailable(nextStations);

                    //Console.WriteLine($"{Code} moved to: {nextStation}");
                }
                while (nextStation != null && nextStations.Count > 0);
            });
        }
        private async Task<Station?> GetFirstAvailable(List<Station> nextStations)
        {
            CancellationTokenSource cts = new CancellationTokenSource();

            var enterStationsTasks = nextStations.Select(async x =>
            {
                return await x.Enter(this, cts);
            }).ToList();

            //var t = enterStationsTasks[0];
            //var c = t.IsCanceled;

            if (enterStationsTasks == null || enterStationsTasks.Count == 0)
                return null;

            Task<Station> task = await Task.WhenAny(enterStationsTasks);

            if (task != null)
                await MoveToNextStation(await task);

            return await task!;
        }
        private async Task MoveToNextStation(Station nextStation)
        {
            if (CurrentStation != null)
            {
                CurrentStation?.Exit();
            }

            CurrentStation = nextStation;

            Console.WriteLine($"airplane {Code} is moving to station {nextStation.Id}");
            await Task.Delay(CurrentStation.WaitTime);
        }

        static Random random = new Random();
        public static string GenerateAirplaneName()
        {
            StringBuilder stringBuilder = new StringBuilder();

            // Generate two uppercase letters
            stringBuilder.Append(RandomUppercaseLetter());
            stringBuilder.Append(RandomUppercaseLetter());

            // Generate three random numbers
            stringBuilder.Append(RandomNumber());
            stringBuilder.Append(RandomNumber());
            stringBuilder.Append(RandomNumber());

            return stringBuilder.ToString();
        }
        private static char RandomUppercaseLetter()
        {
            int num = random.Next(26); // 26 uppercase letters in the English alphabet
            char letter = (char)('A' + num);
            return letter;
        }
        private static int RandomNumber()
        {
            return random.Next(10); // Generates a random number between 0 and 9
        }
    }
}
