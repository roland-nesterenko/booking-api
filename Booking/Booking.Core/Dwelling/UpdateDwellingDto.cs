namespace Booking.Core.Dwelling;

public record UpdateDwellingDto
{
    public required long Id { get; init; }
    
    public required string Name { get; init; }
    
    public required string Description { get; init; }

    public required long OwnerId { get; set; }
}