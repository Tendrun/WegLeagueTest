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
        public int NumofAllBans { get; set; }
        public List<ChampionBan> ChampionBans { get; set; } = new List<ChampionBan>();
    }

    public class ChampionBan
    {
        public int NumbChampionBans { get; set; } = 0;                
        public double PercBan { get; set; }
        public string key { get; set; }
        public string Id { get; set; }
    }
}
