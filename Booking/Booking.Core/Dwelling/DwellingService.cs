using Booking.Infrastructure.Database.Abstractions;
using Booking.Infrastructure.Database.Specifications;
using Booking.Infrastructure.Dwelling;
using ErrorOr;

namespace Booking.Core.Dwelling;

public interface IDwellingService
{
    Task<ErrorOr<List<DwellingDto>>> GetAll();
    Task<ErrorOr<DwellingDto>> Get(long entityId);
    Task<ErrorOr<DwellingDto>> Create(CreateDwellingDto toCreateDwelling);
    Task<ErrorOr<DwellingDto>> Update(UpdateDwellingDto dwellingToUpdate, long userId);
    Task<ErrorOr<bool>> Delete(long entityId, long userId);
}

public class DwellingService(IDwellingRepository repository)
    : IDwellingService
{
    public async Task<ErrorOr<List<DwellingDto>>> GetAll()
    {
        var entities = await repository.GetAll();

        var dtos = entities.Select(entity => new DwellingDto
        {
            Name = entity.Name,
            Id = entity.Id,
            Description = entity.Description,
            OwnerId = entity.OwnerId,
        }).ToList();

        return dtos;
    }

    public async Task<ErrorOr<DwellingDto>> Get(long entityId)
    {
        var entity = await repository.GetById(entityId);

        if (entity is null)
        {
            return DwellingErrors.NotFound(entityId);
        }

        var dto = new DwellingDto
        {
            Name = entity.Name,
            Id = entity.Id,
            Description = entity.Description,
            OwnerId = entity.OwnerId,
        };

        return dto;
    }

    public async Task Booked(long entityId, bool result)
    {
        var entity = await repository.GetById(entityId);
        
        if (entity is not null)
        {
            
        }
    }

    public async Task<ErrorOr<DwellingDto>> Create(CreateDwellingDto toCreateDwelling)
    {
        var entitySpec = DwellingSpecs.ByName(toCreateDwelling.Name);
        var isDuplicate = await repository.ExistsBySpecification(entitySpec);
        if (isDuplicate)
        {
            return DwellingErrors.Duplicate(toCreateDwelling.Name);
        }

        var entityEntityToCreate = new DwellingEntity()
        {
            Name = toCreateDwelling.Name,
            Description = toCreateDwelling.Description,
            OwnerId = toCreateDwelling.OwnerId,
        };

        entityEntityToCreate = await repository.Create(entityEntityToCreate);

        var dto = new DwellingDto
        {
            Id = entityEntityToCreate.Id,
            Name = entityEntityToCreate.Name,
            Description = entityEntityToCreate.Description,
            OwnerId = entityEntityToCreate.OwnerId,
        };

        return dto;
    }

    public async Task<ErrorOr<DwellingDto>> Update(UpdateDwellingDto dwellingToUpdate, long userId)
    {
        var entity = await repository.GetById(dwellingToUpdate.Id);

        if (entity is null)
        {
            return DwellingErrors.NotFound(dwellingToUpdate.Id);
        }
        
        var leftSpecs = DwellingSpecs.ByName(dwellingToUpdate.Name);
        var rightSpecs = DwellingSpecs.ByOwnerId(userId);
        var specs = new AndSpecification<DwellingEntity>(leftSpecs, rightSpecs);
        var entities = await repository.GetBySpecification(specs);
        if (entities.Count >= 1)
        {
            return DwellingErrors.Duplicate(dwellingToUpdate.Name);
        }

        entity.Name = dwellingToUpdate.Name;
        entity.Description = dwellingToUpdate.Description;

        await repository.Update(entity.Id, entity);

        var dto = new DwellingDto
        {
            Id = entity.Id,
            Name = entity.Name,
            Description = entity.Description,
            OwnerId = entity.OwnerId,
        };

        return dto;
    }

    public async Task<ErrorOr<bool>> Delete(long entityId, long userId)
    {
        var leftSpecs = DwellingSpecs.ById(entityId);
        var rightSpecs = DwellingSpecs.ByOwnerId(userId);
        var specs = new AndSpecification<DwellingEntity>(leftSpecs, rightSpecs);

        var entities = await repository.GetBySpecification(specs);
        if (entities.Count is 0 or > 1)
        {
            return DwellingErrors.AccessDenied;
        }
        
        await repository.Delete(entityId);
        
        return Task.CompletedTask.IsCompleted;
    }
}