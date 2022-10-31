namespace SmartCharging.Dto
{
    public class GroupViewDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public decimal Capacity { get; set; }
        List<ChargeStationViewDto> ChargeStations { get; set; }
    }
}
