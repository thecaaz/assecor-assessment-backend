using Microsoft.AspNetCore.Mvc.Testing;
using Person.Presentation.RestAPI.Controllers;
using Person.Presentation.RestAPI.Models;
using System.Net;
using System.Net.Http.Json;
using Xunit;

namespace Person.Presentation.RestAPI.Tests.PeoplesControllerTests;

public partial class PeoplesControllerTests : IClassFixture<WebApplicationFactory<PeoplesController>>
{
    [Fact]
    public async Task GetByColor_ShouldReturnOkResponseAndPeopleFilteredByColor_WhenCalled()
    {
        // Arrange
        var client = _factory.CreateClient();
        var createBluePersonRequest = new CreatePersonRequest
        {
            Name = "John",
            City = "City",
            Color = "blau",
            LastName = "Doe",
            ZipCode = "12345"
        };
        var createdResponse = await client.PostAsJsonAsync("/persons", createBluePersonRequest);
        var createViolettPersonRequest = new CreatePersonRequest
        {
            Name = "John",
            City = "City",
            Color = "violett",
            LastName = "Doe",
            ZipCode = "12345"
        };
        var createViolettPersonResponse = await client.PostAsJsonAsync("/persons", createViolettPersonRequest);

        // Act
        var sut = await client.GetAsync("/persons/color/blau");
        var sutAll = await client.GetAsync("/persons");

        // Assert
        Assert.Equal(HttpStatusCode.OK, sut.StatusCode);
        var blueList = await sut.Content.ReadFromJsonAsync<IEnumerable<GetPersonResponse>>();
        var allList = await sutAll.Content.ReadFromJsonAsync<IEnumerable<GetPersonResponse>>();
        Assert.True(blueList!.Count() < allList!.Count());
        Assert.All(blueList!, person => Assert.Equal("blau", person.Color, ignoreCase: true));
    }

    [Fact]
    public async Task GetByColor_ShouldReturnBadRequestResponse_WhenColorDoesNotExist()
    {
        // Arrange
        var client = _factory.CreateClient();

        // Act
        var sut = await client.GetAsync("/persons/color/foo");

        // Assert
        Assert.Equal(HttpStatusCode.BadRequest, sut.StatusCode);
    }
}
