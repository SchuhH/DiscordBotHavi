using DiscordBotHavi.Adapters;
using DiscordBotHavi.Classes;
using DiscordBotHavi.Services;
using DSharpPlus.CommandsNext;
using DSharpPlus.CommandsNext.Attributes;
using DSharpPlus.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace DiscordBotHavi.Commands
{
    public class StatisticalCommands : BaseCommandModule
    {
        private readonly RiotAdapter adapter = new RiotAdapter();
        private readonly StatisticService statisticService = new StatisticService();

        [Command("opgg")]
        [DSharpPlus.CommandsNext.Attributes.Description("Returns op.gg summary of user. This only works for the NA region at the moment.")]
        public async Task GetOPGG(CommandContext ctx, string userName)
        {
            DiscordMessage message = await ctx.Channel.SendMessageAsync("Loading....").ConfigureAwait(false);

            // get summoner encrypted id
            var basicInfo = await adapter.GetSummonerByNameNA(userName);

            // get match history
            var matchHistory = await adapter.GetMatchHistory(basicInfo.accountId);

            // get unique champions played
            var champsAndWinrates = statisticService.GetUniqueChampionsPlayedById(matchHistory, basicInfo.accountId);

            await ctx.Channel.DeleteMessageAsync(message);

            await ctx.Channel.SendMessageAsync("Summoner: " + userName).ConfigureAwait(false);
            await ctx.Channel.SendMessageAsync("Level: " + basicInfo.summonerLevel).ConfigureAwait(false);
            await ctx.Channel.SendMessageAsync("------------------------").ConfigureAwait(false);
            await ctx.Channel.SendMessageAsync("Most played champs in ranked:").ConfigureAwait(false);

            foreach(ChampionMatchDTO championMatch in champsAndWinrates.Values)
            {
                string result = championMatch.winrate.ToString("#0.##%");
                await ctx.Channel.SendMessageAsync("Champ: " + championMatch.championName).ConfigureAwait(false);
                await ctx.Channel.SendMessageAsync("Winrate: " + result).ConfigureAwait(false);
                await ctx.Channel.SendMessageAsync(" -- ").ConfigureAwait(false);
            }
        }
    }
}
