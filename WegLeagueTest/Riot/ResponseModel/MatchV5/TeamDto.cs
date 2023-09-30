using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WegLeagueTest.Riot.ResponseModel.MatchV5
{
    public class TeamDto
    {
        public List<BanDto> bans { get; set; }
        public ObjectivesDto objectives { get; set; }
        public int teamId { get; set; }
        public bool win { get; set; }
    }
}
