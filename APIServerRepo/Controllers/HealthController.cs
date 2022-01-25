using APIServer.Application.WebAPI.Models;
using APIServer.Core.Exceptions;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Diagnostics.HealthChecks;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace APIServerRepo.Controllers
{
    [Route("api/health")]
    [ApiController]
    public class HealthController : ControllerBase
    {
        private readonly ILogger<HealthController> _logger;
        private readonly HealthCheckService _service;

        public HealthController(ILogger<HealthController> logger, HealthCheckService service)
        {
            _logger = logger;
            _service = service;
        }

        [HttpGet]
        [ProducesResponseType(typeof(CheckHealthResponse), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(HealthReport), (int)HttpStatusCode.ServiceUnavailable)]
        public async Task<IActionResult> Get()
        {
            var report = await _service.CheckHealthAsync();
            _logger.LogInformation($"Get Health Information: {report}");

            return report.Status == HealthStatus.Healthy ? Ok(new CheckHealthResponse { Status = HttpStatusCode.OK.ToString()}) : StatusCode((int)HttpStatusCode.ServiceUnavailable, report);
        }
    }
}
