using libraryLotto;
using lbControlWebPages;
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
using Microsoft.AspNetCore.SignalR.Protocol;
using System.Net.NetworkInformation;
using System.Text;
using System.IO;
using System.Net;

namespace SitoLotto
{
    public class DbBuilder : IHostedService, IDisposable
    {
        private readonly ILogger<DbBuilder> _logger;
        private Timer _timer;
        private int lastDay = DateTime.Now.Day - 1;
        private batchDownloadData lottoData = new batchDownloadData();

        public DbBuilder(ILogger<DbBuilder> logger)
        {
            _logger = logger;
        }


        public Task StartAsync(CancellationToken stoppingToken)
        {
            _timer = new Timer(DoWork, null, TimeSpan.Zero, TimeSpan.FromSeconds(60));
            return Task.CompletedTask;
        }

        private void DoWork(object state)
        {
            if (lastDay != DateTime.Now.Day)
            {
                lastDay = DateTime.Now.Day;
                lottoData.downloadAllLotto();
                DbManagement.DsSiteLoad();
            }
            PingMe();//if i don't do it the sise go down

        }


        public Task StopAsync(CancellationToken stoppingToken)
        {
            return Task.CompletedTask;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
        protected virtual void Dispose(bool disposing)
        {
            if (disposing)
            {
                _timer.Dispose();
            }
        }

        public override bool Equals(object obj)
        {
            return base.Equals(obj);
        }

        public override int GetHashCode()
        {
            return base.GetHashCode();
        }

        public override string ToString()
        {
            return base.ToString();
        }


        private static void PingMe()
        {
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(new Uri("https://luca-site-test.herokuapp.com/api/Lotto/active"));
            request.AutomaticDecompression = DecompressionMethods.GZip | DecompressionMethods.Deflate;

            using (HttpWebResponse response = (HttpWebResponse)request.GetResponse())
            using (Stream stream = response.GetResponseStream())
            using (var reader = new StreamReader(stream))
            {
                Console.WriteLine("PingMe"+reader.ReadToEnd());
            }


        }
    }
}
