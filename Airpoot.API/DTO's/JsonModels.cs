namespace Airpoot.API.DTO_s
{
    public class JsonModels
    {
        public JsonStationModel[]? Stations { get; set; }
        public JsonEdgeModel[]? Edges { get; set; }
    }
    public class JsonStationModel
    {
        public int Id { get; set; }
        public int WaitTime { get; set; }
    }
    public class JsonEdgeModel
    {
        public int From { get; set; }
        public int To { get; set; }
        public bool IsDeparture { get; set; }
    }
}
