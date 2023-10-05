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
            WegLeagueTest.Database.DatabaseManager database = new Database.DatabaseManager();
            Database.DataAnalyzer dataAnalyzer = new Database.DataAnalyzer();


            //read database
            List<Riot.ResponseModel.Player> PlayersDatas = database.ReadPlayersDB();



            List<Riot.ResponseModel.MatchDto> MatchesInfo = new List<Riot.ResponseModel.MatchDto>();
            foreach (var data in PlayersDatas)
            {
                MatchesInfo = Req.ReturnMatchInfo(data.matchDtos, Riot.Requests.WorldRegions.europe);
            }

            Tuple<int, List<Riot.BanData>> datas = dataAnalyzer.CountBanRate(MatchesInfo);

            foreach (var banData in datas.Item2)
            {
                string message = $"ID: {banData.id}, NumBans: {float. banData.NumBans / datas.Item1}%";
                MessageBox.Show(message, "Ban Data Details", MessageBoxButtons.OK, MessageBoxIcon.Information);
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
