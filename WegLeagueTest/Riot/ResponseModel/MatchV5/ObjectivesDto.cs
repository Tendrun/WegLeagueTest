using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WegLeagueTest.Riot.ResponseModel.MatchV5
{
    public class ObjectivesDto
    {
        public ObjectiveDto baron { get; set; }
        public ObjectiveDto champion { get; set; }
        public ObjectiveDto dragon { get; set; }
        public ObjectiveDto inhibitor { get; set; }
        public ObjectiveDto riftHerald { get; set; }
        public ObjectiveDto tower { get; set; }
    }
}
