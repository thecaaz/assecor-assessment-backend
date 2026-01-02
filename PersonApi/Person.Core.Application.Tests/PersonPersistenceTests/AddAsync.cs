using NSubstitute;

namespace Person.Core.Application.Tests.PersonPersistenceTests;

public partial class PersonPersistenceTests
{
    [Fact]
    public async Task AddAsync_ShouldReturnNewPersonId_WhenCalled()
    {
        // Arrange
        var newPerson = new Domain.Person
        {
            Name = "John",
            LastName = "Doe",
            ZipCode = "12345",
            City = "SampleCity",
            Color = Domain.Color.blau
        };
        _repository.InsertAsync(Arg.Any<Domain.Person>())
            .Returns(Task.FromResult(1));
        // Act
        var personId = await _personPersistence.AddAsync(newPerson);

        // Assert
        Assert.Equal(1, personId);
    }

    [Fact]
    public async Task AddAsync_ShouldCallRepositoryInsert_WhenCalled()
    {
        // Arrange
        var newPerson = new Domain.Person
        {
            Name = "John",
            LastName = "Doe",
            ZipCode = "12345",
            City = "SampleCity",
            Color = Domain.Color.blau
        };
        _repository.InsertAsync(Arg.Any<Domain.Person>())
            .Returns(Task.FromResult(1));
        // Act
        var personId = await _personPersistence.AddAsync(newPerson);

        // Assert
        await _repository.Received().InsertAsync(Arg.Is<Domain.Person>(p =>
            p.Name == "John" &&
            p.LastName == "Doe" &&
            p.ZipCode == "12345" &&
            p.City == "SampleCity" &&
            p.Color == Domain.Color.blau));
    }
}
