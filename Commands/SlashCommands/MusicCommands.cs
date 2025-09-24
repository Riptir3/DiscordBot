using DSharpPlus.Entities;
using DSharpPlus.SlashCommands;
using DSharpPlus.VoiceNext;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using YoutubeExplode;

namespace DcYoutubeBot.Commands.SlashCommands
{
    public class MusicCommands : ApplicationCommandModule
    {
        [SlashCommand("play", "Lejátszik egy YouTube videót a voice csatornában")]
        public async Task PlayAsync(InteractionContext ctx, [Option("query", "YouTube link vagy keresés")] string query)
        {
            await Task.CompletedTask;
        }
    }
}
