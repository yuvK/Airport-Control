using Airpoot.API.HUB.Clients;
using Microsoft.AspNetCore.SignalR;

namespace Airpoot.API.HUB
{
    public class AirportHub : Hub
    {
        private readonly MyTimer timer;

        public AirportHub(MyTimer timer)
        {
            this.timer = timer;
        }
        public void StartTimer(string text)
        {
            Console.WriteLine(text);

            timer.StartTimer().Wait();
        }
        //public async Task SendMsg(AirportMsg msg)
        //{
        //    await Clients.All.ReciveMessage(msg);
        //}

        //internal async Task SendMsg(string v)
        //{
        //    await Clients.All.SendAsync(v);
        //}
    }
}
