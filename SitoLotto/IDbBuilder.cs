using System.Threading;
using System.Threading.Tasks;

namespace SitoLotto
{
    public interface IDbBuilder
    {
        void Dispose();
        bool Equals(object obj);
        int GetHashCode();
        Task StartAsync(CancellationToken stoppingToken);
        Task StopAsync(CancellationToken stoppingToken);
        string ToString();
    }
}