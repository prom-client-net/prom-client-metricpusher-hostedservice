using System;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace Prometheus.Client.MetricPusher.HostedService;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddMetricPusherHostedService(this IServiceCollection services, IMetricPusher pusher)
    {
        return AddMetricPusherHostedService(services, pusher, Defaults.PushInterval);
    }

    public static IServiceCollection AddMetricPusherHostedService(this IServiceCollection services, IMetricPusher pusher, TimeSpan pushInterval)
    {
        if (pusher == null)
            throw new ArgumentNullException(nameof(pusher));
        if (pushInterval == TimeSpan.Zero)
            throw new ArgumentOutOfRangeException(nameof(pushInterval));

        services.AddSingleton<IHostedService, MetricPusherHostedService>(_ => new MetricPusherHostedService(pusher, pushInterval));

        return services;
    }
}
