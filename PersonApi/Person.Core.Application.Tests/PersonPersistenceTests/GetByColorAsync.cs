using NSubstitute;

namespace Person.Core.Application.Tests.PersonPersistenceTests;

public partial class PersonPersistenceTests
{
    [Fact]
    public async Task GetByColorAsync_ShouldReturnPeopleFilteredByColorFromRepository_WhenCalled()
    {
        // Arrange
        var people = new List<Domain.Person>
        {
            new Domain.Person { Name = "Alice", Color = Domain.Color.rot },
            new Domain.Person { Name = "Bob", Color = Domain.Color.blau },
            new Domain.Person { Name = "Charlie", Color = Domain.Color.rot }
        };
        _repository.GetAllAsync().Returns(people);

        // Act
        var sut = await _personPersistence.GetByColorAsync(Domain.Color.rot);

        // Assert
        Assert.Equal(2, (sut).Count());
        Assert.All(sut, person => Assert.Equal(Domain.Color.rot, person.Color));
    }
}
