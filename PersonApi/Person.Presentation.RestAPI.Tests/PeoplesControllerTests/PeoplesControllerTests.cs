using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.VisualStudio.TestPlatform.TestHost;
using Person.Presentation.RestAPI.Controllers;
using System.Net;
using System.Net.Http.Json;
using Xunit;

namespace Person.Presentation.RestAPI.Tests.PeoplesControllerTests;

public partial class PeoplesControllerTests : IClassFixture<WebApplicationFactory<PeoplesController>>
{
    private readonly WebApplicationFactory<PeoplesController> _factory;

    public PeoplesControllerTests(WebApplicationFactory<PeoplesController> factory)
    {
        _factory = factory;
    }
}
