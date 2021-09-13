using System.Net;
using Xunit;

namespace ServiceRequest.Tests.Integration
{
    [Collection("")]
    public class ServiceRequestControllerTest : IClassFixture<ServerFixtue>
    {
        private readonly ServerFixtue _server;

        public ServiceRequestControllerTest(ServerFixtue server)
        {
            _server = server;
        }

        [Fact]
        public async void Get_WithOutServiceRequests_ShouldReturn204StatusCode()
        {
            //Arrange
            var client = _server.HttpClient;

            //Act
            var response = await client.GetAsync("https://localhost:44317/api/servicerequest");

            //Asserts
            Assert.True(response.StatusCode.Equals(HttpStatusCode.NoContent));
        }
    }
}
