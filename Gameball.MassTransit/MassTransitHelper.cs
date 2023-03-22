using MassTransit;

namespace Gameball.MassTransit
{
    public static class MassTransitHelper
    {
        public static void AddHeaders(PublishContext context, int clientId)
        {
            context.Headers.Set("x-client-id", clientId);
        }
        public static void AddTraceHeaders(PublishContext context, string spanId, string traceId)
        {
            if (!string.IsNullOrWhiteSpace(spanId)) context.Headers.Set("dd_span_id", spanId);
            if (!string.IsNullOrWhiteSpace(traceId)) context.Headers.Set("dd_trace_id", traceId);
        }
    }

}
