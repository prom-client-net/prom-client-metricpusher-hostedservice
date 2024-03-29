using System;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace Prometheus.Client.MetricPusher.HostedService;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddMetricPusherService(this IServiceCollection services, IMetricPusher pusher)
    {
        return AddMetricPusherService(services, pusher, Defaults.Interval);
    }

    public static IServiceCollection AddMetricPusherService(this IServiceCollection services, IMetricPusher pusher, TimeSpan interval)
    {
        if (pusher == null)
            throw new ArgumentNullException(nameof(pusher));
        if (interval == TimeSpan.Zero)
            throw new ArgumentOutOfRangeException(nameof(interval));

        services.AddSingleton<IHostedService, MetricPusherService>(_ => new MetricPusherService(pusher, interval));

        return services;
    }
}
