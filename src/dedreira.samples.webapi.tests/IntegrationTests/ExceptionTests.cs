
using Xunit;

using Microsoft.AspNetCore.Mvc.Testing;
using System.Net;
using System.Threading.Tasks;
using FluentAssertions;
namespace dedreira.samples.webapi.tests
{
    public class exception_endpoint_should
    :IClassFixture<WebApplicationFactory<Startup>>
    {
        private readonly WebApplicationFactory<Startup> factory;

        public exception_endpoint_should(WebApplicationFactory<Startup> factory)
        {
            this.factory = factory;
        }
        [Fact]
        public async Task return_internal_server_error_when_invoked()
        {
            var client = factory.CreateClient();
            var route = "/api/v2/exception";

            var result = await client.GetAsync(route);

            result.StatusCode.Should().Equals(HttpStatusCode.InternalServerError);
        }
    }
}