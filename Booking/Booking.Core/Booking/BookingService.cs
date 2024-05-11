using Booking.Infrastructure.Booking;
using Booking.Infrastructure.Database.Abstractions;
using Booking.Infrastructure.Database.Specifications;
using ErrorOr;

namespace Booking.Core.Booking;

public interface IBookingService
{
    Task<ErrorOr<List<BookingDto>>> GetAll();
    Task<ErrorOr<BookingDto>> Get(long entityId);
    Task<ErrorOr<BookingDto>> Create(CreateBookingDto toCreate);
    Task<ErrorOr<bool>> Delete(long entityId, long userId);
}

public class BookingService(IBookingRepository repository)
    : IBookingService
{
    public async Task<ErrorOr<List<BookingDto>>> GetAll()
    {
        var entities = await repository.GetAll();

        var dtos = entities.Select(entity => new BookingDto
        {
            DwellingId = entity.DwellingId,
            Id = entity.Id,
            TenantId = entity.TenantId,
            StartBookingDate = entity.StartBookingDate,
            EndBookingDate = entity.EndBookingDate,
        }).ToList();

        return dtos;
    }

    public async Task<ErrorOr<BookingDto>> Get(long entityId)
    {
        var entity = await repository.GetById(entityId);

        if (entity is null)
        {
            return BookingErrors.NotFound(entityId);
        }

        var dto = new BookingDto
        {
            DwellingId = entity.DwellingId,
            Id = entity.Id,
            TenantId = entity.TenantId,
            StartBookingDate = entity.StartBookingDate,
            EndBookingDate = entity.EndBookingDate,
        };

        return dto;
    }

    public async Task<ErrorOr<BookingDto>> Create(CreateBookingDto toCreate)
    {
        if (toCreate.StartBookingDate > DateOnly.FromDateTime(DateTime.UtcNow) ||
            toCreate.EndBookingDate <= DateOnly.FromDateTime(DateTime.UtcNow) ||
            toCreate.StartBookingDate > toCreate.EndBookingDate)
        {
            return BookingErrors.InvalidBookingDate;
        }

        #region Specs check
        var byDwellingIdSpec = BookingSpecs.ByDwellingId(toCreate.DwellingId);
        var byTenantIdSpec = BookingSpecs.ByTenantId(toCreate.TenantId);

        var leftDateSpec = BookingSpecs.ByDateInRange(toCreate.StartBookingDate);
        var rightDateSpec = BookingSpecs.ByDateInRange(toCreate.EndBookingDate);
        
        var dateSpecs = new AndSpecification<BookingEntity>(leftDateSpec, rightDateSpec);
        var idSpecs = new AndSpecification<BookingEntity>(byDwellingIdSpec, byTenantIdSpec);
        
        var specs = new AndSpecification<BookingEntity>(dateSpecs, idSpecs);
        #endregion
        
        var entities = await repository.GetBySpecification(specs);
        if (entities.Count >= 1)
        {
            return BookingErrors.AlreadyBooked;
        }

        var entityEntityToCreate = new BookingEntity()
        {
            DwellingId = toCreate.DwellingId,
            TenantId = toCreate.TenantId,
            StartBookingDate = toCreate.StartBookingDate,
            EndBookingDate = toCreate.EndBookingDate,
        };

        entityEntityToCreate = await repository.Create(entityEntityToCreate);

        var dto = new BookingDto
        {
            DwellingId = entityEntityToCreate.DwellingId,
            Id = entityEntityToCreate.Id,
            TenantId = entityEntityToCreate.TenantId,
            StartBookingDate = entityEntityToCreate.StartBookingDate,
            EndBookingDate = entityEntityToCreate.EndBookingDate,
        };

        return dto;
    }

    public async Task<ErrorOr<bool>> Delete(long entityId, long userId)
    {
        var leftSpecs = BookingSpecs.ById(entityId);
        var rightSpecs = BookingSpecs.ByTenantId(userId);
        var specs = new AndSpecification<BookingEntity>(leftSpecs, rightSpecs);

        var entities = await repository.GetBySpecification(specs);
        if (entities.Count is 0 or > 1)
        {
            return BookingErrors.AccessDenied;
        }

        await repository.Delete(entityId);

        return Task.CompletedTask.IsCompleted;
    }
}