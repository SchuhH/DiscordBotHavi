using DSharpPlus.CommandsNext;
using DSharpPlus.CommandsNext.Attributes;
using DSharpPlus.Entities;
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

        [Command("xtylerd")]
        [DSharpPlus.CommandsNext.Attributes.Description("Flames Tyler.")]
        public async Task FlameTyler(CommandContext ctx)
        {
            await ctx.Channel.SendMessageAsync("<@365172319972491284> u r xd").ConfigureAwait(false);
        }
    }
}
