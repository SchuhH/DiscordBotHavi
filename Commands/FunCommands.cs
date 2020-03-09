using DSharpPlus.CommandsNext;
using DSharpPlus.CommandsNext.Attributes;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;
using System.Threading.Tasks;

namespace DiscordBotHavi.Commands
{
    public class FunCommands : BaseCommandModule
    {
        [Command("ping")]
        [DSharpPlus.CommandsNext.Attributes.Description("Returns pong and mentions the user who dares ping the mega bot.")]
        public async Task Ping(CommandContext ctx)
        {
            await ctx.Channel.SendMessageAsync(ctx.User.Mention +  " pong").ConfigureAwait(false);
        }
        
    }
}
