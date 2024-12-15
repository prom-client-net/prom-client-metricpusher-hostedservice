# prom-client-metricpusher-hostedservice

[![ci](https://img.shields.io/github/actions/workflow/status/prom-client-net/prom-client-metricpusher-hostedservice/ci.yml?branch=main&label=ci&logo=github&style=flat-square)](https://github.com/prom-client-net/prom-client-metricpusher-hostedservice/actions/workflows/ci.yml)
[![nuget](https://img.shields.io/nuget/v/Prometheus.Client.MetricPusher.HostedService?logo=nuget&style=flat-square)](https://www.nuget.org/packages/Prometheus.Client.MetricPusher.HostedService)
[![nuget](https://img.shields.io/nuget/dt/Prometheus.Client.MetricPusher.HostedService?logo=nuget&style=flat-square)](https://www.nuget.org/packages/Prometheus.Client.MetricPusher.HostedService)
[![codecov](https://img.shields.io/codecov/c/github/prom-client-net/prom-client-metricpusher-hostedservice?logo=codecov&style=flat-square)](https://app.codecov.io/gh/prom-client-net/prom-client-metricpusher-hostedservice)
[![license](https://img.shields.io/github/license/prom-client-net/prom-client-metricpusher-hostedservice?style=flat-square)](https://github.com/prom-client-net/prom-client-metricpusher-hostedservice/blob/main/LICENSE)

Extension for [Prometheus.Client](https://github.com/prom-client-net/prom-client)

## Install

```sh
dotnet add package Prometheus.Client.MetricPusher.HostedService
```

## Use

[Examples](https://github.com/prom-client-net/prom-examples)

```c#
var metricPusher = new MetricPusher(new MetricPusherOptions
{
    Endpoint = "http://localhost:9091",
    Job = "pushgateway",
    Instance = "instance"
});
```

```c#
services.AddMetricPusherHostedService(metricPusher);
```

## Contribute

Contributions to the package are always welcome!

* Report any bugs or issues you find on the [issue tracker](https://github.com/prom-client-net/prom-client-metricpusher-hostedservice/issues).
* You can grab the source code at the package's [git repository](https://github.com/prom-client-net/prom-client-metricpusher-hostedservice).

## License

All contents of this package are licensed under the [MIT license](https://opensource.org/licenses/MIT).
