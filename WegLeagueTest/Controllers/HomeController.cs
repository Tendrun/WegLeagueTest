using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using WegLeagueTest.Models;

namespace WegLeagueTest.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult ChampionsDataView()
        {
            //Create Database from players
            Riot.Requests Req = new Riot.Requests();
            WegLeagueTest.Database.DatabaseManager database = new Database.DatabaseManager();
            Database.DataAnalyzer dataAnalyzer = new Database.DataAnalyzer();

            //change paths

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
                if (banData.NumBans == 0) continue;
                //string message = $"ID: {banData.id}, total bans: {datas.Item1} Champion bans: {banData.NumBans}  NumBans: {Math.Round((float)((float)banData.NumBans / datas.Item1), 3, MidpointRounding.AwayFromZero) * 100}%";
                //MessageBox.Show(message, "Ban Data Details", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }

            return View(datas.Item2);
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
