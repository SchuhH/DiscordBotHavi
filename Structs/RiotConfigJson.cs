using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace DiscordBotHavi.Structs
{
    public struct RiotConfigJson
    {
        [JsonProperty("key")]
        public string Key { get; private set; }
    }
}
