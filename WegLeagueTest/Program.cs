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
            //Create Database from players
            Riot.Requests Req = new Riot.Requests();
            /*
            Req.CreatePlayerDB(Riot.Requests.Regions.eun1, Riot.Requests.WorldRegions.europe);
            */

            //Check if match is up to today's patch

            //Read file champion data

            Riot.ChampionData championDatas = new Riot.ChampionData();
            using (StreamReader r = new StreamReader(@"C:\Users\mikis\Downloads\dragontail-13.18.1\13.18.1\data\en_US\champion.json"))
            {
                string jsondata = r.ReadToEnd();

                championDatas = JsonSerializer.Deserialize<Riot.ChampionData>(jsondata);
            }
            // -1 no ban


            //for now try to get the biggest ban rate
            List<Riot.ResponseModel.Player> PlayersDatas = new List<Riot.ResponseModel.Player>();
            using (StreamReader r = new StreamReader(@"C:\Users\mikis\Documents\WazneProgramy\LOLDB\Players.txt"))
            {
                string jsondata = r.ReadToEnd();
                PlayersDatas = JsonSerializer.Deserialize<List<Riot.ResponseModel.Player>>(jsondata);
            }

            List<Riot.ResponseModel.MatchDto> MatchesInfo = new List<Riot.ResponseModel.MatchDto>();
            foreach (var data in PlayersDatas)
            {
                MatchesInfo =  Req.ReturnMatchInfo(data.matchDtos, Riot.Requests.WorldRegions.europe);                
            }

            MessageBox.Show("MatchesInfo = "+ MatchesInfo.Count);

            foreach (var match in MatchesInfo)
            {
                foreach (var team in match.info.teams)
                {
                    foreach (var ban in team.bans)
                    {
                        MessageBox.Show(ban.championId.ToString());
                    }                   
                }
            }
            
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
