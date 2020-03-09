using DSharpPlus.CommandsNext;
using DSharpPlus.CommandsNext.Attributes;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace DiscordBotHavi.Commands
{
    public class StatisticalCommands : BaseCommandModule
    {
        [Command("opgg")]
        [DSharpPlus.CommandsNext.Attributes.Description("Returns op.gg summary of user. This only works for the NA region at the moment.")]
        public async Task GetRankAndWinrate(CommandContext ctx, string userName)
        {
            
        }
    }
}
