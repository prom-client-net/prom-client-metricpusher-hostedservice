using System;

namespace Prometheus.Client.MetricPusher.HostedService;

internal static class Defaults
{
    internal static TimeSpan Interval = TimeSpan.FromMilliseconds(1000);
}
