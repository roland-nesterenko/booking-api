using Booking.Core.Dwelling;
using Booking.Infrastructure.Database.Abstractions;
using Booking.Infrastructure.Dwelling;
using Booking.Tests.Core.Dwelling.Helpers;
using FluentAssertions;
using NSubstitute;
using NSubstitute.ReturnsExtensions;

namespace Booking.Tests.Core.Dwelling;

public class DwellingServiceTests
{
    private readonly IDwellingRepository _repositoryMock;
    private readonly IDwellingService _sut;

    public DwellingServiceTests()
    {
        _repositoryMock = Substitute.For<IDwellingRepository>();
        _sut = new DwellingService(_repositoryMock);
    }

    [Fact]
    public async Task GetAsync_ShouldReturnNotFoundError_WhenNoDwellingFound()
    {
        // Arrange
        const long id = long.MaxValue - 1;
        _repositoryMock
            .GetById(id)
            .ReturnsNull();

        // Act
        var result = await _sut.Get(id);

        // Assert
        result.IsError.Should().BeTrue();
        result.FirstError.Should().BeEquivalentTo(DwellingErrors.NotFound(id).FirstError);
    }

    [Theory]
    [ClassData(typeof(DwellingDtoFakeProvider))]
    public async Task GetAsync_ShouldReturnDwelling_WhenDwellingHasBeenFound(DwellingDto dto)
    {
        // Arrange
        var categoryEntity = new DwellingEntity()
        {
            Id = dto.Id,
            Name = dto.Name,
            Description = dto.Description,
            OwnerId = dto.OwnerId
        };
        _repositoryMock
            .GetById(dto.Id)!
            .Returns(Task.FromResult(categoryEntity));

        // Act
        var result = await _sut.Get(dto.Id);

        // Assert
        result.Value.Should().NotBeNull();
        result.IsError.Should().BeFalse();
        result.Value.Id.Should().Be(dto.Id);
        result.Value.Name.Should().Be(dto.Name);
        result.Value.Description.Should().Be(dto.Description);
        result.Value.OwnerId.Should().Be(dto.OwnerId);
    }

    [Fact]
    public async Task UpdateAsync_ShouldReturnNotFoundError_WhenDwellingHasNotBeenFound()
    {
        // Arrange
        var toUpdate = new UpdateDwellingDto()
        {
            Id = 1,
            Name = string.Empty,
            Description = string.Empty,
        };
        _repositoryMock
            .GetById(toUpdate.Id)
            .ReturnsNull();

        // Act
        var result = await _sut.Update(toUpdate, 1);

        // Assert
        result.Value.Should().BeNull();
        result.IsError.Should().BeTrue();
        result.FirstError.Should().BeEquivalentTo(DwellingErrors.NotFound(toUpdate.Id).FirstError);
    }

    // [Theory]
    // [ClassData(typeof(UpdateDwellingDtoFakeProvider))]
    // public async Task UpdateAsync_ShouldUpdateEntity_WhenDwellingHasBeenFound(UpdateDwellingDto dto)
    // {
    //     // Arrange
    //     var entity = new DwellingEntity()
    //     {
    //         Id = dto.Id,
    //         Name = dto.Name,
    //         OwnerId = dto.OwnerId,
    //         Description = dto.Description
    //     };
    //     _repositoryMock
    //         .GetById(dto.Id)!
    //         .Returns(Task.FromResult(entity));
    //
    //     // Act
    //     var result = await _sut.Update(dto);
    //
    //     // Assert
    //     result.Value.Should().NotBeNull();
    //     result.IsError.Should().BeFalse();
    //     result.Value.Id.Should().Be(entity.Id);
    //     result.Value.Name.Should().Be(entity.Name);
    //     result.Value.OwnerId.Should().Be(entity.OwnerId);
    //     result.Value.Description.Should().Be(entity.Description);
    // }
}