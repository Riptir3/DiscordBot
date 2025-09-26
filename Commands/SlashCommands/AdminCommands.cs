using DSharpPlus.CommandsNext.Attributes;
using DSharpPlus.Entities;
using DSharpPlus.SlashCommands;
using System;
using System.Threading.Tasks;

namespace DcYoutubeBot.Commands.SlashCommands
{
    public class AdminCommands : ApplicationCommandModule
    {
        [SlashCommand("help", "Display admin only commands")]
        [RequireRoles(RoleCheckMode.SpecifiedOnly, "Admin")]

        public async Task HelpCommand(InteractionContext ctx)
        {
            await ctx.DeferAsync();

            await ctx.EditResponseAsync(new DiscordWebhookBuilder
            {
                Content = "Admin Commands:\n" +
                          "/help - Display admin only commands\n" +
                          "/timeout - Ban a user\n" +
                          "/kick - Unban a user\n" +
                          "/ban - Mute a user\n" +
                          "/unban - Unmute a user\n"
            });
        }

        [SlashCommand("timeout", "Timeout selected user.")]
        [RequireRoles(RoleCheckMode.SpecifiedOnly, "Admin")]

        public async Task Timeout(InteractionContext ctx, [Option("User","Select user.")] DiscordUser user,
                                                          [Option("Time", "Add time in seconds.")] long duration,
                                                          [Option("Reason", "Add reason.")] string reason)
        {
            await ctx.DeferAsync();

            var discordmember = (DiscordMember)user;
            var timeoutDuration = DateTime.Now + TimeSpan.FromSeconds(duration);
            await discordmember.TimeoutAsync(timeoutDuration,reason);

            await ctx.EditResponseAsync(new DiscordWebhookBuilder
            {
                Content = $"{user.Username} has been timed out for {duration} seconds for: {reason}\n" +
                          $"executed by {ctx.User.Username}"
            });
        }

        [SlashCommand("kick", "Kick selected user.")]
        [RequireRoles(RoleCheckMode.SpecifiedOnly, "Admin")]

        public async Task Kick(InteractionContext ctx, [Option("User", "Select user.")] DiscordUser user)
        {
            await ctx.DeferAsync();

            var discordmember = (DiscordMember)user;
            await discordmember.RemoveAsync($"Kicked by admin: {ctx.User.Username}");

            await ctx.EditResponseAsync(new DiscordWebhookBuilder
            {
                Content = $"{user.Username} has been kicked out.\n" +
                          $"executed by {ctx.User.Username}"
            });
        }

        [SlashCommand("ban", "Ban selected user.")]
        [RequireRoles(RoleCheckMode.SpecifiedOnly, "Admin")]

        public async Task Ban(InteractionContext ctx, [Option("User", "Select user.")]DiscordUser user,
                                                      [Option("Reason", "Reason.")] string reason)
        {
            await ctx.DeferAsync();

            var discordmember = (DiscordMember)user;
            await discordmember.BanAsync(default,reason);

            await ctx.EditResponseAsync(new DiscordWebhookBuilder
            {
                Content = $"{user.Username} has been banned.\n" +
                          $"executed by {ctx.User.Username}"
            });
        }
    }
}
