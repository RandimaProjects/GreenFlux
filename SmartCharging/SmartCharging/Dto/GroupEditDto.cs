using System.ComponentModel.DataAnnotations;

namespace SmartCharging.Dto
{
    public class GroupEditDto
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "The field {0} is required")]
        [StringLength(150, ErrorMessage = "The field {0} must be between {2} and {1} characters", MinimumLength = 2)]
        public string Name { get; set; }
        [Range(0, Double.MaxValue, ErrorMessage = "The field {0} must be greater than {1}.")]
        public decimal Capacity { get; set; }
    }
}
