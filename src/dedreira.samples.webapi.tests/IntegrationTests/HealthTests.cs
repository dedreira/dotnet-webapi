
using Xunit;

using Microsoft.AspNetCore.Mvc.Testing;
using System.Net;
using System.Threading.Tasks;
using FluentAssertions;
namespace dedreira.samples.webapi.tests
{
    public class health_endpoint_should
    :IClassFixture<WebApplicationFactory<Startup>>
    {
        private readonly WebApplicationFactory<Startup> factory;

        public health_endpoint_should(WebApplicationFactory<Startup> factory)
        {
            this.factory = factory;
        }

        [Fact]
        public async Task return_ok_when_invoked()
        {
            // Arrange
            var client = factory.CreateClient();
            var route = "/api/health";

            // Act
            var response = await client.GetAsync(route);

            // Asert
            response.EnsureSuccessStatusCode();
            response.StatusCode.Should().Equals(HttpStatusCode.OK);
        }
    }
}
