
using Xunit;

using Microsoft.AspNetCore.Mvc.Testing;
using System.Net;
using System.Threading.Tasks;
using FluentAssertions;
namespace dedreira.samples.webapi.tests
{
    public class swagger_endpoint_should
    :IClassFixture<WebApplicationFactory<Startup>>
    {
        private readonly WebApplicationFactory<Startup> factory;

        public swagger_endpoint_should(WebApplicationFactory<Startup> factory)
        {
            this.factory = factory;
        }

        [Fact]
        public async Task return_ok_when_navigate_to_path()
        {
            // Arrange
            var client = factory.CreateClient();
            var route = "/";

            // Act
            var response = await client.GetAsync(route);

            // Asert
            response.EnsureSuccessStatusCode();
                      
        }

        [Fact]
        public async Task return_ok_when_navigate_to_json_file()
        {
            // Arrange
            var client = factory.CreateClient();
            var route = "/swagger/1.0/swagger.json";

            // Act
            var response = await client.GetAsync(route);

            // Asert
            response.EnsureSuccessStatusCode();
            response.StatusCode.Should().Equals(HttpStatusCode.OK);  
        }
    }
}
