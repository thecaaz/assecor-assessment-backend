using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.VisualStudio.TestPlatform.TestHost;
using Person.Presentation.RestAPI.Models;
using System.Net;
using System.Net.Http.Json;
using Xunit;

namespace Person.Presentation.RestAPI.Tests.PeoplesControllerTests;

public partial class PeoplesControllerTests : IClassFixture<WebApplicationFactory<Program>>
{
    [Fact]
    public async Task Get_ShouldReturn404_WhenIdDoesNotExist()
    {
        // Arrange
        var client = _factory.CreateClient();
        var allPersonsResponse = await client.GetAsync("/persons");
        var persons = await allPersonsResponse.Content.ReadFromJsonAsync<IEnumerable<GetPersonResponse>>();
        var maxPersonId = persons!.Any() ? persons!.Max(p => p.Id) : 0;

        // Act
        var sut = await client.GetAsync($"/persons/{maxPersonId + 1}");

        // Assert
        Assert.Equal(HttpStatusCode.NotFound, sut.StatusCode);
    }

    [Fact]
    public async Task Get_ShouldReturnPerson_WhenIdExists()
    {
        // Arrange
        var client = _factory.CreateClient();
        var createPersonRequest = new CreatePersonRequest
        {
            Name = "John",
            City = "City",
            Color = "blau",
            LastName = "Doe",
            ZipCode = "12345"
        };
        var createdResponse = await client.PostAsJsonAsync("/persons", createPersonRequest);
        var personResponse = await createdResponse.Content.ReadFromJsonAsync<GetPersonResponse>();

        // Act
        var sut = await client.GetAsync($"/persons/{personResponse!.Id}");

        // Assert
        Assert.Equal(HttpStatusCode.OK, sut.StatusCode);
        var getPersonResponse = await sut.Content.ReadFromJsonAsync<GetPersonResponse>();
        Assert.NotNull(getPersonResponse);
        Assert.Equal(personResponse.Id, getPersonResponse.Id);
    }
}
