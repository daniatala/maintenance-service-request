using System;
using System.Collections.Generic;
using System.Linq;
using ServiceRequest.DataAccess.Interfaces;
using ServiceRequest.DataModel;

namespace ServiceRequest.DataAccess
{
    public class ServiceRequestRepository : IServiceRequestRepository
    {
        private readonly IList<ServiceRequestModel> _serviceRequestsList;

        public ServiceRequestRepository()
        {
            _serviceRequestsList = new List<ServiceRequestModel>();
        }

        public Guid Add(ServiceRequestModel serviceRequest)
        {
            var serviceRequestId = serviceRequest.Id == Guid.Empty ? Guid.NewGuid() : serviceRequest.Id;
            _serviceRequestsList.Add(new ServiceRequestModel(serviceRequestId, serviceRequest.BuildingCode,
                serviceRequest.Description, serviceRequest.CurrentStatus, serviceRequest.CreatedBy,
                serviceRequest.CreatedDate, serviceRequest.LastModifiedBy, serviceRequest.LastModifiedDate));
            return serviceRequestId;
        }

        public IList<ServiceRequestModel> GetAll()
        {
            return _serviceRequestsList;
        }

        public ServiceRequestModel GetById(Guid serviceRequestId)
        {
            return _serviceRequestsList.FirstOrDefault(sr => sr.Id == serviceRequestId);
        }

        public ServiceRequestModel Update(Guid serviceRequestId, ServiceRequestModel modifiedServiceRequest)
        {
            var serviceRequest = _serviceRequestsList.FirstOrDefault(sr => sr.Id == serviceRequestId);
            _serviceRequestsList.Remove(serviceRequest);
            _serviceRequestsList.Add(new ServiceRequestModel(serviceRequestId, modifiedServiceRequest.BuildingCode,
                modifiedServiceRequest.Description, modifiedServiceRequest.CurrentStatus, modifiedServiceRequest.CreatedBy,
                modifiedServiceRequest.CreatedDate, modifiedServiceRequest.LastModifiedBy, modifiedServiceRequest.LastModifiedDate));
            return GetById(serviceRequestId);
        }
    }
}
