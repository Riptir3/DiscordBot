using DSharpPlus.CommandsNext;
using DSharpPlus.CommandsNext.Attributes;
using System.Threading.Tasks;

namespace DcYoutubeBot.Commands
{
    public class BasicCommands : BaseCommandModule
    {
        [Command("help")]
        public async Task TestCommand(CommandContext ctx)
        {
            await ctx.RespondAsync($"Test command executed successfully by {ctx.User.Username}");
        }
    }
}
