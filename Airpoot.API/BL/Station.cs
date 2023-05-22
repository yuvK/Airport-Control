using Airpoot.API.DTO_s;
using Airpoot.API.HUB;
using Microsoft.AspNetCore.SignalR;
using System.Text.Json;

namespace Airpoot.API.BL
{
    public class Station
    {
        private readonly IHubContext<AirportHub> _hub;
        public int Id { get; set; }
        public TimeSpan WaitTime { get; set; }
        public Airplane? Airplane { get; set; }
        public bool IsAvailable { get => Airplane == null; }

        SemaphoreSlim _sem = new SemaphoreSlim(1);

        public Station(IHubContext<AirportHub> hub)
        {
            this._hub = hub;
        }

        public async Task<Station> Enter(Airplane airplane, CancellationTokenSource cts)
        {
            try
            {
                await _sem.WaitAsync(cts.Token);
                cts.Cancel();
                this.Airplane = airplane;
                if (Id != 10 && Id != 11)
                {
                    var info = new StationDTO(
                          Id,
                          Airplane.Id,
                          airplane.Code,
                          airplane.IsDeparture
                    );
                    var data = JsonSerializer.Serialize(info);
                    await _hub.Clients.All.SendAsync("StationsUpdate", data/*, CurrentStation*/);
                    //await _hub.Clients.All.SendAsync("StationsUpdate", airplane.CurrentStation);
                }
                return this;
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                return this;
            }
        }
        internal async Task Exit(/*Station CurrentStation*/)
        {
            this.Airplane = null;

            if (Id != 10 && Id != 11)
            {
                var info = new StationDTO(
                  Id,
                  0,
                  "",
                  false
                   );
                var data = JsonSerializer.Serialize(info);
                await _hub.Clients.All.SendAsync("StationsUpdate", data/*, CurrentStation*/);
            }
            _sem.Release();
        }
    }
}