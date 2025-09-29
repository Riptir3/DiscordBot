using DSharpPlus.CommandsNext.Attributes;
using DSharpPlus.Entities;
using DSharpPlus.SlashCommands;
using System;
using System.Linq;
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
                          "/timeout - Restrict user writing for a period of time\n" +
                          "/kick - Kick a user\n" +
                          "/banlist - Get all banned user\n" +
                          "/ban - Ban a user\n" +
                          "/unban - Unban a user"
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

        [SlashCommand("unban", "Unban selected user.")]
        [RequireRoles(RoleCheckMode.SpecifiedOnly, "Admin")]
        public async Task Unban(InteractionContext ctx, [Option("Id", "Id of banned user.")] string userId)
        {
            await ctx.DeferAsync();

            var userIdLong = ulong.Parse(userId);

            var user = await ctx.Client.GetUserAsync(userIdLong);
            if (user == null)
            {
                await ctx.EditResponseAsync(new DiscordWebhookBuilder
                {
                    Content = $"User with ID {userId} not found.\n" +
                              $"executed by {ctx.User.Username}"
                });
                return;
            }

            var guild = await ctx.Client.GetGuildAsync(ctx.Guild.Id);

            await guild.UnbanMemberAsync(userIdLong);
            await ctx.EditResponseAsync(new DiscordWebhookBuilder
            {
                Content = $"{user.Username} has been unbanned.\n" +
                          $"executed by {ctx.User.Username}"
            });
        }

        [SlashCommand("banlist", "Get all banned user.")]
        [RequireRoles(RoleCheckMode.SpecifiedOnly, "Admin")]
        public async Task BanListAsync(InteractionContext ctx)
        {
            var bans = await ctx.Guild.GetBansAsync();

            if (bans.Count == 0)
            {
                await ctx.CreateResponseAsync("✅ No banned users.");
                return;
            }

            // Összerakjuk a listát
            var description = string.Join("\n", bans.Select(b =>
                $"👤 **{b.User.Username}#{b.User.Discriminator}** – ID: `{b.User.Id}`"));

            // Embedben küldjük vissza
            var embed = new DiscordEmbedBuilder()
                .WithTitle("🚫 Banned Users")
                .WithDescription(description)
                .WithColor(DiscordColor.Red);

            await ctx.CreateResponseAsync(embed);
        }
    }
}
