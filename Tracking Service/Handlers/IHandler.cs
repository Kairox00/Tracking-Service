using Dummy_Server.Models;

namespace Tracking_Service.Handlers
{
    public interface IHandler
    {
        Task MakeCall(SpecMessage msg);
    }
}
