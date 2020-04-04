using System;
using Xunit;
using dedreira.samples.webapi;
using Microsoft.AspNetCore.Mvc.Testing;
using System.Net;
using System.Threading.Tasks;
namespace dedreira.samples.webapi.tests
{
    public class swagger_should
    :IClassFixture<WebApplicationFactory<Startup>>
    {
        private readonly WebApplicationFactory<Startup> factory;

        public swagger_should(WebApplicationFactory<Startup> factory)
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
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        }

        [Fact]
        public async Task return_ok_when_navigate_to_json_file()
        {
            // Arrange
            var client = factory.CreateClient();
            var route = "/swagger/v1/swagger.json";

            // Act
            var response = await client.GetAsync(route);

            // Asert
            response.EnsureSuccessStatusCode();
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        }
    }
}
