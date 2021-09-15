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

        [HttpGet(Name = "GetAll")]
        public IActionResult GetAll()
        {
            var serviceRequest = _serviceRequestService.GetAll();
            if (serviceRequest.Any())
                return Ok(_mapper.Map<IList<ServiceRequestModel>, IList<ServiceRequestModelResponse>>(serviceRequest));
            return NoContent();
        }

        [HttpGet("{id}" ,Name = "GetById")]
        [Route("{id}")]
        public IActionResult GetById([FromRoute] Guid id)
        {
            var serviceRequest = _serviceRequestService.GetById(id);
            if (serviceRequest != null)
                return Ok(_mapper.Map<ServiceRequestModel, ServiceRequestModelResponse>(serviceRequest));
            return NotFound();
        }

        [HttpPost]
        public IActionResult Post([FromBody] ServiceRequestModelRequest newServiceRequest)
        {
            var serviceRequest = _serviceRequestService.Add(_mapper.Map<ServiceRequestModelRequest, ServiceRequestModel>(newServiceRequest));
            var serviceRequestModelResponse = _mapper.Map<ServiceRequestModel, ServiceRequestModelResponse>(serviceRequest);
            return CreatedAtRoute("GetById", new {id = serviceRequest.Id.ToString("N")},
                serviceRequestModelResponse);
        }

        [HttpPut]
        [Route("{id}")]
        public IActionResult Put([FromRoute(Name = "id")] Guid serviceRequestId, [FromBody] ServiceRequestModelRequest modifiedServiceRequest)
        {
            var serviceRequest = _serviceRequestService.Update(serviceRequestId, _mapper.Map<ServiceRequestModelRequest, ServiceRequestModel>(modifiedServiceRequest));
            if(serviceRequest != null)
                return Ok(_mapper.Map<ServiceRequestModel, ServiceRequestModelResponse>(serviceRequest));
            return NotFound();
        }

        [HttpDelete]
        [Route("{id}")]
        public IActionResult Delete([FromRoute(Name = "id")] Guid serviceRequestId)
        {
            _serviceRequestService.Delete(serviceRequestId);
            return Ok();
        }
    }
}
