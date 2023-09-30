using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WegLeagueTest.Riot
{
    //DONT BE MISTAKEN TO DESERIALIZE DATA USE ChampionData
    public class ChampionData
    {
        public string type { get; set; }
        public string format { get; set; }
        public string version { get; set; }
        public Dictionary<string, ChampionInfo> data { get; set; }
    }

    public class ChampionInfo
    {
        public string version { get; set; }
        public string id { get; set; }
        public string key { get; set; }
        public string name { get; set; }
        public string title { get; set; }
        public string blurb { get; set; }
        public ChampionInfoStats info { get; set; }
        public ChampionImage image { get; set; }
        public List<string> tags { get; set; }
        public string partype { get; set; }
        public ChampionStats stats { get; set; }
    }

    public class ChampionInfoStats
    {
        public int attack { get; set; }
        public int defense { get; set; }
        public int magic { get; set; }
        public int difficulty { get; set; }
    }

    public class ChampionStats
    {
        public int hp { get; set; }
        public int hpperlevel { get; set; }
        public int mp { get; set; }
        public float mpperlevel { get; set; }
        public int movespeed { get; set; }
        public int armor { get; set; }
        public float armorperlevel { get; set; }
        public int spellblock { get; set; }
        public float spellblockperlevel { get; set; }
        public int attackrange { get; set; }
        public float hpregen { get; set; }
        public float hpregenperlevel { get; set; }
        public float mpregen { get; set; }
        public float mpregenperlevel { get; set; }
        public int crit { get; set; }
        public int critperlevel { get; set; }
        public int attackdamage { get; set; }
        public float attackdamageperlevel { get; set; }
        public float attackspeedperlevel { get; set; }
        public float attackspeed { get; set; }
    }

    public class ChampionImage
    {
        public string full { get; set; }
        public string sprite { get; set; }
        public string group { get; set; }
        public int x { get; set; }
        public int y { get; set; }
        public int w { get; set; }
        public int h { get; set; }
    }
}
