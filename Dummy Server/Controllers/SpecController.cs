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
                {"needCommon","1,2,10" }
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
                {"needCommon","3" }
            };
            SpecMessage msg = new SpecMessage("7893", properties);

            publisher.Publish("Track", msg.Serialize());
            return Ok(msg);
        }

        [HttpGet("{id}/props/{commonIndex}")]
        public IActionResult GetCommonProp(string id, string commonIndex)
        {
            var resp = CommonProps.Instance.GetOne(id, commonIndex);
            return Ok(resp);
        }
    }
}
