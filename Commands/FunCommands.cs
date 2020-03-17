using DSharpPlus.CommandsNext;
using DSharpPlus.CommandsNext.Attributes;
using DSharpPlus.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.InteropServices.ComTypes;
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

        [Command("simp")]
        [DSharpPlus.CommandsNext.Attributes.Description("Checks the level of simpness for a given user.")]
        public async Task SimpCheck(CommandContext ctx, string username)
        {
            Random random = new Random();
            var next = random.NextDouble();

            double simpLevel = 0.01 + (next * (1 - 0.01));

            StringBuilder builder = new StringBuilder();
            builder.AppendLine(">>> **simp detecting machine**");
            builder.AppendLine($"{username} simp level");

            if ((username.Equals("<@!365172319972491284>") || simpLevel > .90) && !ctx.User.Username.Equals("tyrapple"))
            {
                builder.AppendLine("100% a simp lord. This guy will simp anything!");
            }
            else
            {
                builder.AppendLine(simpLevel.ToString("#0.##%"));
            }


            await ctx.Channel.SendMessageAsync(builder.ToString()).ConfigureAwait(false);
        }

        [Command("simp")]
        [DSharpPlus.CommandsNext.Attributes.Description("Checks the level of simpness for a given user.")]
        public async Task SimpCheck(CommandContext ctx)
        {
            Random random = new Random();
            var next = random.NextDouble();

            double simpLevel = 0.01 + (next * (1 - 0.01));

            StringBuilder builder = new StringBuilder();
            builder.AppendLine(">>> **simp detecting machine**");
            builder.AppendLine(ctx.User.Username+ " simp level");

            if(ctx.User.Id == 365172319972491284 || simpLevel > .90)
            {
                builder.AppendLine("100% a simp lord. This guy will simp anything!");
            }
            else
            {
                builder.AppendLine(simpLevel.ToString("#0.##%"));
            }
            

            await ctx.Channel.SendMessageAsync(builder.ToString()).ConfigureAwait(false);
        }
    }
}
