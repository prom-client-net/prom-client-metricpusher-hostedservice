using System;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Hosting;

namespace Prometheus.Client.MetricPusher.HostedService;

public class MetricPusherHostedService(IMetricPusher pusher, TimeSpan pushInterval) : BackgroundService
{
    public MetricPusherHostedService(IMetricPusher pusher)
        : this(pusher, Defaults.PushInterval)
    {
    }

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        while (!stoppingToken.IsCancellationRequested)
        {
            await DoPushAsync();
            try
            {
                await Task.Delay(pushInterval, stoppingToken);
            }
            catch (TaskCanceledException)
            {
            }
        }

        // Push the very last metric values before exit
        await DoPushAsync();
        return;

        async Task DoPushAsync()
        {
            try
            {
                await pusher.PushAsync();
            }
            catch (Exception)
            {
                // TODO: report error to DiagnosticSource?
            }
        }
    }
}
