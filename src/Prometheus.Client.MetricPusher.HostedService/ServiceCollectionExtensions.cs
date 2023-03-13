using System;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Prometheus.Client.Collectors;

namespace Prometheus.Client.MetricPusher.HostedService;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddMetricPusherService(this IServiceCollection services, MetricPusherOptions options, TimeSpan interval)
    {
        if (options == null) throw new ArgumentNullException(nameof(options));
        if (interval == TimeSpan.Zero) throw new ArgumentOutOfRangeException(nameof(interval));

        services.AddSingleton<IHostedService, MetricPusherService>(provider =>
        {
            options.CollectorRegistry ??= provider.GetRequiredService<ICollectorRegistry>();
            return new MetricPusherService(options, interval);
        });

        return services;
    }
}
