using System.Collections.Generic;
using ServiceRequest.DataAccess.Interfaces;

namespace ServiceRequest.DataAccess
{
    public class ServiceRequestRepository : IServiceRequestRepository
    {
        private readonly IList<string> _serviceRequestsList;

        public ServiceRequestRepository()
        {
            _serviceRequestsList = new List<string>();
        }

        public void Add(string serviceRequest)
        {
            _serviceRequestsList.Add(serviceRequest);
        }

        public IList<string> GetAll()
        {
            return _serviceRequestsList;
        }
    }
}
