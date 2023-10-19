using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Windows.Forms;
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

        //temp for here
        Riot.BanData datas;
        public IActionResult ChampionsDataView()
        {
            //Create Database from players
            Riot.Requests Req = new Riot.Requests();
            Database.DatabaseManager database = new Database.DatabaseManager();
            Database.DataAnalyzer dataAnalyzer = new Database.DataAnalyzer();

            //change paths

            //read database
            List<Riot.ResponseModel.Player> PlayersDatas = database.ReadPlayersDB();



            List<Riot.ResponseModel.MatchDto> MatchesInfo = new List<Riot.ResponseModel.MatchDto>();
            foreach (var data in PlayersDatas)
            {
                MatchesInfo = Req.ReturnMatchInfo(data.matchDtos, Riot.Requests.WorldRegions.europe);
            }

            datas = dataAnalyzer.CountBanRate(MatchesInfo);          
            

            return View(datas);
        }

        public IActionResult Sort()
        {



            MessageBox.Show("DZIALA");
            //datas.ChampionBans = datas.ChampionBans.OrderByDescending(ban => ban.pickChampionDatas.Max(pc => pc.Value.Winrate)).ToList();
            return View(datas);
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
