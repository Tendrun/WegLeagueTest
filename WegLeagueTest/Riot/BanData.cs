using System;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace WegLeagueTest.Riot
{
    public class BanData
    {
        public int NumBans { get; set; } = 0;
        public string key { get; set; }
        public string Id { get; set; }
    }
}
