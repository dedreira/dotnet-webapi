using Microsoft.AspNetCore.Mvc;
using System.Net.Mime;
using Microsoft.AspNetCore.Http;
using System;
namespace dedreira.samples.webapi.Controllers
{
    [ApiController]
    [Produces(MediaTypeNames.Application.Json)]
    [ApiVersion("2.0")]
    [Route("/api/v{version:apiVersion}/[controller]")]
    public class ExceptionController: ControllerBase
    {
        [HttpGet]                
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public IActionResult ThrowException()
        {
            throw new Exception("Exception thrown");
        }
    }
}
