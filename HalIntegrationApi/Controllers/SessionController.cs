using System.Collections.Generic;
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
        public IActionResult Post([FromBody] SessionRequest sessionRequest)
        {
            return new ContentResult
            {
                ContentType = "application/json",
                Content = sessionRequest.SessionId
            };
        }
    }

    public class SessionRequest
    {
        public string SessionId { get; set; }
    }
}
