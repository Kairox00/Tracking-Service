using Dummy_Server.Models;

namespace Tracking_Service.Handlers
{
    public interface IHandler
    {
        Task SendToSegment(SpecMessage msg);
    }
}
