using DiscordBotHavi.Adapters;
using DiscordBotHavi.Classes;
using DiscordBotHavi.Services;
using DSharpPlus.CommandsNext;
using DSharpPlus.CommandsNext.Attributes;
using DSharpPlus.Entities;
using DSharpPlus.Interactivity;
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


            StringBuilder stringBuilder = new StringBuilder();

            stringBuilder.AppendLine(">>> **Summoner** : " + userName);
            stringBuilder.AppendLine("**Level** : " + basicInfo.summonerLevel);
            stringBuilder.AppendLine("**Most played champs in ranked:**");
            stringBuilder.AppendLine("------------------------");
           

            foreach(ChampionMatchDTO championMatch in champsAndWinrates.Values)
            {
                string result = championMatch.winrate.ToString("#0.##%");
                stringBuilder.AppendLine("**Champ** : " + championMatch.championName);
                stringBuilder.AppendLine("**Winrate** : " + result);
                stringBuilder.AppendLine(" --");
            }
            await ctx.Channel.SendMessageAsync(stringBuilder.ToString()).ConfigureAwait(false);


        }
    }
}
