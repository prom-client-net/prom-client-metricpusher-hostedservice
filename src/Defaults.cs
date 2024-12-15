using System;

namespace Prometheus.Client.MetricPusher.HostedService;

internal static class Defaults
{
    internal static TimeSpan PushInterval = TimeSpan.FromMilliseconds(1000);
}
