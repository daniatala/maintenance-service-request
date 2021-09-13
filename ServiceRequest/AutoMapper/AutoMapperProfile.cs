using AutoMapper;
using ServiceRequest.DataModel;
using ServiceRequest.ViewModels;

namespace ServiceRequest.AutoMapper
{
    /// <summary>
    /// Used to register classes to map
    /// </summary>
    public class AutoMapperProfile : Profile
    {
        /// <summary>
        /// Constructor
        /// </summary>
        public AutoMapperProfile()
        {
            CreateMap<ServiceRequestModel, ServiceRequestModelResponse>();
            CreateMap<ServiceRequestModelRequest, ServiceRequestModel>();
        }
    }

}
