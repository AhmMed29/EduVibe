using System.ComponentModel.DataAnnotations;

namespace EduVibe.DTOs.Shared;

public class AddressDto
{
    [Required]
    [StringLength(30, ErrorMessage = "City must be less than 30 characters")]
    public string City { get; set; } = null!;

    [Required]
    [StringLength(30, ErrorMessage = "Country must be less than 30 characters")]
    public string Country { get; set; } = null!;
}
