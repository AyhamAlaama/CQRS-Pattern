using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Post.Api.Controllers.V1
{
    //[Authorize(Roles ="Admin")]
    [Route("api/[controller]")]
    [ApiController]
    public class ServerDetailsController : ControllerBase
    {
        /// <summary>
        /// Get Server Dates Types
        /// </summary>
        /// <returns></returns>
        [HttpGet("Server/GetDate")]
        public IActionResult GetDate()
        {
            
            return Ok(new
            {
                DateTimeUtcNow = DateTime.UtcNow,
                DateTimeNow = DateTime.Now,
                DateTimeOffsetNow = DateTimeOffset.Now
            });
        }
    }
}
