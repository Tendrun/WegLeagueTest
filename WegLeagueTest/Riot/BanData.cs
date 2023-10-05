using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WegLeagueTest.Riot
{
    public class BanData
    {
        public BanData(string id, string key)
        {
            this.key = key;
            this.id = id;
        }

        public int NumBans = 0;
        public string key;
        public string id;
    }
}
