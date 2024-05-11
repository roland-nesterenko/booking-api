using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;

namespace Booking.RestApi.Dwelling.Models;

public class CreateDwellingViewModel
{
    [Required]
    [MaxLength(128)]
    public required string Name { get; init; }
    
    [Required]
    [MaxLength(256)]
    public required string Description { get; init; }
}