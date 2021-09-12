using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using ServiceRequest.Controllers;
using Xunit;

namespace ServiceRequest.Tests.Unit
{
    public class ServiceRequestControllerTest
    {
        [Fact]
        public void Get_WithOutServiceRequests_ShouldReturn204StatusCode()
        {
            //Arrange
            var serviceRequestController = new ServiceRequestController();

            //Act
            var response = serviceRequestController.Get();

            //Asserts
            response.Should().NotBeNull();
            response.Should().BeOfType<NoContentResult>();
        }
    }
}
