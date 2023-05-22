namespace Airpoot.API.DAL
{
    public class AirplaneHistory
    {
        public Guid Id { get; set; }
        public string? Code { get; set; }
        public bool IsDeparted { get; set; }
        public DateTime Start { get; set; }
        public DateTime Finish { get; set; }
    }
}
