using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WegLeagueTest.Riot.ResponseModel
{
    public class MetadataDto
    {
        public string dataVersion { get;set;}
        public string matchId { get; set; }
        public List<string> participants { get; set; }
    }
}
