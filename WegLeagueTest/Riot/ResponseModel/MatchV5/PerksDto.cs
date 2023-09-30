using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WegLeagueTest.Riot.ResponseModel.MatchV5
{
    public class PerksDto
    {
        public PerkStatsDto statPerks { get; set; }
        public List<PerkStyleDto> styles { get; set; }
    }
}
