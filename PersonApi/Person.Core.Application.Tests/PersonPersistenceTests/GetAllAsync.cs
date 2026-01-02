using NSubstitute;

namespace Person.Core.Application.Tests.PersonPersistenceTests;

public partial class PersonPersistenceTests
{
    [Fact]
    public async Task GetAllAsync_ShouldCallRepositoryGetAll_WhenCalled()
    {
        // Arrange
        _repository.GetAllAsync().Returns(new List<Domain.Person>());

        // Act
        var sut = await _personPersistence.GetAllAsync();

        // Assert
        await _repository.Received(1).GetAllAsync();
    }
}
