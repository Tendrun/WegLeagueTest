using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WegLeagueTest
{
    public class Program
    {
        public static void Main(string[] args)
        {
            //MyCode
            Riot.Requests Req = new Riot.Requests();
            Req.GetPlayers(Riot.Requests.Division.IV, Riot.Requests.Tier.DIAMOND, Riot.Requests.Queue.RANKED_SOLO_5x5, Riot.Requests.Regions.eun1);


            CreateHostBuilder(args).Build().Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                });
    }
}
