using Microsoft.AspNetCore.Mvc;
using System.Net.Mime;
using Microsoft.AspNetCore.Http;
namespace dedreira.samples.webapi.Controllers
{
    [ApiController]
    [Produces(MediaTypeNames.Application.Json)]
    [ApiVersion("1.0")]
    [Route("/api/v{version:apiVersion}/[controller]")]
    public class HelloController: ControllerBase
    {
        [HttpGet]                
        [ProducesResponseType(StatusCodes.Status200OK)]
        public IActionResult SayHello(string name)
        {
            return Ok($"Hello {name}");            
        }
    }
}
