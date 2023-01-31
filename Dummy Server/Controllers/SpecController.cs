using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Reflection;
using Dummy_Server.Services;
using Dummy_Server.Models;

namespace Dummy_Server.Controllers
{
    [ApiController]
    public class SpecController : ControllerBase
    {
        Publisher publisher = new();

        [HttpGet("/")]
        public IActionResult Index()
        {
            return Ok("Hello");
        }

        [HttpGet("/i")]
        public IActionResult Identify()
        {
            SpecMessage msg = new SpecMessage(
                type: SpecType.Identify.ToString(),
                userId: "1234",
                traits: new Dictionary<string, object>(){
                    {"name", 92221},
                    {"email", "a@a.com" }
                });

            publisher.Publish("queue", msg.Serialize());
            return Ok(msg);
        }

        [HttpGet("/t")]
        public IActionResult Track()
        {
            SpecMessage msg = new SpecMessage(
                type: SpecType.Track.ToString(),
                userId: "7893",
                @event: "Click",
                properties: new Dictionary<string, object>(){
                    {"buttonId", 832}
                });

            publisher.Publish("queue", msg.Serialize());
            return Ok(msg);
        }
    }
}
