namespace Booking.Core.Dwelling;

public record DwellingDto
{
    public required long Id { get; set; }
    
    public required string Name { get; init; }

    public required string Description { get; init; }

    public required long OwnerId { get; set; }
}