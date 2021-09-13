using System;
using System.Collections.Generic;
using ServiceRequest.DataAccess.Interfaces;
using ServiceRequest.DataModel;
using ServiceRequest.Services.Interfaces;

namespace ServiceRequest.Services
{
    public class ServiceRequestService : IServiceRequestService
    {
        private readonly IServiceRequestRepository _serviceRequestRepository;

        public ServiceRequestService(IServiceRequestRepository serviceRequestRepository)
        {
            _serviceRequestRepository = serviceRequestRepository;
        }

        public IList<ServiceRequestModel> GetAll()
        {
            return _serviceRequestRepository.GetAll();
        }

        public ServiceRequestModel GetById(Guid serviceRequestId)
        {
            return _serviceRequestRepository.GetById(serviceRequestId);
        }

        public ServiceRequestModel Add(ServiceRequestModel serviceRequest)
        {
            return _serviceRequestRepository.GetById(_serviceRequestRepository.Add(serviceRequest));
        }
    }
}