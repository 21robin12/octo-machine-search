using OctoMachineSearch.Config;
using System;
using System.IO;
using System.Net.Http;
using System.Reflection;
using System.Threading.Tasks;

namespace OctoMachineSearch
{
    class Program
    {
        private static readonly HttpClient client = new HttpClient();

        static void Main(string[] args)
        {
            RunAsync().Wait();
        }

        static async Task RunAsync()
        {
            var config = ConfigManager.Config;
            var url = $"{config.OctoUrlBase}/api/machines/all?apikey={config.ApiKey}";
            var responseString = await client.GetStringAsync(url);
            Console.WriteLine("Hello World!");
        }
    }
}