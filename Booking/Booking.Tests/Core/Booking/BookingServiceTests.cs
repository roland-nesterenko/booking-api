using Booking.Core.Booking;
using Booking.Infrastructure.Booking;
using Booking.Infrastructure.Database.Abstractions;
using Booking.Tests.Core.Booking.Helpers;
using FluentAssertions;
using NSubstitute;
using NSubstitute.ReturnsExtensions;

namespace Booking.Tests.Core.Booking;

public class BookingServiceTests
{
    private readonly IBookingRepository _repositoryMock;
    private readonly IBookingService _sut;

    public BookingServiceTests()
    {
        _repositoryMock = Substitute.For<IBookingRepository>();
        _sut = new BookingService(_repositoryMock);
    }
    
    [Fact]
    public async Task Get_ShouldReturnNotFoundError_WhenNoFilmFound()
    {
        // Arrange
        const long id = int.MaxValue - 1;
        _repositoryMock
            .GetById(id)
            .ReturnsNull();

        // Act
        var result = await _sut.Get(id);

        // Assert
        result.IsError.Should().BeTrue();
        result.FirstError.Should().BeEquivalentTo(BookingErrors.NotFound(id).FirstError);
    }

    [Theory]
    [ClassData(typeof(BookingDtoFakeProvider))]
    public async Task Get_ShouldReturnFilm_WhenFilmHasBeenFound(BookingDto dto)
    {
        // Arrange
        var filmEntity = new BookingEntity()
        {
            Id = dto.Id,
            TenantId = dto.TenantId,
            DwellingId = dto.DwellingId,
            StartBookingDate = dto.StartBookingDate,
            EndBookingDate = dto.EndBookingDate
        };
        _repositoryMock
            .GetById(dto.Id)!
            .Returns(Task.FromResult(filmEntity));

        // Act
        var result = await _sut.Get(dto.Id);

        // Assert
        result.Value.Should().NotBeNull();
        result.IsError.Should().BeFalse();
        result.Value.Id.Should().Be(dto.Id);
        result.Value.DwellingId.Should().Be(dto.DwellingId);
        result.Value.StartBookingDate.Should().Be(dto.StartBookingDate);
        result.Value.EndBookingDate.Should().Be(dto.EndBookingDate);
    }
}