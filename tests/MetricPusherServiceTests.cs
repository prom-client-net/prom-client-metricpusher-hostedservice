using System.Threading;
using System.Threading.Tasks;

namespace Prometheus.Client.MetricPusher.HostedService.Tests
{
    public class MetricPusherServiceTests
    {
        [Fact]
        public async Task WithGivenInterval_PushMetricPeriodically()
        {
            var metricPusherMock = Substitute.For<IMetricPusher>();
            var metricPusherService = new MetricPusherService(metricPusherMock, TimeSpan.FromSeconds(1));
            var canellationToken = Arg.Any<CancellationToken>();

            await metricPusherService.StartAsync(canellationToken);
            await Task.Delay(TimeSpan.FromSeconds(1), canellationToken);
            await metricPusherService.StopAsync(canellationToken);

            await metricPusherMock.Received(3).PushAsync();
        }
    }
}
