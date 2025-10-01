using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using DcYoutubeBot.Commands;
using DcYoutubeBot.Commands.SlashCommands;
using DcYoutubeBot.Config;
using DcYoutubeBot.Logger;
using DSharpPlus;
using DSharpPlus.CommandsNext;
using DSharpPlus.Entities;
using DSharpPlus.EventArgs;
using DSharpPlus.SlashCommands;
using DSharpPlus.SlashCommands.EventArgs;

namespace DiscordBot
{
    internal class Program
    {
        public static DiscordClient Client {  get; private set; }
        public static CommandsNextExtension Commands { get; private set; }

        private static readonly SlashLogger logger = new SlashLogger();

        public static Config config = new Config();
        static async Task Main(string[] args)
        {
             config = await ConfigLoader.LoadAsync();

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
            Client.MessageCreated += Client_MessageCreated;
            Client.GuildMemberAdded += Client_GuildMemberAdded;

            //Commands configuration
            Commands = Client.UseCommandsNext(discordCommandsConfig);
            Commands.RegisterCommands<BasicCommands>();

            //Slash commands configuration
            var slash = Client.UseSlashCommands();
            slash.RegisterCommands<AdminCommands>();
            slash.SlashCommandExecuted += Slash_SlashCommandExecuted;

            //Connect to Discord
            await Client.ConnectAsync();
            await Task.Delay(-1);
        }

        private static async Task Client_GuildMemberAdded(DiscordClient sender, GuildMemberAddEventArgs args) //Event handler for when a new member joins the guild | Welcome message
        {
            var embed = new DiscordEmbedBuilder
            {
                Title = "Welcome to the server!",
                Description = $"Hello {args.Member.Mention}, welcome to {args.Guild.Name}! We're glad to have you here.",
                Color = DiscordColor.Blurple,
                Thumbnail = new DiscordEmbedBuilder.EmbedThumbnail { Url = args.Member.AvatarUrl }
            };
            var channel = Client.GetChannelAsync(config.WelcomeChannelId); 
            await channel.Result.SendMessageAsync(embed: embed);
            return;
        }

        private static Task Slash_SlashCommandExecuted(SlashCommandsExtension sender, SlashCommandExecutedEventArgs args) //Event handler for when a slash command is executed | Logger
        {
            return logger.LogCommand(args);
        }

        private static Task Client_MessageCreated(DiscordClient sender, MessageCreateEventArgs args) //Event handler for when a message is created
        {
            foreach (var word in new List<string> { "Fuck"})
            {
                if (args.Message.Content.ToLower().Contains(word))
                {
                    args.Message.DeleteAsync();
                    args.Channel.SendMessageAsync($"{args.Message.Author.Mention}, please avoid using inappropriate language.");
                    break;
                }
            }
            return Task.CompletedTask;
        }


        private static Task _Client_Ready(DiscordClient sender, ReadyEventArgs e) //Event handler for when the client is ready
        {
            Console.WriteLine("Bot is ready");
            return Task.CompletedTask;
        }
    }
}
