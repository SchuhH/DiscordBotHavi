using DiscordBotHavi.Commands;
using DiscordBotHavi.Structs;
using DSharpPlus;
using DSharpPlus.CommandsNext;
using DSharpPlus.EventArgs;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading.Tasks;

namespace DiscordBotHavi.Classes
{
    public class DiscordBot
    {
        public static Welcome champs;

        public DiscordClient Client { get; set; }
        public CommandsNextExtension Commands { get; set; }


        // Starts bot
        public async Task RunAsync()
        {
            await GenerateChampionsFromJson();

            var json = string.Empty;

            using (var fs = File.OpenRead("discordConfig.json"))
            {
                using var sr = new StreamReader(fs, new UTF8Encoding(false));
                json = await sr.ReadToEndAsync().ConfigureAwait(false);
            }

            var configJson = JsonConvert.DeserializeObject<DiscordConfigJson>(json);

            var config = new DiscordConfiguration
            {
                Token = configJson.Token,
                TokenType = TokenType.Bot,
                AutoReconnect = true,
                LogLevel = LogLevel.Debug,
                UseInternalLogHandler = true
            };

            Client = new DiscordClient(config);

            Client.Ready += OnClientReady;


            var commandsConfig = new CommandsNextConfiguration
            {
                StringPrefixes = new string[] { configJson.Prefix },
                EnableMentionPrefix = true,
                EnableDms = true,
                DmHelp = true           
            };

            Commands = Client.UseCommandsNext(commandsConfig);
            Commands.RegisterCommands<FunCommands>();
            Commands.RegisterCommands<StatisticalCommands>();
            Commands.RegisterCommands<TwitchCommands>();

            await Client.ConnectAsync();
            await Task.Delay(-1);
        }


        private async Task GenerateChampionsFromJson()
        {
            string json = string.Empty;

            using (var fs = File.OpenRead("champions.json"))
            {
                using var sr = new StreamReader(fs, new UTF8Encoding(false));
                json = await sr.ReadToEndAsync().ConfigureAwait(false);
            }

            champs = Welcome.FromJson(json);

        }

        private Task OnClientReady(ReadyEventArgs e)
        {
            return Task.CompletedTask;
        }

    }
}
