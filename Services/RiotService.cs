using DiscordBotHavi.Classes;
using DiscordBotHavi.Structs;
using Newtonsoft.Json;
using System.IO;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace DiscordBotHavi.Services
{
    public class RiotService
    {
        private HttpClient client = new HttpClient();
        
        public RiotService()
        {
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("X-Riot-Token", GetAuthToken().Result);
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
