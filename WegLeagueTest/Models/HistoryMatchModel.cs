using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace WegLeagueTest.Models
{
    public class HistoryMatchModel
    {
        [Key]
        public int MatchId { get; set; }
    }
}
