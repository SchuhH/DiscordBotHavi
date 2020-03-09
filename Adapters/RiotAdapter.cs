using DiscordBotHavi.Classes;
using DiscordBotHavi.Classes.MatchDTO;
using DiscordBotHavi.Services;
using System.Threading.Tasks;

namespace DiscordBotHavi.Adapters
{
    public class RiotAdapter
    {
        private readonly RiotService riotService = new RiotService();
        private readonly string baseUrl = "https://na1.api.riotgames.com";

        public async Task<SummonerDTO> GetSummonerByNameNA(string summonerName)
        {
            return await riotService.GetSummonerByNameNA($"{baseUrl}/lol/summoner/v4/summoners/by-name/{summonerName}");
        }

        public async Task<MatchlistDTO> GetMatchHistory(string encryptedId)
        {
            //TODO: add functionality to allow user to analyze season and queues with parameters. For now its hardcoded.
            //420 is ranked queue; 13 is current ranked season

            return await riotService.GetMatchHistory($"{baseUrl}/lol/match/v4/matchlists/by-account/{encryptedId}?queue=420&season=13");
        }

        public async Task<MatchDto> GetMatchById(string gameId)
        {
            return await riotService.GetMatchResult($"{baseUrl}/lol/match/v4/matches/{gameId}");
        }
    }
}
