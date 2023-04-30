using System.ComponentModel.DataAnnotations;

namespace WebApiPractice.Models.UpdateDtos
{
    public class PointOfInterestForUpdateDto
    {
        [Required(ErrorMessage = "You should provide a name Value")] // data validation. There are many types of attributes that can be used for data validation
        [MaxLength(50)]
        public string Name { get; set; } = string.Empty;

        [MaxLength(200)]
        public string? Description { get; set; }
    }
}
