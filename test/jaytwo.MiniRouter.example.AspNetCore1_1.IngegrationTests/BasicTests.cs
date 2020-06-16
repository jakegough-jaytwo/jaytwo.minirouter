using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace jaytwo.MiniRouter.example.AspNetCore1_1.IngegrationTests
{
    public class BasicTests : IClassFixture<TestServerFixture>
    {
        private readonly TestServerFixture _fixture;

        public BasicTests(TestServerFixture fixture)
        {
            _fixture = fixture;
        }

        [Fact]
        public async Task Get_EndpointsReturnSuccess()
        {
            // Arrange
            var client = _fixture.CreateClient();
            var expectedNamespace = typeof(example.AspNetCore1_1.Startup).GetTypeInfo().Assembly.GetName().Name;

            // Act
            using (var response = await client.GetAsync("/"))
            {
                // Assert
                response.EnsureSuccessStatusCode();

                var content = await response.Content.ReadAsStringAsync();
                Assert.Contains(expectedNamespace, content);
            }
        }

        [Fact]
        public async Task Get_HelloMidlewareReturnSuccess()
        {
            // Arrange
            var client = _fixture.CreateClient();
            var expectedString = "HelloMiniRouter";

            // Act
            using (var response = await client.GetAsync("/hello"))
            {
                // Assert
                response.EnsureSuccessStatusCode();

                var content = await response.Content.ReadAsStringAsync();
                Assert.Contains(expectedString, content);
            }
        }

        [Fact]
        public async Task Get_xml_HelloidlewareReturns_xml()
        {
            // Arrange
            var client = _fixture.CreateClient();
            var expectedString = "<para>hello world</para>";

            // Act
            using (var response = await client.GetAsync("/hello/xml"))
            {
                // Assert
                response.EnsureSuccessStatusCode();

                var content = await response.Content.ReadAsStringAsync();
                Assert.Contains(expectedString, content);
            }
        }

        [Fact]
        public async Task Get_accept_xml_HelloidlewareReturns_xml()
        {
            // Arrange
            var client = _fixture.CreateClient();
            var expectedString = "<para>hello world</para>";

            // Act
            using (var request = new HttpRequestMessage(HttpMethod.Get, "/hello"))
            {
                request.Headers.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/xml"));

                using (var response = await client.SendAsync(request))
                {
                    // Assert
                    response.EnsureSuccessStatusCode();

                    var content = await response.Content.ReadAsStringAsync();
                    Assert.Contains(expectedString, content);
                }
            }
        }

        [Fact]
        public async Task Get_json_HelloidlewareReturns_json()
        {
            // Arrange
            var client = _fixture.CreateClient();
            var expectedString = @"{""para"":""hello world""}";

            // Act
            using (var response = await client.GetAsync("/hello/json"))
            {
                // Assert
                response.EnsureSuccessStatusCode();

                var content = await response.Content.ReadAsStringAsync();
                Assert.Contains(expectedString, content);
            }
        }

        [Fact]
        public async Task Get_accept_json_HelloidlewareReturns_json()
        {
            // Arrange
            var client = _fixture.CreateClient();
            var expectedString = @"{""para"":""hello world""}";

            // Act
            using (var request = new HttpRequestMessage(HttpMethod.Get, "/hello"))
            {
                request.Headers.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));

                using (var response = await client.SendAsync(request))
                {
                    // Assert
                    response.EnsureSuccessStatusCode();

                    var content = await response.Content.ReadAsStringAsync();
                    Assert.Contains(expectedString, content);
                }
            }
        }
    }
}
