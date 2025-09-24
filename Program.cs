using DcYoutubeBot.Commands;
using DcYoutubeBot.Commands.SlashCommands;
using DcYoutubeBot.Config;
using DSharpPlus;
using DSharpPlus.CommandsNext;
using DSharpPlus.EventArgs;
using DSharpPlus.SlashCommands;
using DSharpPlus.VoiceNext;
using System;
using System.Threading.Tasks;

namespace DcYoutubeBot
{
    internal class Program
    {
        public static DiscordClient Client { get; set; }
        public static CommandsNextExtension Commands { get; set; }
        static async Task Main(string[] args)
        {
            //Read configuration
            var config = new BotConfigReader();
            await config.ReadJson();

            //Discord configuration
            var discordClientConfig = new DiscordConfiguration
            {
                Token = config.Token,
                TokenType = TokenType.Bot,
                Intents = DiscordIntents.All,
            };

            var discordCommandsConfig = new CommandsNextConfiguration
            {
                StringPrefixes = new[] { config.Prefix },
                EnableDefaultHelp = false
            };

            Client = new DiscordClient(discordClientConfig);
            Client.Ready += _Client_Ready;

            //Youtube configuration
            Client.UseVoiceNext();

            //Commands configuration
            Commands = Client.UseCommandsNext(discordCommandsConfig);
            Commands.RegisterCommands<BasicCommands>();

            //Slash commands configuration
            var slash = Client.UseSlashCommands();
            slash.RegisterCommands<MusicCommands>();

            //Connect to Discord
            await Client.ConnectAsync();
            await Task.Delay(-1);
        }

        private static Task _Client_Ready(DiscordClient sender, ReadyEventArgs e)
        {
            Console.WriteLine("Bot is ready");
            return Task.CompletedTask;
        }
    }
}
