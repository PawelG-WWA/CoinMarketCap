using System;
using System.Threading.Tasks;

namespace CryptoCurrencyBrowser.DataJob
{
    class Program
    {
        static void Main(string[] args)
        {
            Host.Bootstrap();

            if (Host.IsBootstrapSuccessful)
            {
                // So I will know that exceptions occured
                Host.StartJobs().GetAwaiter().GetResult();
            }

            Console.ReadLine();
        }
    }
}
