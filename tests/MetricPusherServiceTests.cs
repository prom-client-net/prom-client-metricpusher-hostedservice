using System.Threading;
using System.Threading.Tasks;

namespace Prometheus.Client.MetricPusher.HostedService.Tests;

public class MetricPusherServiceTests
{
    [Fact]
    public async Task WithDefaultInterval_PushMetricPeriodically()
    {
        var metricPusherMock = Substitute.For<IMetricPusher>();
        var metricPusherService = new MetricPusherService(metricPusherMock);
        var canellationToken = Arg.Any<CancellationToken>();

        await metricPusherService.StartAsync(canellationToken);
        await Task.Delay(GetDelay(1), canellationToken);
        await metricPusherService.StopAsync(canellationToken);

        await metricPusherMock.Received(3).PushAsync();
    }

    [Theory]
    [InlineData(1)]
    [InlineData(2)]
    [InlineData(3)]
    [InlineData(5)]
    public async Task WithGivenInterval_PushMetricPeriodically(int seconds)
    {
        var metricPusherMock = Substitute.For<IMetricPusher>();
        var metricPusherService = new MetricPusherService(metricPusherMock, TimeSpan.FromSeconds(seconds));
        var canellationToken = Arg.Any<CancellationToken>();

        await metricPusherService.StartAsync(canellationToken);
        await Task.Delay(GetDelay(seconds), canellationToken);
        await metricPusherService.StopAsync(canellationToken);

        await metricPusherMock.Received(3).PushAsync();
    }

    private static TimeSpan GetDelay(int seconds)
    {
        var milliseconds = seconds * 1000 + 100;
        return TimeSpan.FromMilliseconds(milliseconds);
    }
}
