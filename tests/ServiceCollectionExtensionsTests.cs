using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace Prometheus.Client.MetricPusher.HostedService.Tests;

public class ServiceCollectionExtensionsTests
{
    [Fact]
    public void AddMetricPusherService_WithNullMetricPusher_ThrowsArgumentNullException()
    {
        var servicesCollection = Substitute.For<IServiceCollection>();

        Assert.Throws<ArgumentNullException>(() => servicesCollection.AddMetricPusherHostedService(null, TimeSpan.FromSeconds(1)));
    }

    [Fact]
    public void AddMetricPusherService_WithZeroTimeInterval_ThrowsArgumentOutOfRangeException()
    {
        var servicesCollection = Substitute.For<IServiceCollection>();
        var metricPusher = Substitute.For<IMetricPusher>();

        Assert.Throws<ArgumentOutOfRangeException>(() => servicesCollection.AddMetricPusherHostedService(metricPusher, TimeSpan.Zero));
    }

    [Fact]
    public void AddMetricPusherService_WithDefaultInterval_AddsMetricPusherServiceInServiceCollection()
    {
        var servicesCollection = new ServiceCollection();
        var metricPusher = Substitute.For<IMetricPusher>();

        servicesCollection.AddMetricPusherHostedService(metricPusher);
        var provider = servicesCollection.BuildServiceProvider();
        var service = provider.GetRequiredService<IHostedService>();

        Assert.IsType<MetricPusherHostedService>(service);
    }

    [Fact]
    public void AddMetricPusherService_WithValidParameterValues_AddsMetricPusherServiceInServiceCollection()
    {
        var servicesCollection = new ServiceCollection();
        var metricPusher = Substitute.For<IMetricPusher>();

        servicesCollection.AddMetricPusherHostedService(metricPusher, TimeSpan.FromSeconds(1));
        var provider = servicesCollection.BuildServiceProvider();
        var service = provider.GetRequiredService<IHostedService>();

        Assert.IsType<MetricPusherHostedService>(service);
    }
}
