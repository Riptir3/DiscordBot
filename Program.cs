using System;
using System.Threading.Tasks;
using DcYoutubeBot.Commands;
using DcYoutubeBot.Commands.SlashCommands;
using DcYoutubeBot.Config;
using DSharpPlus;
using DSharpPlus.CommandsNext;
using DSharpPlus.EventArgs;
using DSharpPlus.SlashCommands;

namespace DiscordBot
{
    internal class Program
    {
        public static DiscordClient Client {  get; private set; }
        public static CommandsNextExtension Commands { get; private set; }

        static async Task Main(string[] args)
        {
            //Read config configuration
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
            // Event handlers for the Client
            Client.Ready += _Client_Ready;

            //Commands configuration
            Commands = Client.UseCommandsNext(discordCommandsConfig);
            Commands.RegisterCommands<BasicCommands>();

            //Slash commands configuration
            var slash = Client.UseSlashCommands();
            slash.RegisterCommands<AdminCommands>(); 

            //Connect to Discord
            await Client.ConnectAsync();
            await Task.Delay(-1);
        }

        private static Task _Client_Ready(DiscordClient sender, ReadyEventArgs e) //Event handler for when the client is ready
        {
            Console.WriteLine("Bot is ready");
            return Task.CompletedTask;
        }
    }
}
