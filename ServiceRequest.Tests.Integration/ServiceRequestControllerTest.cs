using System;
using System.Net;
using System.Net.Http;
using System.Text;
using FluentAssertions;
using Newtonsoft.Json;
using ServiceRequest.DataModel.Enums;
using ServiceRequest.ViewModels;
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
        public async void Get01_WithOutServiceRequests_ShouldReturn204StatusCode()
        {
            //Arrange
            var client = _server.HttpClient;

            //Act
            var response = await client.GetAsync("https://localhost:44317/api/servicerequest");

            //Asserts
            Assert.True(response.StatusCode.Equals(HttpStatusCode.NoContent));
        }

        [Fact]
        public async void Get02_WithServiceRequests_ShouldReturn200StatusCode()
        {
            //Arrange
            var client = _server.HttpClient;

            var serviceRequest = new ServiceRequestModelRequest("A1", "Roof repair", CurrentStatus.Created, "John", DateTime.Now.AddDays(-2), "John",
                DateTime.Now.AddDays(-1));

            var stringContent = new StringContent(JsonConvert.SerializeObject(serviceRequest), Encoding.UTF8,
                "application/json");
            var postResponse = await client.PostAsync("https://localhost:44317/api/servicerequest", stringContent);
            var createdServiceRequest = postResponse.Content.ReadAsAsync<ServiceRequestModelResponse>().Result;

            //Act
            var response = await client.GetAsync($"https://localhost:44317/api/servicerequest/{createdServiceRequest.Id}");
            var receivedServiceRequest = response.Content.ReadAsAsync<ServiceRequestModelResponse>().Result;

            //Asserts
            Assert.True(response.StatusCode.Equals(HttpStatusCode.OK));
            receivedServiceRequest.Should().NotBeNull();
            receivedServiceRequest.BuildingCode.Should().Be(serviceRequest.BuildingCode);
            receivedServiceRequest.CreatedBy.Should().Be(serviceRequest.CreatedBy);
            receivedServiceRequest.CreatedDate.Should().Be(serviceRequest.CreatedDate);
            receivedServiceRequest.Description.Should().Be(serviceRequest.Description);
            receivedServiceRequest.LastModifiedBy.Should().Be(serviceRequest.LastModifiedBy);
            receivedServiceRequest.LastModifiedDate.Should().Be(serviceRequest.LastModifiedDate);
        }

        [Fact]
        public async void Delete03_WithServiceRequests_ShouldReturn200StatusCode()
        {
            //Arrange
            var client = _server.HttpClient;

            var serviceRequest = new ServiceRequestModelRequest("A1", "Roof repair", CurrentStatus.Created, "John", DateTime.Now.AddDays(-2), "John",
                DateTime.Now.AddDays(-1));

            var stringContent = new StringContent(JsonConvert.SerializeObject(serviceRequest), Encoding.UTF8,
                "application/json");
            var postResponse = await client.PostAsync("https://localhost:44317/api/servicerequest", stringContent);
            var createdServiceRequest = postResponse.Content.ReadAsAsync<ServiceRequestModelResponse>().Result;

            //Act
            var response = await client.DeleteAsync($"https://localhost:44317/api/servicerequest/{createdServiceRequest.Id}");

            //Asserts
            Assert.True(response.StatusCode.Equals(HttpStatusCode.OK));
        }
    }
}
