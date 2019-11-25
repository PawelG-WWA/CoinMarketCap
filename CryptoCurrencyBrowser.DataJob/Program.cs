using System;

namespace CryptoCurrencyBrowser.DataJob
{
    class Program
    {
        static void Main(string[] args)
        {
            Host.Bootstrap();

            Host.RunJobs();

            Console.WriteLine("Press any key to prevent all jobs from running");

            Console.ReadKey();
        }
    }
}
