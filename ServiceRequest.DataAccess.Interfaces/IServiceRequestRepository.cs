using System;
using System.Collections.Generic;
using ServiceRequest.DataModel;

namespace ServiceRequest.DataAccess.Interfaces
{
    public interface IServiceRequestRepository
    {
        Guid Add(ServiceRequestModel serviceRequest);
        IList<ServiceRequestModel> GetAll();
        ServiceRequestModel GetById(Guid serviceRequestId);
        ServiceRequestModel Update(Guid serviceRequestId, ServiceRequestModel modifiedServiceRequest);
        void Delete(Guid serviceRequestId);
    }
}
