using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Reflection;
using Dummy_Server.Services;
using Dummy_Server.Models;
using Dummy_Server.Database;

namespace Dummy_Server.Controllers
{
    [ApiController]
    public class SpecController : ControllerBase
    {
        Publisher publisher = new();

        [HttpGet("/")]
        public IActionResult Index()
        {
            var db = DB.Instance;
           
            //new DB_Receiver().StartReceiving("From_Service");

            return Ok("hi");
        }

        [HttpGet("/i")]
        public IActionResult Identify()
        {
            Dictionary<string, string> properties = new()
            {
                {"type", SpecType.Identify.ToString() },
                {"age", "21" },
                {"email", "a@aa.com" },
                {"needG","1,2,10" }
            };
            SpecMessage msg = new SpecMessage("123", properties);

            publisher.Publish("Spec_Call", msg.Serialize());

            return Ok(msg);
        }

        [HttpGet("/t")]
        public IActionResult Track()
        {
            Dictionary<string, string> properties = new()
            {
                {"type", SpecType.Track.ToString() },
                {"event", "Click" },
                {"buttonId", "832" },
                {"needG","3" }
            };
            SpecMessage msg = new SpecMessage("7893", properties);

            publisher.Publish("Track", msg.Serialize());
            return Ok(msg);
        }

        [HttpGet("{userId}/props/G/{gIndex}")]
        public IActionResult GetGProp(string id, string gIndex)
        {
            var resp = GProps.Instance.GetOne(id, gIndex);
            return Ok(resp);
        }
    }
}
