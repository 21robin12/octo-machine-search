using Newtonsoft.Json;
using OctoMachineSearch.Config;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
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
            RunAsync(args[0]).Wait();
        }

        static async Task RunAsync(string searchText)
        {
            var config = ConfigManager.Config;
            var url = $"{config.OctoUrlBase}/api/machines/all?apikey={config.ApiKey}";
            var responseString = await client.GetStringAsync(url);
            var result = JsonConvert.DeserializeObject<IList<Machine>>(responseString);
            var matching = result.Where(x => x.Roles.Any(r => r.ToLower().Contains(searchText))).ToList();

            if (matching.Count == 0)
            {
                Console.WriteLine("No results");
            }
            else
            {
                PrintResults(matching.ToList(), searchText);
            }            
        }

        static void PrintResults(IList<Machine> results, string searchText)
        {
            Console.WriteLine();
            var longestName = results.OrderByDescending(x => x.Name.Length).FirstOrDefault().Name.Length;
            foreach(var result in results)
            {
                Console.WriteLine($"{result.Name.PadRight(longestName + 5)}{string.Join(" | ", result.Roles.Where(r => r.ToLower().Contains(searchText)))}");
            }
        }
    }

    class Machine
    {
        public string Name { get; set; }
        public IList<string> EnvironmentIds { get; set; }
        public IList<string> Roles { get; set; }
    }
}