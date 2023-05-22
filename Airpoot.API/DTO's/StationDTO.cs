namespace Airpoot.API.DTO_s
{
    public class StationDTO
    {
        public int stationId { get; set; }
        public int airplaneId { get; set; }
        public string airplaneCode { get; set; }
        public bool airplaneIsDeparture { get; set; }

        public StationDTO(int stationId, int airplaneId, string airplaneCode, bool airplaneIsDeparture)
        {
            this.stationId = stationId;
            this.airplaneId = airplaneId;
            this.airplaneCode = airplaneCode;
            this.airplaneIsDeparture = airplaneIsDeparture;
        }
    }
}
