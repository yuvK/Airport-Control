using Microsoft.AspNetCore.SignalR;

namespace Airpoot.API.HUB.Clients
{
    public class MyTimer
    {
        private readonly IHubContext<AirportHub> hub;

        public MyTimer(IHubContext<AirportHub> hub)
        {
            this.hub = hub;
        }
        int count = 1;
        public async Task StartTimer() { 
            await Task.Delay(1000);

            await hub.Clients.All.SendAsync("sendMsg", count);
            count++;
            if(count < 15)
            {
                await StartTimer();
            }
            else
            {
                count = 1;
            }
        }
    }
}
