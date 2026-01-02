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
    public async Task Post_ShouldReturnCreatedResponse_WhenCalled()
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

        // Act
        var sut = await client.PostAsJsonAsync("/persons", createPersonRequest);

        // Assert
        Assert.Equal(HttpStatusCode.Created, sut.StatusCode);
        Assert.NotNull(sut.Headers.Location);
        var getPersonResponse = await sut.Content.ReadFromJsonAsync<GetPersonResponse>();
        Assert.Equal("/persons/" + getPersonResponse!.Id, sut.Headers.Location!.AbsolutePath);
    }

    [Fact]
    public async Task Post_ShouldReturnBadRequestResponse_WhenCalled()
    {
        // Arrange
        var client = _factory.CreateClient();
        var createPersonRequest = new CreatePersonRequest
        {
            Name = "John",
            City = "City",
            Color = "foo",
            LastName = "Doe",
            ZipCode = "12345"
        };

        // Act
        var sut = await client.PostAsJsonAsync("/persons", createPersonRequest);

        // Assert
        Assert.Equal(HttpStatusCode.BadRequest, sut.StatusCode);
    }
}
