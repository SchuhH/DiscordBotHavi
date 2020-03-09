using DiscordBotHavi.Classes;
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
    }
}
