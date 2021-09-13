using System;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using ServiceRequest.Controllers;
using ServiceRequest.DataAccess;
using ServiceRequest.DataAccess.Interfaces;
using ServiceRequest.Services;
using ServiceRequest.Services.Interfaces;
using ServiceRequest.DataModel.Enums;
using ServiceRequest.DataModel;
using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using ServiceRequest.AutoMapper;
using ServiceRequest.ViewModels;
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
            IMapper mapper = new Mapper(new MapperConfiguration(cfg => {
                cfg.AddProfile<AutoMapperProfile>();
            }));
            _serviceRequestController = new ServiceRequestController(serviceRequestService, mapper);
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
            var serviceRequest1 = new ServiceRequestModel(Guid.NewGuid(), "A1", "Roof repair", CurrentStatus.Created, "John", DateTime.Now.AddDays(-2), "John",
                DateTime.Now.AddDays(-1));
            var serviceRequest2 = new ServiceRequestModel(Guid.NewGuid(), "B2", "Power outage", CurrentStatus.Created, "Marie", DateTime.Now.AddDays(-2), "Marie",
                DateTime.Now.AddDays(-1));
            _serviceRequestRepository.Add(serviceRequest1);
            _serviceRequestRepository.Add(serviceRequest2);

            //Act
            var response = _serviceRequestController.Get();

            //Asserts
            response.Should().NotBeNull();
            response.Should().BeOfType<OkObjectResult>();
            var receivedServiceRequests = (IList<ServiceRequestModelResponse>)((OkObjectResult)response).Value;
            receivedServiceRequests.Should().HaveCount(2);
            receivedServiceRequests.First(sr => sr.Id == serviceRequest1.Id).Should().BeEquivalentTo(serviceRequest1);
            receivedServiceRequests.First(sr => sr.Id == serviceRequest2.Id).Should().BeEquivalentTo(serviceRequest2);
        }

        [Fact]
        public void GetById_WithOutServiceRequests_ShouldReturn404StatusCode()
        {
            //Arrange
            var invalidServiceRequestId = Guid.NewGuid();

            //Act
            var response = _serviceRequestController.Get(invalidServiceRequestId);

            //Asserts
            response.Should().NotBeNull();
            response.Should().BeOfType<NotFoundResult>();
        }

        [Fact]
        public void GetById_WithServiceRequest_ShouldReturn200StatusCodeAndSingleServiceRequest()
        {
            //Arrange
            var serviceRequest1 = new ServiceRequestModel(Guid.NewGuid(), "A1", "Roof repair", CurrentStatus.Created, "John", DateTime.Now.AddDays(-2), "John",
                DateTime.Now.AddDays(-1));
            var serviceRequest2 = new ServiceRequestModel(Guid.NewGuid(), "B2", "Power outage", CurrentStatus.Created, "Marie", DateTime.Now.AddDays(-2), "Marie",
                DateTime.Now.AddDays(-1));
            _serviceRequestRepository.Add(serviceRequest1);
            _serviceRequestRepository.Add(serviceRequest2);

            //Act
            var response = _serviceRequestController.Get(serviceRequest1.Id);

            //Asserts
            response.Should().NotBeNull();
            response.Should().BeOfType<OkObjectResult>();
            var receivedServiceRequest = (ServiceRequestModelResponse)((OkObjectResult)response).Value;
            receivedServiceRequest.Should().NotBeNull();
            receivedServiceRequest.Should().BeEquivalentTo(serviceRequest1);
        }

        [Fact]
        public void Post_NewServiceRequest_ShouldReturn201StatusCodeAndServiceRequestWithId()
        {
            //Arrange
            var newServiceRequest = new ServiceRequestModelRequest("A1", "Roof repair", CurrentStatus.Created, "John", DateTime.Now.AddDays(-2), "John",
                DateTime.Now.AddDays(-1));

            //Act
            var response = _serviceRequestController.Post(newServiceRequest);

            //Asserts
            response.Should().NotBeNull();
            response.Should().BeOfType<CreatedAtRouteResult>();
            var receivedServiceRequest = (ServiceRequestModelResponse)((CreatedAtRouteResult)response).Value;
            receivedServiceRequest.Should().NotBeNull();
            receivedServiceRequest.BuildingCode.Should().Be(newServiceRequest.BuildingCode);
            receivedServiceRequest.CreatedBy.Should().Be(newServiceRequest.CreatedBy);
            receivedServiceRequest.CreatedDate.Should().Be(newServiceRequest.CreatedDate);
            receivedServiceRequest.CurrentStatus.Should().Be(newServiceRequest.CurrentStatus);
            receivedServiceRequest.Description.Should().Be(newServiceRequest.Description);
            receivedServiceRequest.LastModifiedBy.Should().Be(newServiceRequest.LastModifiedBy);
            receivedServiceRequest.LastModifiedDate.Should().Be(newServiceRequest.LastModifiedDate);
        }

        [Fact]
        public void Put_WithExistingServiceRequest_ShouldReturn200StatusCode()
        {
            //Arrange
            var serviceRequestId = Guid.NewGuid();
            var serviceRequest1 = new ServiceRequestModel(serviceRequestId, "A1", "Roof repair", CurrentStatus.Created, "John", DateTime.Now.AddDays(-2), "John",
                DateTime.Now.AddDays(-1));
            _serviceRequestRepository.Add(serviceRequest1);
            var modifiedServiceRequest = new ServiceRequestModelRequest("B2", "Roof repair", CurrentStatus.Complete, "Marie", DateTime.Now.AddDays(-20), "Marie",
                DateTime.Now.AddDays(-1));

            //Act
            var response = _serviceRequestController.Put(serviceRequestId, modifiedServiceRequest);

            //Asserts
            response.Should().NotBeNull();
            response.Should().BeOfType<OkObjectResult>();
            var receivedServiceRequest = (ServiceRequestModelResponse)((OkObjectResult)response).Value;
            receivedServiceRequest.Should().NotBeNull();
            receivedServiceRequest.Id.Should().Be(serviceRequestId);
            receivedServiceRequest.BuildingCode.Should().Be(modifiedServiceRequest.BuildingCode);
            receivedServiceRequest.CreatedBy.Should().Be(modifiedServiceRequest.CreatedBy);
            receivedServiceRequest.CreatedDate.Should().Be(modifiedServiceRequest.CreatedDate);
            receivedServiceRequest.CurrentStatus.Should().Be(modifiedServiceRequest.CurrentStatus);
            receivedServiceRequest.Description.Should().Be(modifiedServiceRequest.Description);
            receivedServiceRequest.LastModifiedBy.Should().Be(modifiedServiceRequest.LastModifiedBy);
            receivedServiceRequest.LastModifiedDate.Should().Be(modifiedServiceRequest.LastModifiedDate);
        }

        [Fact]
        public void Put_WithOutServiceRequests_ShouldReturn404StatusCode()
        {
            //Arrange
            var serviceRequestId = Guid.NewGuid();
            var modifiedServiceRequest = new ServiceRequestModelRequest("B2", "Roof repair", CurrentStatus.Complete, "Marie", DateTime.Now.AddDays(-20), "Marie",
                DateTime.Now.AddDays(-1));

            //Act
            var response = _serviceRequestController.Put(serviceRequestId, modifiedServiceRequest);

            //Asserts
            response.Should().NotBeNull();
            response.Should().BeOfType<NotFoundResult>();
        }
    }
}
