using System.Collections.Generic;

namespace ServiceRequest.DataAccess.Interfaces
{
    public interface IServiceRequestRepository
    {
        void Add(string serviceRequest);
        IList<string> GetAll();
    }
}
