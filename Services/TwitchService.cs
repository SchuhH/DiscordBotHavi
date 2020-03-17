using DiscordBotHavi.Adapters;
using DiscordBotHavi.Classes.TwitchStreamDTO;
using DiscordBotHavi.Classes.TwitchUsersDTO;
using DiscordBotHavi.Structs;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace DiscordBotHavi.Services
{
    public class TwitchService
    {
        private HttpClient client = new HttpClient();

        public TwitchService()
        {
            client.DefaultRequestHeaders.Add("Accept", "application/vnd.twitchtv.v5+json");
            client.DefaultRequestHeaders.Add("Client-ID", GetClientId().Result);
        }
        
        public async Task<TwitchUsersDTO> GetTwitchUsers(string path)
        {
            using (HttpResponseMessage response = await client.GetAsync(path).ConfigureAwait(false))
            {
                if (response.IsSuccessStatusCode)
                {
                    string jsonResponse = await response.Content.ReadAsStringAsync().ConfigureAwait(false);

                    TwitchUsersDTO twitchUsers = JsonConvert.DeserializeObject<TwitchUsersDTO>(jsonResponse);

                    return twitchUsers;
                }
            }

            return null;

        }

        public async Task<bool> CheckIfStreamIsLive(string path)
        {
            using (HttpResponseMessage response = await client.GetAsync(path).ConfigureAwait(false))
            {
                if (response.IsSuccessStatusCode)
                {
                    string jsonResponse = await response.Content.ReadAsStringAsync().ConfigureAwait(false);

                    TwitchStreamDto twitchUser = JsonConvert.DeserializeObject<TwitchStreamDto>(jsonResponse);

                    if (twitchUser.Stream != null)
                    {
                        return true;
                    }                 
                }
            }
            return false;
        }


        private async Task<string> GetClientId()
        {
            string json = string.Empty;

            using (var fs = File.OpenRead("twitchConfig.json"))
            {
                using var sr = new StreamReader(fs, new UTF8Encoding(false));
                json = await sr.ReadToEndAsync().ConfigureAwait(false);
            }

            var configJson = JsonConvert.DeserializeObject<TwitchConfigJson>(json);

            return configJson.ClientId;
        }

        private async Task<string> GetClientSecret()
        {
            string json = string.Empty;

            using (var fs = File.OpenRead("twitchConfig.json"))
            {
                using var sr = new StreamReader(fs, new UTF8Encoding(false));
                json = await sr.ReadToEndAsync().ConfigureAwait(false);
            }

            var configJson = JsonConvert.DeserializeObject<TwitchConfigJson>(json);

            return configJson.ClientSecret;
        }
    }
}
