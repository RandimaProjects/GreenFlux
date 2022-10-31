namespace SmartCharging.Dto
{
    public class ConnectorDto
    {
        public int Id { get; set; }
        public int ChargeStationId { get; set; }
        public decimal MaxCurrent { get; set; }
    }
}
