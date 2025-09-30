using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using DSharpPlus.SlashCommands.EventArgs;
using YamlDotNet.Serialization;
using YamlDotNet.Serialization.NamingConventions;

namespace DcYoutubeBot.Logger
{
    public class SlashLogger
    {
        private readonly string _logFile = "slash_logs.yaml";
        private readonly ISerializer _serializer;
        private readonly IDeserializer _deserializer;

        public SlashLogger()
        {
            _serializer = new SerializerBuilder()
                .WithNamingConvention(CamelCaseNamingConvention.Instance)
                .Build();

            _deserializer = new DeserializerBuilder()
                .WithNamingConvention(CamelCaseNamingConvention.Instance)
                .IgnoreUnmatchedProperties()
                .Build();
        }

        public Task LogCommand(SlashCommandExecutedEventArgs e)
        {
            var entry = new SlashLogEntry
            {
                Timestamp = DateTime.UtcNow,
                Command = e.Context.Interaction.Data.Name,
                User = $"{e.Context.User.Username}#{e.Context.User.Discriminator}",
                Guild = e.Context.Guild?.Name ?? "DM",
                Parameters = new Dictionary<string, object>()
            };

            if (e.Context.Interaction.Data.Options != null)
            {
                foreach (var option in e.Context.Interaction.Data.Options)
                    entry.Parameters[option.Name] = option.Value;
            }

            List<SlashLogEntry> logEntries = new List<SlashLogEntry>();
            if (File.Exists(_logFile))
            {
                var existingYaml = File.ReadAllText(_logFile);
                if (!string.IsNullOrWhiteSpace(existingYaml))
                {
                    try
                    {
                        logEntries = _deserializer.Deserialize<List<SlashLogEntry>>(existingYaml)
                                     ?? new List<SlashLogEntry>();
                    }
                    catch
                    {
                        logEntries = new List<SlashLogEntry>();
                    }
                }
            }

            logEntries.Add(entry);

            var yaml = _serializer.Serialize(logEntries);
            File.WriteAllText(_logFile, yaml);

            return Task.CompletedTask;
        }
    } 

    internal sealed class SlashLogEntry //Log entry structure
    {
        public DateTime Timestamp { get; set; }
        public string Command { get; set; }
        public string User { get; set; }
        public string Guild { get; set; }
        public Dictionary<string, object> Parameters { get; set; }
    }
}

