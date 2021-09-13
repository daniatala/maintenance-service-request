using System;
using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using ServiceRequest.DataModel;
using ServiceRequest.Services.Interfaces;
using ServiceRequest.ViewModels;

namespace ServiceRequest.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ServiceRequestController : ControllerBase
    {
        private readonly IServiceRequestService _serviceRequestService;
        private readonly IMapper _mapper;

        public ServiceRequestController(IServiceRequestService serviceRequestService, IMapper mapper)
        {
            _serviceRequestService = serviceRequestService;
            _mapper = mapper;
        }

        [HttpGet]
        public IActionResult Get()
        {
            var requestService = _serviceRequestService.GetAll();
            if (requestService.Any())
                return Ok(_mapper.Map<IList<ServiceRequestModel>, IList<ServiceRequestModelResponse>>(requestService));
            return NoContent();
        }

        [HttpGet]
        [Route("{id}")]
        public IActionResult Get(Guid serviceRequestId)
        {
            var requestService = _serviceRequestService.GetById(serviceRequestId);
            if (requestService != null)
                return Ok(_mapper.Map<ServiceRequestModel, ServiceRequestModelResponse>(requestService));
            return NotFound();
        }

        [HttpPost]
        public IActionResult Post(ServiceRequestModelRequest newServiceRequest)
        {
            var requestService = _serviceRequestService.Add(_mapper.Map<ServiceRequestModelRequest, ServiceRequestModel>(newServiceRequest));
            return CreatedAtRoute("api/serviceRequest", _mapper.Map<ServiceRequestModel, ServiceRequestModelResponse>(requestService));
        }

        [HttpPut]
        [Route("{id}")]
        public IActionResult Put([FromRoute(Name = "id")] Guid serviceRequestId, [FromBody] ServiceRequestModelRequest modifiedServiceRequest)
        {
            var requestService = _serviceRequestService.Update(serviceRequestId, _mapper.Map<ServiceRequestModelRequest, ServiceRequestModel>(modifiedServiceRequest));
            return Ok(_mapper.Map<ServiceRequestModel, ServiceRequestModelResponse>(requestService));
        }
    }
}
