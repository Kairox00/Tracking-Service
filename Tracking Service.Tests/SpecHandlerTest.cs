using Gameball.MassTransit.DTOs.Segment;
using Segment;
using Newtonsoft.Json.Linq;
using Tracking_Service.Consumers;
using Tracking_Service.Handlers;
using Options = Segment.Model.Options;

namespace Tracking_Service.Tests
{
    public class SpecHandlerTest
    {

        [Fact]
        public async void TestProcessIdentifyMessage()
        {
            Analytics.Initialize("xOYECODe4mKLEVWyF5ZGoE04cU8CxnTj");
            var clientId = "123";
            var age = 21;
            var email = "a@aa.com";

            Dictionary<string, object> properties = new()
            {
                {"age", age },
                {"email", email }
            };
            SpecMessage msg = new IdentifyMessage(clientId, properties);
            SpecHandler handler = new IdentifyHandler();

            Dictionary<string, object> result = await handler.ProcessMessage(msg);
            Dictionary<string, object> args = (Dictionary<string, object>)result["args"];

            Assert.Equal(clientId, result["clientId"]);
            Assert.Equal(age, args["age"]);
            Assert.Equal(email, args["email"]);
        }

        [Fact]
        public async void TestProcessTrackMessage()
        {
            Analytics.Initialize("xOYECODe4mKLEVWyF5ZGoE04cU8CxnTj");
            var clientId = "123";
            var @event = "Button Clicked";
            var email = "a@aa.com";

            Dictionary<string, object> properties = new()
            {
                {"event", @event },
                {"email", email }
            };
            SpecMessage msg = new TrackMessage(clientId, properties);
            SpecHandler handler = new TrackHandler();

            Dictionary<string, object> result = await handler.ProcessMessage(msg);
            Dictionary<string, object> args = (Dictionary<string, object>)result["args"];

            Assert.Equal(clientId, result["clientId"]);
            Assert.Equal(@event, result["event"]);
            Assert.Equal(email, args["email"]);
        }

        [Fact]
        public async void TestProcessGroupMessage()
        {
            Analytics.Initialize("xOYECODe4mKLEVWyF5ZGoE04cU8CxnTj");
            var clientId = "123";
            var groupId = "Group345";
            var email = "a@aa.com";

            Dictionary<string, object> properties = new()
            {
                {"groupId", groupId },
                {"email", email }
            };
            SpecMessage msg = new GroupMessage(clientId, properties);
            SpecHandler handler = new GroupHandler();

            Dictionary<string, object> result = await handler.ProcessMessage(msg);
            Dictionary<string, object> args = (Dictionary<string, object>)result["args"];

            Assert.Equal(clientId, result["clientId"]);
            Assert.Equal(groupId, result["groupId"]);
            Assert.Equal(email, args["email"]);
        }

        [Fact]
        public async void TestProcessAliasMessage()
        {
            Analytics.Initialize("xOYECODe4mKLEVWyF5ZGoE04cU8CxnTj");
            var clientId = "123";
            var newId = "456";
            var email = "a@aa.com";

            Dictionary<string, object> properties = new()
            {
                {"newId", newId },
                {"email", email }
            };
            SpecMessage msg = new AliasMessage(clientId, properties);
            SpecHandler handler = new AliasHandler();

            Dictionary<string, object> result = await handler.ProcessMessage(msg);
            Dictionary<string, object> args = (Dictionary<string, object>)result["args"];

            Assert.Equal(clientId, result["clientId"]);
            Assert.Equal(newId, result["newId"]);
            Assert.Equal(email, args["email"]);

        }

        [Fact]
        public void TestAddErrorToContext()
        {
            Analytics.Initialize("xOYECODe4mKLEVWyF5ZGoE04cU8CxnTj");
            var clientId = "123";
            var newId = "456";
            var error = "Error message";

            Dictionary<string, object> properties = new()
            {
                {"newId", newId },
                {"error", error }
            };
            SpecMessage msg = new AliasMessage(clientId, properties);
            Options result = SpecHandler.AddErrorToContext(msg);
            Assert.Equal(error, result.Context["error"]);
        }

        [Fact]
        public void TestGetCommonKeys()
        {
            object commonKeys1 = "1";
            object commonKeys2 = new JArray(new string[] { "1", "2" });
            string[] res1 = SpecHandler.GetCommonKeys(commonKeys1);
            string[] res2 = SpecHandler.GetCommonKeys(commonKeys2);
            Assert.Single(res1);
            Assert.Equal(2, res2.Length);
        }
    }
}