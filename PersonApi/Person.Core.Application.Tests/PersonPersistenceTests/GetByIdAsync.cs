using NSubstitute;

namespace Person.Core.Application.Tests.PersonPersistenceTests;

public partial class PersonPersistenceTests
{
    [Fact]
    public async Task GetByIdAsync_ShouldReturnPersonFromRepositoryMatchingId_WhenCalled()
    {
        // Arrange
        var person = new Domain.Person
        {
            Id = 1234,
            Name = "Max",
            LastName = "Mustermann",
            ZipCode = "12345",
            City = "Musterstadt",
            Color = Domain.Color.rot
        };
        _repository.GetByIdOrDefaultAsync(person.Id).Returns(person);

        // Act
        var sut = await _personPersistence.GetByIdAsync(person.Id);

        // Assert
        Assert.Equal(person, sut);
    }

    [Fact]
    public async Task GetByIdAsync_ShouldThrowKeyNotFoundException_WhenPersonWithIdNotFound()
    {
        // Arrange
        var person = new Domain.Person
        {
            Id = 1234,
            Name = "Max",
            LastName = "Mustermann",
            ZipCode = "12345",
            City = "Musterstadt",
            Color = Domain.Color.rot
        };

        // Act / Assert
        await Assert.ThrowsAsync<KeyNotFoundException>(async () => await _personPersistence.GetByIdAsync(0));
    }
}
