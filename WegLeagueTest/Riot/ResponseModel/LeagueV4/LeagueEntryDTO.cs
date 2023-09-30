using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WegLeagueTest.Riot.ResponseModel
{
    public class LeagueEntryDTO
    {
        public string leagueId { get; set; }
        public string summonerId { get; set; }
        public string summonerName { get; set; }
        public string queueType { get; set; }
        public string tier { get; set; }
        public string rank{ get; set; }
        public int leaguePoints { get; set; }
        public int wins { get; set; }
        public int losses { get; set; }
        public bool hotStreak { get; set; }
        public bool veteran { get; set; }
        public bool freshBlood { get; set; }
        public bool inactive { get; set; }
        public MiniSeriesDTO miniSeries { get; set; }
    }
}
