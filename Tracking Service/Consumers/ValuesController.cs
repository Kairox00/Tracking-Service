using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Tracking_Service.Cache;

namespace Tracking_Service.Consumers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ValuesController : ControllerBase
    {
        private ICacheService _cacheService;
        public ValuesController(ICacheService cacheService)
        {
            _cacheService = cacheService;
        }


        [HttpGet]
        public IActionResult Index()
        {
            _cacheService.Set("galal", "12");
            return Ok("hi redis");
        }

        [HttpGet("r")]
        public IActionResult REdis()
        {
            _cacheService.Set("galal", "12");
            return Ok("hi redis");
        }
    }

}
