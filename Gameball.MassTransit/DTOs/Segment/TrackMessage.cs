using MassTransit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gameball.MassTransit.DTOs.Segment
{
    [EntityName(MessageQueueConstants.TrackQueue)]
    public class TrackMessage : SpecMessage
    {
        public TrackMessage(string ClientId, Dictionary<string, object> Properties) : base(ClientId, Properties)
        {
        }
    }
}
