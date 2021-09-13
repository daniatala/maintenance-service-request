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

        public void Add(ServiceRequestModel serviceRequest)
        {
            _serviceRequestsList.Add(serviceRequest);
        }

        public IList<ServiceRequestModel> GetAll()
        {
            return _serviceRequestsList;
        }

        public ServiceRequestModel GetById(Guid serviceRequestId)
        {
            return _serviceRequestsList.FirstOrDefault(sr => sr.Id == serviceRequestId);
        }
    }
}
