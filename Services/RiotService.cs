using DiscordBotHavi.Classes;
using DiscordBotHavi.Classes.MatchDTO;
using DiscordBotHavi.Classes.RankedEntryDTO;
using DiscordBotHavi.Structs;
using Newtonsoft.Json;
using System.IO;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace DiscordBotHavi.Services
{
    public class RiotService
    {
        private HttpClient client = new HttpClient();

        public RiotService()
        {
            client.DefaultRequestHeaders.Add("X-Riot-Token", GetAuthToken().Result);
        }

        public async Task<SummonerDTO> GetSummonerByNameNA(string path)
        {
            using (HttpResponseMessage response = await client.GetAsync(path).ConfigureAwait(false))
            {
                if (response.IsSuccessStatusCode)
                {
                    string jsonResponse = await response.Content.ReadAsStringAsync().ConfigureAwait(false);

                    SummonerDTO summoner = JsonConvert.DeserializeObject<SummonerDTO>(jsonResponse);

                    return summoner;
                }
            }

            return null;

        }

        public async Task<RankedEntryDto[]> GetSummonerRankedEntriesById(string path)
        {
            using (HttpResponseMessage response = await client.GetAsync(path).ConfigureAwait(false))
            {
                if (response.IsSuccessStatusCode)
                {
                    string jsonResponse = await response.Content.ReadAsStringAsync().ConfigureAwait(false);

                    RankedEntryDto[] rankedEntryDto = JsonConvert.DeserializeObject<RankedEntryDto[]>(jsonResponse);

                    return rankedEntryDto;
                }
            }

            return null;
        }

        public async Task<MatchlistDTO> GetMatchHistory(string path)
        {
            using (HttpResponseMessage response = await client.GetAsync(path).ConfigureAwait(false))
            {
                if (response.IsSuccessStatusCode)
                {
                    string jsonResponse = await response.Content.ReadAsStringAsync().ConfigureAwait(false);

                    MatchlistDTO matchlist = JsonConvert.DeserializeObject<MatchlistDTO>(jsonResponse);

                    return matchlist;
                }
            }

            return null;
        }

        public async Task<MatchDto> GetMatchResult(string path)
        {
            using (HttpResponseMessage response = await client.GetAsync(path).ConfigureAwait(false))
            {
                if (response.IsSuccessStatusCode)
                {
                    string jsonResponse = await response.Content.ReadAsStringAsync().ConfigureAwait(false);

                    var matchDto = MatchDto.FromJson(jsonResponse);

                    return matchDto;
                }
            }

            return null;
        }

        //public async Task<SummonerDTO> GetSummonerByNameEU(string summonerName)
        //{

        //}

        private async Task<string> GetAuthToken()
        {
            string json = string.Empty;

            using (var fs = File.OpenRead("riotConfig.json"))
            {
                using var sr = new StreamReader(fs, new UTF8Encoding(false));
                json = await sr.ReadToEndAsync().ConfigureAwait(false);
            }

            var configJson = JsonConvert.DeserializeObject<RiotConfigJson>(json);

            return configJson.Key;
        }
    }
}
