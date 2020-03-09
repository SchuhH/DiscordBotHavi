using System;
using System.Collections.Generic;
using System.Text;

namespace DiscordBotHavi.Classes
{
    public class MatchlistDTO
    {
        public List<MatchReferenceDTO> matches { get; set; }
        public int endIndex { get; set; }
        public int startIndex { get; set; }
        public int totalGames { get; set; }

    }
}
