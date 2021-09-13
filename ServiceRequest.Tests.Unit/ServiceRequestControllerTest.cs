using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using ServiceRequest.Controllers;
using ServiceRequest.DataAccess;
using ServiceRequest.DataAccess.Interfaces;
using ServiceRequest.Services;
using ServiceRequest.Services.Interfaces;
using System.Collections.Generic;
using Xunit;

namespace ServiceRequest.Tests.Unit
{
    public class ServiceRequestControllerTest
    {
        private readonly ServiceRequestController _serviceRequestController;
        private readonly IServiceRequestRepository _serviceRequestRepository;

        public ServiceRequestControllerTest()
        {
            _serviceRequestRepository = new ServiceRequestRepository();
            IServiceRequestService serviceRequestService = new ServiceRequestService(_serviceRequestRepository);
            _serviceRequestController = new ServiceRequestController(serviceRequestService);
        }

        [Fact]
        public void Get_WithOutServiceRequests_ShouldReturn204StatusCode()
        {
            //Arrange

            //Act
            var response = _serviceRequestController.Get();

            //Asserts
            response.Should().NotBeNull();
            response.Should().BeOfType<NoContentResult>();
        }

        [Fact]
        public void Get_WithServiceRequests_ShouldReturn200StatusCode()
        {
            //Arrange
            const string serviceRequest1 = "Service Request 1";
            const string serviceRequest2 = "Service Request 2";
            _serviceRequestRepository.Add(serviceRequest1);
            _serviceRequestRepository.Add(serviceRequest2);

            //Act
            var response = _serviceRequestController.Get();

            //Asserts
            response.Should().NotBeNull();
            response.Should().BeOfType<OkObjectResult>();
            var serviceRequests = (IList<string>)((OkObjectResult)response).Value;
            serviceRequests.Should().HaveCount(2);
            serviceRequests.Should().Contain(serviceRequest1);
            serviceRequests.Should().Contain(serviceRequest2);
        }
    }
}
