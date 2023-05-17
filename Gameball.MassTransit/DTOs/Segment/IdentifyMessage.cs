using MassTransit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gameball.MassTransit.DTOs.Segment
{
    [EntityName(MessageQueueConstants.IdentifyQueue)]
    public class IdentifyMessage : SpecMessage
    {
        public IdentifyMessage(string ClientId, Dictionary<string, object> Properties) : base(ClientId, Properties)
        {
        }
    }
}
