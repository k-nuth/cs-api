using System;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace SSESample
{
    [Route("/api/sse")]
    public class SSEController {
        private readonly IHttpContextAccessor _httpContextAccessor;
        public SSEController(IHttpContextAccessor httpContextAccessor) {
            _httpContextAccessor = httpContextAccessor;
        }

        [HttpGet]
        public async Task Get() {
            var response = _httpContextAccessor.HttpContext.Response;
            response.Headers.Add("Content-Type", "text/event-stream");
            response.StatusCode = 200;

            for (var i = 0; true; ++i) {
                await response.WriteAsync($"data: Controller {i} at {DateTime.Now}\r\r");
                response.Body.Flush();
                await Task.Delay(5 * 1000);
            }
        }
    }
}