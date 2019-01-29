using System.Collections.Generic;
using HalIntegration.Common;
using Microsoft.AspNetCore.Mvc;

namespace HalIntegrationApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SessionController : ControllerBase
    {
        [HttpGet]
        public IEnumerable<string> Get()
        {
            return new[] {"value1", "value2"};
        }

        // POST: api/Default
        [HttpPost]
        // public IActionResult Post([FromBody] string sessionId)
        public IActionResult Post(SessionRequest sessionRequest)
        {
            return new ContentResult
            {
                ContentType = "application/json",
                Content = sessionRequest.SessionId
            };
        }
    }
}
