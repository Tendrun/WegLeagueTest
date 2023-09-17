using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WegLeagueTest.Riot.ResponseModel
{
    public class SummonerDTO
    {
        string accountId { get; set; }
        int profileIconId { get; set; }
        long revisionDate { get; set; }
        string name { get; set; }
        string id { get; set; }
        string puuid { get; set; }
        long summonerLevel { get; set; }
    }
}
