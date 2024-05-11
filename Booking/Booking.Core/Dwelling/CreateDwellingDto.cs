namespace Booking.Core.Dwelling;

public record CreateDwellingDto
{
    public required string Name { get; init; }

    public required string Description { get; init; }
    
    public required long OwnerId { get; init; }
}