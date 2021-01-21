using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DotNetCoreApiStarter.Models;
using Microsoft.AspNetCore.Mvc;

namespace DotNetCoreApiStarter.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ValuesController : ControllerBase
    {
        // GET api/values
        [HttpGet]
        public IActionResult Get()
        {
            //var users = await _service.GetAll();
            return Ok("Vales");
        }

        
    }
}
