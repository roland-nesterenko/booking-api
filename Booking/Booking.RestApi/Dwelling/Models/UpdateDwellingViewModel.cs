using System.ComponentModel.DataAnnotations;

namespace Booking.RestApi.Dwelling.Models;

public class UpdateDwellingViewModel
{
    [Required]
    public required string Name { get; init; }
    
    [Required]
    public required string Description { get; init; }
}