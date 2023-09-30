using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WegLeagueTest.Riot.ResponseModel
{
    public class Player
    {
        //for now all matches and puuid 

        //summonerid etc
        public LeagueEntryDTO leagueEntryDTO { get; set; }

        //puuuid
        public SummonerDTO summonerDTO { get; set; }

        //matches
        public List<string> matchDtos { get; set; }
    }
}
