namespace SmartCharging.Dto
{
    public class ChargeStationViewDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int GroupId { get; set; }
        List<ConnectorDto> Connectors { get; set; }
    }
}
