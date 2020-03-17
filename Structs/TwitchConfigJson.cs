using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace DiscordBotHavi.Structs
{
    public struct TwitchConfigJson
    {
        [JsonProperty("clientId")]
        public string ClientId { get; private set; }

        [JsonProperty("clientSecret")]
        public string ClientSecret { get; private set; }
    }
}
