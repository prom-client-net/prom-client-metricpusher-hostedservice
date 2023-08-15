using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace Prometheus.Client.MetricPusher.HostedService.Tests
{
    public class ServiceCollecttionExtensionsTests
    {
        [Fact]
        public void AddMetricPusherService_WithNullMetricPusher_ThrowsArgumentNullException()
        {
            IServiceCollection servicesCollection = Moq.Mock.Of<IServiceCollection>();

            Action act = () => ServiceCollectionExtensions.AddMetricPusherService(servicesCollection, null, TimeSpan.FromSeconds(1));

            act.Should().Throw<ArgumentNullException>();
        }

        [Fact]
        public void AddMetricPusherService_WithZeroTimeInterval_ThrowsArgumentOutOfRangeException()
        {
            IServiceCollection servicesCollection = Moq.Mock.Of<IServiceCollection>();
            IMetricPusher metricPusher = Moq.Mock.Of<IMetricPusher>();

            Action act = () => ServiceCollectionExtensions.AddMetricPusherService(servicesCollection, metricPusher, TimeSpan.Zero);

            act.Should().Throw<ArgumentOutOfRangeException>();
        }

        [Fact]
        public void AddMetricPusherService_WithValidParameterValues_AddsMetricPusherServiceInServiceCollection()
        {
            IServiceCollection servicesCollection = new ServiceCollection();
            IMetricPusher metricPusher = Moq.Mock.Of<IMetricPusher>();

            ServiceCollectionExtensions.AddMetricPusherService(servicesCollection, metricPusher, TimeSpan.FromSeconds(1));
            var provider = servicesCollection.BuildServiceProvider();
            var service = provider.GetRequiredService<IHostedService>();

            service.Should().BeOfType<MetricPusherService>();
        }
    }
}
