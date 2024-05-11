using System.ComponentModel.DataAnnotations;

namespace Booking.RestApi.Booking.Models;

public class CreateBookingViewModel
{
    [Required]
    public required long DwellingId { get; set; }

    [Required]
    public required DateOnly StartBookingDate { get; set; }
    
    [Required]
    public required DateOnly EndBookingDate { get; set; }
}