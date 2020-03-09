using System;
using System.Collections.Generic;
using System.Text;

namespace DiscordBotHavi.Classes
{
    public class SummonerDTO
    {
        public int profileIconId { get; set; }
        public string name { get; set; }
        public string puuid { get; set; }
        public int summonerLevel { get; set; }
        public string accountId { get; set; }
        public string id { get; set; }
        public long revisionDate { get; set; }
    }
}
