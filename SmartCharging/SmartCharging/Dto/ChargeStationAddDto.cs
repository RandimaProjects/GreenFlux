using System.ComponentModel.DataAnnotations;

namespace SmartCharging.Dto
{
    public class ChargeStationAddDto
    {
      
        [Required(ErrorMessage = "The field {0} is required")]
        [StringLength(150, ErrorMessage = "The field {0} must be between {2} and {1} characters", MinimumLength = 2)]
        public string Name { get; set; }
        [Required(ErrorMessage = "The field {0} is required")]
        public int GroupId { get; set; }
        public decimal ConnectorMaxCurrent { get; set; }
    }
}
