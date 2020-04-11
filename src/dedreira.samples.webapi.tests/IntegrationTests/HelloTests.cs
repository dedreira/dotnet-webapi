using System;
using Xunit;
using dedreira.samples.webapi;
using Microsoft.AspNetCore.Mvc.Testing;
using System.Net;
using System.Threading.Tasks;
using FluentAssertions;
namespace dedreira.samples.webapi.tests
{
    public class hello_endpoint_should
    :IClassFixture<WebApplicationFactory<Startup>>
    {
        private readonly WebApplicationFactory<Startup> factory;

        public hello_endpoint_should(WebApplicationFactory<Startup> factory)
        {
            this.factory = factory;
        }

        [Theory]
        [InlineData("Daniel")]
        [InlineData("Diana")]
        [InlineData("John")]
        [InlineData("Sarah")]
        public async Task greet_user_who_invokes_endpoint(string user)
        {
            string expected = $"Hello {user}";
            string route = $"api/v1/hello?name={user}";
            var client = this.factory.CreateClient();

            var result = await client.GetAsync(route);

            result.EnsureSuccessStatusCode();
            result.Content.ReadAsStringAsync().Should().Equals(expected);
        }
    }
}