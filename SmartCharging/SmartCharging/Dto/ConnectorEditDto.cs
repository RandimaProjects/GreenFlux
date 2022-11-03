using System.ComponentModel.DataAnnotations;

namespace SmartCharging.Dto
{
    public class ConnectorEditDto
    {
        public int Id { get; set; }
        [Range(0, Double.MaxValue, ErrorMessage = "The field {0} must be greater than {1}.")]
        public decimal MaxCurrent { get; set; }
    }
}
