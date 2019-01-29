using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace HalIntegrationApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DetectedDevicesController : ControllerBase
    {
        [HttpGet]
        public IEnumerable<string> Get()
        {
            return new[] {"value1", "value2"};
        }

    }
}