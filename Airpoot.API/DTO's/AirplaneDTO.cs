namespace Airpoot.API.DTO_s
{
    public class AirplaneDTO
    {
        public int airplaneId { get; set; }
        public string airplaneCode { get; set; }
        public bool airplaneIsDeparture { get; set; }

        public AirplaneDTO(int airplaneId, string airplaneCode, bool airplaneIsDeparture)
        {
            this.airplaneId = airplaneId;
            this.airplaneCode = airplaneCode;
            this.airplaneIsDeparture = airplaneIsDeparture;
        }
    }
}
