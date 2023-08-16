using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace Prometheus.Client.MetricPusher.HostedService.Tests
{
    public class ServiceCollecttionExtensionsTests
    {
        [Fact]
        public void AddMetricPusherService_WithNullMetricPusher_ThrowsArgumentNullException()
        {
            var servicesCollection = Substitute.For<IServiceCollection>();

            Action act = () => servicesCollection.AddMetricPusherService(null, TimeSpan.FromSeconds(1));

            act.Should().Throw<ArgumentNullException>();
        }

        [Fact]
        public void AddMetricPusherService_WithZeroTimeInterval_ThrowsArgumentOutOfRangeException()
        {
            var servicesCollection = Substitute.For<IServiceCollection>();
            var metricPusher = Substitute.For<IMetricPusher>();

            Action act = () => servicesCollection.AddMetricPusherService(metricPusher, TimeSpan.Zero);

            act.Should().Throw<ArgumentOutOfRangeException>();
        }

        [Fact]
        public void AddMetricPusherService_WithValidParameterValues_AddsMetricPusherServiceInServiceCollection()
        {
            var servicesCollection = new ServiceCollection();
            var metricPusher = Substitute.For<IMetricPusher>();

            servicesCollection.AddMetricPusherService(metricPusher, TimeSpan.FromSeconds(1));
            var provider = servicesCollection.BuildServiceProvider();
            var service = provider.GetRequiredService<IHostedService>();

            service.Should().BeOfType<MetricPusherService>();
        }
    }
}
