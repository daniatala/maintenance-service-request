using System.Linq;
using Microsoft.AspNetCore.Mvc;
using ServiceRequest.Services.Interfaces;

namespace ServiceRequest.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ServiceRequestController : ControllerBase
    {
        private readonly IServiceRequestService _serviceRequestService;

        public ServiceRequestController(IServiceRequestService serviceRequestService)
        {
            _serviceRequestService = serviceRequestService;
        }

        [HttpGet]
        public IActionResult Get()
        {
            var requestService = _serviceRequestService.GetAll();
            if (requestService.Any())
                return Ok(requestService);
            return NoContent();
        }

        [HttpGet]
        public IActionResult Get(long serviceRequestId)
        {
            return NotFound();
        }
    }
}
