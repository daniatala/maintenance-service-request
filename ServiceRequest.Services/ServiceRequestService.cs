using System.Collections.Generic;
using ServiceRequest.DataAccess.Interfaces;
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

        public IList<string> GetAll()
        {
            return _serviceRequestRepository.GetAll();
        }
    }
}