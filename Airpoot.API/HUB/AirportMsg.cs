using Airpoot.API.BL;

namespace Airpoot.API.HUB
{
    public class AirportMsg
    {
        public Station Station { get; set; }
        public string Message { get; set; }
        public AirportMsg(Station currentStation, string v)
        {
            Station= currentStation;
            Message = v;
        }

    }
}