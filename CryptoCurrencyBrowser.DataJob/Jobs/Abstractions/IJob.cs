using System.Threading.Tasks;

namespace CryptoCurrencyBrowser.DataJob.Jobs.Abstractions
{
    public interface IJob
    { 
        Task DoWork();
    }
}
