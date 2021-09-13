using System.Collections.Generic;

namespace ServiceRequest.Services.Interfaces
{
    public interface IServiceRequestService
    {
        IList<string> GetAll();
    }
}
