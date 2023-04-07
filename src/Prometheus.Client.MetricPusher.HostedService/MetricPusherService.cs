using System;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Hosting;

namespace Prometheus.Client.MetricPusher.HostedService;

public class MetricPusherService : BackgroundService
{
    private readonly MetricPusher _pusher;
    private readonly TimeSpan _interval;

    public MetricPusherService(MetricPusherOptions pusherOptions, TimeSpan interval)
    {
        _interval = interval;
        _pusher = new MetricPusher(pusherOptions);
    }

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
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
    }

    public override void Dispose()
    {
        base.Dispose();
        _pusher?.Dispose();
    }
}
