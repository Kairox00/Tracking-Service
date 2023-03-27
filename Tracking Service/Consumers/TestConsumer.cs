using Gameball.MassTransit;
using MassTransit;
using Tracking_Service.Handlers;

namespace Tracking_Service.Consumers
{
    public class TestConsumer : IConsumer<TestMessage>
    {
        private readonly ILogger<TestConsumer> _logger;
        //private readonly IHandler _handler;

        public TestConsumer(ILogger<TestConsumer> logger)
        {
            _logger = logger;
 //           _handler = new IdentifyHandler();
        }

        public async Task Consume(ConsumeContext<TestMessage> context)
        {
            var message = context.Message;
            _logger.LogInformation("Identify, {ClientId}", context.Message.ClientId);
          //  await _handler.SendToTracker(context.Message);

        }
    }
}
