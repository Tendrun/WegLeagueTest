using System;
using System.Collections.Generic;
using System.Windows.Forms;
using System.Linq;
using System.Text.Json;
using System.IO;
using System.Threading.Tasks;

namespace WegLeagueTest.Database
{
    public class DataAnalyzer
    {
        Riot.ChampionData ChampionDatas;

        DataAnalyzer()
        {
            using (StreamReader r = new StreamReader(@"C:\Users\mikis\Downloads\dragontail-13.18.1\13.18.1\data\en_US\champion.json"))
            {
                string jsondata = r.ReadToEnd();
                ChampionDatas = JsonSerializer.Deserialize<Riot.ChampionData>(jsondata);
            }
        }

        public string BanRate(List<Riot.ResponseModel.MatchDto> MatchesInfo)
        {
            //read
            List<Riot.ChampionData> ChampionDatas

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

            return null;
        }
    }
}
