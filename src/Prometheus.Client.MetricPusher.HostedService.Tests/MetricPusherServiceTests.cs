using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Moq;

namespace Prometheus.Client.MetricPusher.HostedService.Tests
{
    public class MetricPusherServiceTests
    {
        [Fact]
        public async Task WithGivenInterval_PushMetricPeriodically()
        {
            var metricPusherMock = new Moq.Mock<IMetricPusher>();
            var metricPusherService = new MetricPusherService(metricPusherMock.Object, TimeSpan.FromSeconds(1));
            var canellationToken = It.IsAny<CancellationToken>();

            await metricPusherService.StartAsync(canellationToken);
            await Task.Delay(TimeSpan.FromSeconds(1));
            await metricPusherService.StopAsync(canellationToken);

            metricPusherMock.Verify(o => o.PushAsync(), Moq.Times.AtLeast(2));
        }
    }
}
