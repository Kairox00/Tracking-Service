using Gameball.MassTransit;

namespace Tracking_Service.Handlers
{
    public interface IHandler
    {
        /// <summary>
        /// Sends the content of <paramref name="msg"/> to the tracking service.
        /// </summary>
        /// <param name="msg"></param>
        /// <returns></returns>
        Task SendToTracker(SpecMessage msg);
       
    }
}
