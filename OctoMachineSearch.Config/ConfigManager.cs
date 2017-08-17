using Newtonsoft.Json;
using System.IO;
using System.Reflection;

namespace OctoMachineSearch.Config
{
    public static class ConfigManager
    {
        private static Config _config;
        public static Config Config
        {
            get
            {

                if (_config == null)
                {
                    var configPath = ConfigPath();
                    if (!File.Exists(configPath))
                    {
                        throw new FileNotFoundException($"No config file found at {configPath}");
                    }

                    var text = File.ReadAllText(configPath);
                    _config = JsonConvert.DeserializeObject<Config>(text);
                }

                return _config;
            }
        }

        private static string ConfigPath()
        {
            var assembley = Assembly.GetEntryAssembly();
            var directory = assembley.Location.Substring(0, assembley.Location.LastIndexOf('\\'));
            return Path.Combine(directory, "config.json");
        }
    }
}
