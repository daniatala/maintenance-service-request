using System;
using System.Collections.Generic;
using ServiceRequest.DataModel;

namespace ServiceRequest.Services.Interfaces
{
    public interface IServiceRequestService
    {
        IList<ServiceRequestModel> GetAll();
        ServiceRequestModel GetById(Guid serviceRequestId);
        ServiceRequestModel Add(ServiceRequestModel serviceRequest);
        ServiceRequestModel Update(Guid serviceRequestId, ServiceRequestModel serviceRequest);
    }
}
