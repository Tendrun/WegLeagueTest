using System;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace WegLeagueTest.Riot
{
    //Who bans it(what champion), on what lane
    //What is banned

    public class BanData
    {
        public int NumbOfMatches { get; set; }
        public int NumofAllBans { get; set; }
        public List<ChampionBan> ChampionBans { get; set; } = new List<ChampionBan>();
    }

    public class ChampionBan
    {
        public int NumbChampionBans { get; set; } = 0;
        //All stats
        public int Rank { get; set; }
        public double BanRate { get; set; }
        //There has to be diffrent data for every role 
        public Dictionary<ChampionPosition, PickChampionData> pickChampionDatas { get; set; } = new Dictionary<ChampionPosition, PickChampionData>();
        public string key { get; set; }
        public string Id { get; set; }
    }

    public class PickChampionData
    {        
        public TierChampion Tier { get; set; }
        public double Winrate { get; set; }
        public int wins { get; set; }
        public float Pickrate { get; set; }
        public int Matches { get; set; }
    }
}
