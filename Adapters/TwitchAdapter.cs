using DiscordBotHavi.Classes.TwitchUsersDTO;
using DiscordBotHavi.Services;
using DSharpPlus.CommandsNext;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace DiscordBotHavi.Adapters
{
    public class TwitchAdapter
    {
        
        private readonly string baseUrl = "https://api.twitch.tv/kraken";
        private readonly TwitchService twitchService = new TwitchService();


        public async Task<TwitchUsersDTO> GetTwitchUsers(string[] userList)
        {
            string path = ($"{baseUrl}/users?login=");
            bool firstUserAdded = false;


            foreach(string user in userList)
            {
                if (!firstUserAdded)
                {
                    path += user;
                    firstUserAdded = !firstUserAdded;
                }
                else
                {
                    path += ($",{user}");
                }
            }

            return await twitchService.GetTwitchUsers(path);
        }

        public async Task<bool> CheckIfStreamIsLive(string userChannelId)
        {
            return await twitchService.CheckIfStreamIsLive($"{baseUrl}/streams/{userChannelId}");
        }


    }
}
