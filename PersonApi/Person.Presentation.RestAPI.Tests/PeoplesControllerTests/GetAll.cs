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
    public async Task GetAll_ShouldReturnList_WhenCalled()
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

        // Act
        var sut = await client.GetAsync("/persons");

        // Assert
        Assert.Equal(HttpStatusCode.OK, sut.StatusCode);
        var list = await sut.Content.ReadFromJsonAsync<IEnumerable<object>>();
        Assert.NotNull(list);
        Assert.NotEmpty(list);
    }
}
