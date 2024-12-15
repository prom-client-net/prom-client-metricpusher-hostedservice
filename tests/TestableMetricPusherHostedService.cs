namespace Prometheus.Client.MetricPusher.HostedService.Tests;

public class TestableMetricPusherHostedService(IMetricPusher pusher) : MetricPusherHostedService(pusher)
{
    public bool ErrorHandled { get; private set; }

    protected override void OnPushError(IMetricPusher metricPusher, Exception exception)
    {
        ErrorHandled = true;
    }
}
