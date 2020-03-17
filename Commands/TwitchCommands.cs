using DiscordBotHavi.Adapters;
using DiscordBotHavi.Classes.TwitchUsersDTO;
using DSharpPlus.CommandsNext;
using DSharpPlus.CommandsNext.Attributes;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace DiscordBotHavi.Commands
{
    public class TwitchCommands : BaseCommandModule
    {
        private readonly TwitchAdapter twitchAdapter = new TwitchAdapter();

        [Command("twitch")]
        [DSharpPlus.CommandsNext.Attributes.Description("Checks if channel is live based on username.")]
        public async Task IsLive(CommandContext ctx, string twitchUsername)
        {
            
            string[] userList = { twitchUsername };

            // get twitch user's id
            var twitchUser = await twitchAdapter.GetTwitchUsers(userList);

            if(twitchUser != null && twitchUser.Total > 0)
            {
                bool isLive = await twitchAdapter.CheckIfStreamIsLive(twitchUser.Users[0].Id.ToString());

                if (isLive)
                {
                    await ctx.Channel.SendMessageAsync(twitchUser.Users[0].Name + " is live!").ConfigureAwait(false);
                }
                else
                {
                    await ctx.Channel.SendMessageAsync($"{twitchUsername} is not live!").ConfigureAwait(false);
                }
            }
            else
            {
                await ctx.Channel.SendMessageAsync($"{twitchUsername} does not exist!").ConfigureAwait(false);
            }
            
        }

        [Command("twitch")]
        [DSharpPlus.CommandsNext.Attributes.Description("Checks if channel is live based on username.")]
        public async Task Track(CommandContext ctx, string trackCheck, string twitchUsername)
        {

            if (!trackCheck.Equals("track"))
            {
                return;
            }

            string[] userList = { twitchUsername };

            // get twitch user's id
            var twitchUser = await twitchAdapter.GetTwitchUsers(userList);

            if (twitchUser != null && twitchUser.Total > 0)
            {
                await IsLiveLoop(ctx, twitchUser);     
            }
            else
            {
                await ctx.Channel.SendMessageAsync($"{twitchUsername} does not exist!").ConfigureAwait(false);
            }

        }

        private async Task IsLiveLoop(CommandContext ctx, TwitchUsersDTO twitchUser)
        {
            string twitchPath = "https://www.twitch.tv/";
            bool isLive = false;

            while (!isLive)
            {
                Thread.Sleep(30000);

                isLive = await twitchAdapter.CheckIfStreamIsLive(twitchUser.Users[0].Id.ToString());

                if (isLive)
                {
                    StringBuilder builder = new StringBuilder();

                    builder.AppendLine(twitchUser.Users[0].Name + " is live!");
                    builder.AppendLine($"{twitchPath}{twitchUser.Users[0].Name}");

                    await ctx.Channel.SendMessageAsync(builder.ToString()).ConfigureAwait(false);
                }
            }
            while (isLive)
            {
                Thread.Sleep(30000);

                isLive = await twitchAdapter.CheckIfStreamIsLive(twitchUser.Users[0].Id.ToString());

                if (!isLive)
                {
                    await IsLiveLoop(ctx, twitchUser);
                }
            }
        }
    }
}
