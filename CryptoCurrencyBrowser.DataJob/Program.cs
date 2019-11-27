using System;

namespace CryptoCurrencyBrowser.DataJob
{
    class Program
    {
        static void Main(string[] args)
        {
            Host.Bootstrap();

            if (Host.IsBootstrapSuccessful)
            {
                Host.StartJobs();
            }

            Console.ReadLine();
        }
    }
}
