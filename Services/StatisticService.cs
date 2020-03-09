using DiscordBotHavi.Adapters;
using DiscordBotHavi.Classes;
using DiscordBotHavi.Classes.MatchDTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Text.RegularExpressions;
using System.Xml.Schema;

namespace DiscordBotHavi.Services
{
    public class StatisticService
    {

        private readonly RiotAdapter riotAdapter = new RiotAdapter();

        public Dictionary<int, ChampionMatchDTO> GetUniqueChampionsPlayedById(MatchlistDTO matchlist, string accountId)
        {
            Dictionary<int, ChampionMatchDTO> uniqueChampionDictionary = new Dictionary<int, ChampionMatchDTO>();

            foreach(MatchReferenceDTO match in matchlist.matches)
            {
                if (uniqueChampionDictionary.ContainsKey(match.champion))
                {
                    uniqueChampionDictionary[match.champion].matchIds.Add(match.gameId);
                }
                else
                {
                    string championName = GetChampionNameById(match.champion);
                    
                    uniqueChampionDictionary.Add(match.champion, new ChampionMatchDTO()
                    {
                        championId = match.champion,
                        championName = championName,
                        matchIds = new List<long>()
                        {
                            match.gameId
                        },
                        
                    } );
                }
            }

            return GetWinRateByChampionAndReturn(LimitToTop5PlayedChamps(uniqueChampionDictionary), accountId);
        }

   
        private Dictionary<int, ChampionMatchDTO> LimitToTop5PlayedChamps(Dictionary<int, ChampionMatchDTO> champs)
        {
            Dictionary<int, ChampionMatchDTO> newDictionary = new Dictionary<int, ChampionMatchDTO>();

            ChampionMatchDTO currentDTO = new ChampionMatchDTO()
            {
                matchIds = new List<long>()
            };

            foreach(ChampionMatchDTO championMatch in champs.Values)
            {
                if( currentDTO.matchIds.Count < championMatch.matchIds.Count)
                {
                    currentDTO = championMatch;
                }
            }

            newDictionary.Add(currentDTO.championId, currentDTO);
            champs.Remove(currentDTO.championId);
            currentDTO = new ChampionMatchDTO()
            {
                matchIds = new List<long>()
            };

            foreach (ChampionMatchDTO championMatch in champs.Values)
            {
                if (currentDTO.matchIds.Count < championMatch.matchIds.Count)
                {
                    currentDTO = championMatch;
                }
            }

            newDictionary.Add(currentDTO.championId, currentDTO);
            champs.Remove(currentDTO.championId);
            currentDTO = new ChampionMatchDTO()
            {
                matchIds = new List<long>()
            };

            foreach (ChampionMatchDTO championMatch in champs.Values)
            {
                if (currentDTO.matchIds.Count < championMatch.matchIds.Count)
                {
                    currentDTO = championMatch;
                }
            }

            newDictionary.Add(currentDTO.championId, currentDTO);

            return newDictionary;
        }

        private bool GetWinLossByMatchId(long gameId, string accountId)
        {
            MatchDto match = riotAdapter.GetMatchById(gameId.ToString()).Result;

            if (match != null)
            {
                long participantId = 0;

                // 100 for blue side; 200 for red side
                long teamId = 0;

                // "fail" for loss; "Win" for win
                string winStatus = string.Empty;

                foreach (ParticipantIdentity participant in match.ParticipantIdentities)
                {
                    if (participant.Player.AccountId.Equals(accountId))
                    {
                        participantId = participant.ParticipantId;
                        break;
                    }
                }

                foreach (Participant participant in match.Participants)
                {
                    if (participant.ParticipantId == participantId)
                    {
                        teamId = participant.TeamId;
                        break;
                    }
                }

                foreach (Team team in match.Teams)
                {
                    if (team.TeamId == teamId)
                    {
                        winStatus = team.Win;
                        break;
                    }
                }

                switch (winStatus)
                {
                    case "Win":
                        return true;
                    default:
                        return false;
                }
            }
            throw new Exception("match was null: problem with API");
        }

        private Dictionary<int, ChampionMatchDTO> GetWinRateByChampionAndReturn(Dictionary<int, ChampionMatchDTO> champs, string accountId)
        {
            double wins = 0;
            double total = 0;

            foreach(ChampionMatchDTO championMatch in champs.Values)
            {
                wins = 0;
                total = 0;

                foreach(long matchId in championMatch.matchIds)
                {
                    if (GetWinLossByMatchId(matchId, accountId))
                    {
                        wins++;
                        total++;
                    }
                    else
                    {
                        total++;
                    }
                }
               
                    championMatch.winrate = Math.Round((wins / total), 2);
              
               
            }

            return champs;
        }

        private string GetChampionNameById(long champId)
        {
            var list = DiscordBot.champs.Data.Values.ToList();

            foreach(Datum champion in list)
            {
                if(champion.Key == champId)
                {
                    return champion.Name;
                }
            }

            return null;
        }
        
    }
}
