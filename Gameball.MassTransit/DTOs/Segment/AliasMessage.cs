using MassTransit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gameball.MassTransit.DTOs.Segment
{
    [EntityName(MessageQueueConstants.AliasQueue)]
    public class AliasMessage : SpecMessage
    {
        public AliasMessage(string ClientId, Dictionary<string, object> Properties) : base(ClientId, Properties)
        {
        }
    }
}
