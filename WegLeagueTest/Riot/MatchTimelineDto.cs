using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WegLeagueTest.Riot
{
    public class MatchTimelineDto
    {
        public List<string> metadata { get; set; }
    }

    public class metadata
    {
        public string dataVersion { get; set; }
    }
}
