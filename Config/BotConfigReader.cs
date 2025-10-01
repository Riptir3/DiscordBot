using System;
using System.Collections.Generic;
using System.IO;
using System.Text.Json;
using System.Threading.Tasks;

    public class Config
    {
        public string Token { get; set; }
        public string Prefix { get; set; }
        public List<string> forbiddenWords { get; set; }
        public string LogFilePath { get; set; }
        public ulong SafeChannelId { get; set; }
        public ulong WelcomeChannelId { get; set; }
        public ApiSettings Apis { get; set; }
    }

    public class ApiSettings
    {
        public string JokeApi { get; set; }
        public string WeatherApiUrl { get; set; }
        public string GeoApiUrl { get; set; }
        public string MovieApiKey { get; set; }
        public string MovieApiUrl { get; set; }
    }

    public static class ConfigLoader
    {
        public static async Task<Config> LoadAsync(string path = "ClientConfig.json")
        {
            if (!File.Exists(path))
                throw new FileNotFoundException($"No config file: {path}");

            var options = new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            };

            using (var fs = File.OpenRead(path))
            {
                var config = await JsonSerializer.DeserializeAsync<Config>(fs, options);
                return config ?? throw new Exception("Can't load config file!");
            }
        }
    }


