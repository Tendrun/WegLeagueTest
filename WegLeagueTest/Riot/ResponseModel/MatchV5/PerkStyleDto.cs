using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WegLeagueTest.Riot.ResponseModel.MatchV5
{
    public class PerkStyleDto
    {
        public string description { get; set; }
        public List<PerkStyleSelectionDto> selections { get; set; }
        public int style { get; set; }
    }
}
