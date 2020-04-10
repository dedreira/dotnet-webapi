using Microsoft.AspNetCore.Mvc;
using System.Net.Mime;
using Microsoft.AspNetCore.Http;
namespace dedreira.samples.webapi.Controllers
{
    [ApiController]
    [Produces(MediaTypeNames.Application.Json)]
    [Route("/api/[controller]")]
    public class HelloController: ControllerBase
    {
        [HttpGet]        
        [ApiVersion("1.0")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public IActionResult SayHello(string name)
        {
            return Ok($"Hello {name}");            
        }
    }
}
