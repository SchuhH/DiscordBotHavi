using System;
using System.Collections.Generic;
using System.Text;

namespace DiscordBotHavi.Classes
{
    public class ChampionMatchDTO
    {
        public int championId { get; set; }

        public string championName { get; set; }
        public List<long> matchIds { get; set; }      

        public double winrate { get; set; }

    }
}
