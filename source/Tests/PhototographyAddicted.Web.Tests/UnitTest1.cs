using Microsoft.AspNetCore.Mvc.Testing;
using PhotographyAddicted.Web;
using System;
using System.Net;
using System.Threading.Tasks;
using Xunit;

namespace PhototographyAddicted.Web.Tests
{
    public class UnitTest1 : IClassFixture<WebApplicationFactory<Startup>>
    {
        private readonly WebApplicationFactory<Startup> server;

        public UnitTest1(WebApplicationFactory<Startup> server)
        {
            this.server = server;
        }

        //Themes/CreateTheme

        [Fact]
        public async Task TestingCreatePageAuthorizattion()
        {
            var client = this.server.CreateClient(
                new WebApplicationFactoryClientOptions(){AllowAutoRedirect = false });
            var response = await client.GetAsync("/Themes/CreateTheme");

            Assert.Equal(HttpStatusCode.Redirect, response.StatusCode);
            //Assert.True(response.StatusCode == HttpStatusCode.Redirect);
        }

        [Fact]
        public async Task IndexPageShouldReturnStatusCode200()
        {
            var client = this.server.CreateClient();
            var response = await client.GetAsync("/");
            response.EnsureSuccessStatusCode();
            var responseContent = await response.Content.ReadAsStringAsync();
            Assert.Contains("<title>",responseContent);
        }
    }
}
