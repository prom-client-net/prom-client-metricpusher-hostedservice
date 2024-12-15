using System.Threading;
using System.Threading.Tasks;

namespace Prometheus.Client.MetricPusher.HostedService.Tests;

public class MetricPusherHostedServiceTests
{
    [Fact]
    public async Task WithDefaultInterval_PushMetricPeriodically()
    {
        var metricPusherMock = Substitute.For<IMetricPusher>();
        var metricPusherService = new MetricPusherHostedService(metricPusherMock);
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
        var metricPusherService = new MetricPusherHostedService(metricPusherMock, TimeSpan.FromSeconds(seconds));
        var canellationToken = Arg.Any<CancellationToken>();

        await metricPusherService.StartAsync(canellationToken);
        await Task.Delay(GetDelay(seconds), canellationToken);
        await metricPusherService.StopAsync(canellationToken);

        await metricPusherMock.Received(3).PushAsync();
    }

    [Fact]
    public async Task OnPushError_HandlesException()
    {
        var pusher = Substitute.For<IMetricPusher>();
        pusher.PushAsync().Returns(Task.FromException(new Exception("Simulated Push Exception")));
        var ct = Arg.Any<CancellationToken>();

        var hostedService = new TestableMetricPusherHostedService(pusher);
        await hostedService.StartAsync(ct);
        await Task.Delay(1000, ct);
        await hostedService.StopAsync(ct);

        Assert.True(hostedService.ErrorHandled);
    }

    private static TimeSpan GetDelay(int seconds)
    {
        var milliseconds = seconds * 1000 + 100;
        return TimeSpan.FromMilliseconds(milliseconds);
    }
}
