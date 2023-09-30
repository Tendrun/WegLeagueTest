using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WegLeagueTest.Riot.ResponseModel
{
    public class MatchDto
    {
        public MetadataDto metadata { get; set; }
        public InfoDto info { get; set; }
    }
}
