using libraryLotto;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.SpaServices.AngularCli;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace LottoWeb
{
  public class DbBuilder : IHostedService, IDisposable
  {
    private int executionCount = 0;
    private readonly ILogger<DbBuilder> _logger;
    private Timer _timer;
    private int lastDay = DateTime.Now.Day-1;
    private batchDownloadData lottoData = new batchDownloadData();

    public DbBuilder(ILogger<DbBuilder> logger)
    {
      _logger = logger;
    }

    public Task StartAsync(CancellationToken stoppingToken)
    {
      _logger.LogInformation("Timed Hosted Service is running.");
      _timer = new Timer(DoWork, null, TimeSpan.Zero, TimeSpan.FromSeconds(60*1000));
      return Task.CompletedTask;
    }

    private void DoWork(object state)
    {
      if (lastDay != DateTime.Now.Day )
      {
        lastDay = DateTime.Now.Day;
        lottoData.downloadAllLotto();
        _logger.LogInformation("Db Updated");
      }

    }

    public Task StopAsync(CancellationToken stoppingToken)
    {
      _logger.LogInformation("Timed Hosted Service is stopping.");
      return Task.CompletedTask;
    }

    public void Dispose()
    {
      _timer?.Dispose();
    }
  }
}
