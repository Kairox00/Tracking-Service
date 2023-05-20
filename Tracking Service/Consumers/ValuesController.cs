using Gameball.MassTransit.DTOs.Segment;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Tracking_Service.Cache;
using Tracking_Service.Managers.Interfaces;

namespace Tracking_Service.Consumers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ValuesController : ControllerBase
    {
        private ICacheService _cacheService;
        private IIdentifyManager _identifyManager;
        public ValuesController(ICacheService cacheService, IIdentifyManager identifyManager)
        {
            _cacheService = cacheService;
            _identifyManager = identifyManager;
        }


        [HttpGet]
        public IActionResult Index()
        {
            _cacheService.Set("galal", "12");
            return Ok("hi redis");
        }

        [HttpGet("r")]
        public IActionResult Redis()
        {
            _cacheService.Set("galal", "12");
            return Ok("hi redis");
        }

        [HttpGet("i")]
        public IActionResult Identify()
        {
            SpecMessage message = new SpecMessage("123", new Dictionary<string, object> { { "hi", "bye" } });
            _identifyManager.SendToTracker(message);
            return Ok("i");
        }
    }

}
