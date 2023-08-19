using System;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Hosting;

namespace Prometheus.Client.MetricPusher.HostedService;

public class MetricPusherService : BackgroundService
{
    private readonly IMetricPusher _pusher;
    private readonly TimeSpan _interval;

    public MetricPusherService(IMetricPusher pusher)
        : this(pusher, TimeSpan.FromMilliseconds(1000))
    {
    }

    public MetricPusherService(IMetricPusher pusher, TimeSpan interval)
    {
        _pusher = pusher;
        _interval = interval;
    }

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        while (!stoppingToken.IsCancellationRequested)
        {
            await DoPushAsync();
            try
            {
                await Task.Delay(_interval, stoppingToken);
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
                await _pusher.PushAsync();
            }
            catch (Exception)
            {
                // TODO: report error to DiagnosticSource?
            }
        }
    }
}
