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

            publisher.Publish("Identify", msg.Serialize());

            return Ok(msg);
        }

        [HttpGet("/t")]
        public IActionResult Track()
        {
            Dictionary<string, string> properties = new()
            {
                {"type", SpecType.Track.ToString() },
                {"event", "Click" },
                {"buttonId", "832" }
            };
            SpecMessage msg = new SpecMessage("1234", properties);

            publisher.Publish("Track", msg.Serialize());
            return Ok(msg);
        }

        [HttpGet("/g")]
        public IActionResult Group()
        {
            Dictionary<string, string> properties = new()
            {
                {"type", SpecType.Group.ToString() },
                {"groupId", "4576" },
                {"needCommon","2" }
            };
            SpecMessage msg = new SpecMessage("1234", properties);

            publisher.Publish("Group", msg.Serialize());
            return Ok(msg);
        }

        [HttpGet("/a")]
        public IActionResult Alias()
        {
            Dictionary<string, string> properties = new()
            {
                {"type", SpecType.Alias.ToString() },
                {"newId", "7878" }
            };
            SpecMessage msg = new SpecMessage("1234", properties);

            publisher.Publish("Alias", msg.Serialize());
            return Ok(msg);
        }

        [HttpGet("{id}/props/{commonIndex}")]
        public IActionResult GetCommonProp(string id, string commonIndex)
        {
            var resp = CommonProps.Instance.GetOne(id, commonIndex);
            return Ok(resp);
        }

        [HttpGet("{id}/propsAll/{commonIndeces}")]
        public IActionResult GetAllCommonProp(string id, string commonIndeces)
        {
            var resp = CommonProps.Instance.GetAll(id, commonIndeces);
            return Ok(resp);
        }
    }
}
