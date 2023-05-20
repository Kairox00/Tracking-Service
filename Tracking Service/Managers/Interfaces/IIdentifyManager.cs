using Gameball.MassTransit.DTOs.Segment;
namespace Tracking_Service.Managers.Interfaces
{
    public interface IIdentifyManager
    {
        Task SendToTracker(SpecMessage msg);
        bool Validate(Dictionary<string, object> data);
    }
}
