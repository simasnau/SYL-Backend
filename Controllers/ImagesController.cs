using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace SYL_Backend.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class ImagesController : ControllerBase
    {
        private readonly ILogger<ImagesController> _logger;

        public ImagesController(ILogger<ImagesController> logger)
        {
            _logger = logger;
        }


        
        // GET local images (usually product images)
        [HttpGet("{name}")]
        public async Task<IActionResult> GetAsync(string name)
        {
            try
            {
                var path = AppDomain.CurrentDomain.BaseDirectory + "images/" + name + ".jpg";
                var file=System.IO.File.ReadAllBytes(path);

                _logger.LogInformation(path);

                return File(file, "image/jpeg");
            }
            catch (Exception e)
            {
                return BadRequest();

            }
            

        }




    }
}
