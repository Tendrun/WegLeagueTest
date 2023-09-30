using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System.IO;
using System;
using System.Text.Json;
using System.Windows.Forms;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WegLeagueTest
{
    public class Program
    {
        public static void Main(string[] args)
        {

            Riot.Requests Req = new Riot.Requests();


            //rate Limit problem error 429
            //re send header and set httpclient rate limit

            Req.CreatePlayerDB(Riot.Requests.Regions.eun1, Riot.Requests.WorldRegions.europe);



            //for now 2 matches

            /*
            List<string> ListOfMatches = new List<string> { "EUN1_3454690331" };
            var MatchesInfo = Req.ReturnMatchInfo(ListOfMatches, Riot.Requests.WorldRegions.europe);

            //Check if match is up to today's patch

            //Read file champion data

            Riot.ChampionData championDatas;
            using (StreamReader r = new StreamReader(@"C:\Users\mikis\Downloads\dragontail-13.18.1\13.18.1\data\en_US\champion.json"))
            {
                string jsondata = r.ReadToEnd();
                championDatas = JsonSerializer.Deserialize<Riot.ChampionData>(jsondata);
            }

            //for now try to get the biggest ban rate

            
            var MatchInfo = MatchesInfo[0];

            int i = 1;
            foreach (var ban in MatchInfo.info.teams[0].bans)
            {
                MessageBox.Show($"ban {i} + "+ ban.championId.ToString());
            }
            

            //MatchInfo.info.teams[0].bans
            */

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
