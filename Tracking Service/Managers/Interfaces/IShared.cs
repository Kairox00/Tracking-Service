using Gameball.MassTransit.DTOs.Segment;

namespace Tracking_Service.Managers.Interfaces
{
    public interface IShared
    {
        Task<Dictionary<string, object>> ProcessMessage(SpecMessage msg);
    }
}
