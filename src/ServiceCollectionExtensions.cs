using System;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace Prometheus.Client.MetricPusher.HostedService;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddMetricPusherHostedService(this IServiceCollection services, IMetricPusher pusher)
    {
        return AddMetricPusherHostedService(services, pusher, Defaults.Interval);
    }

    public static IServiceCollection AddMetricPusherHostedService(this IServiceCollection services, IMetricPusher pusher, TimeSpan interval)
    {
        if (pusher == null)
            throw new ArgumentNullException(nameof(pusher));
        if (interval == TimeSpan.Zero)
            throw new ArgumentOutOfRangeException(nameof(interval));

        services.AddSingleton<IHostedService, MetricPusherHostedService>(_ => new MetricPusherHostedService(pusher, interval));

        return services;
    }
}
