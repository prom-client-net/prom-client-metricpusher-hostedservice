using System.Threading;
using System.Threading.Tasks;

namespace Prometheus.Client.MetricPusher.HostedService.Tests;

public class MetricPusherHostedServiceTests
{
    [Fact]
    public async Task WithDefaultInterval_PushMetricPeriodically()
    {
        var pusher = Substitute.For<IMetricPusher>();
        var ct = Arg.Any<CancellationToken>();

        var hostedService = new MetricPusherHostedService(pusher);

        await hostedService.StartAsync(ct);
        await Task.Delay(GetDelay(1), ct);
        await hostedService.StopAsync(ct);

        await pusher.Received(3).PushAsync();
    }

    [Theory]
    [InlineData(1)]
    [InlineData(2)]
    [InlineData(3)]
    [InlineData(5)]
    public async Task WithGivenInterval_PushMetricPeriodically(int seconds)
    {
        var pusher = Substitute.For<IMetricPusher>();
        var ct = Arg.Any<CancellationToken>();

        var hostedService = new MetricPusherHostedService(pusher, TimeSpan.FromSeconds(seconds));

        await hostedService.StartAsync(ct);
        await Task.Delay(GetDelay(seconds), ct);
        await hostedService.StopAsync(ct);

        await pusher.Received(3).PushAsync();
    }

    [Fact]
    public async Task OnPushError_HandlesException()
    {
        var pusher = Substitute.For<IMetricPusher>();
        pusher.PushAsync().Returns(Task.FromException(new Exception("Simulated Push Exception")));
        var ct = Arg.Any<CancellationToken>();

        var hostedService = new TestableMetricPusherHostedService(pusher);

        await hostedService.StartAsync(ct);
        await Task.Delay(GetDelay(1), ct);
        await hostedService.StopAsync(ct);

        hostedService.ErrorHandled.Should().BeTrue();
    }

    private static TimeSpan GetDelay(int seconds)
    {
        var milliseconds = seconds * 1000 + 100;
        return TimeSpan.FromMilliseconds(milliseconds);
    }
}
