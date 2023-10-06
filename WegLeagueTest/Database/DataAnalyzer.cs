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
        const string JsonPath = @"C:\Users\Mikołaj\Downloads\dragontail-13.18.1.json";


        //Read data about champions on creating instance DataAnalyzer
        public DataAnalyzer()
        {
            using (StreamReader r = new StreamReader(JsonPath))
            {
                string jsondata = r.ReadToEnd();
                ChampionDatas = JsonSerializer.Deserialize<Riot.ChampionData>(jsondata);
            }
        }

        public Tuple<int, List<Riot.BanData>> CountBanRate(List<Riot.ResponseModel.MatchDto> MatchesInfo)
        {
            //create list of champions to count bans for every invidual
            List<Riot.BanData> banDatas = new List<Riot.BanData>();

            int bans = 0;

            foreach (var data in ChampionDatas.data.Values)
            {
                banDatas.Add(new Riot.BanData(data.id, data.key));
            }

            foreach (var match in MatchesInfo)
            {
                foreach (var team in match.info.teams)
                {
                    foreach (var ban in team.bans)
                    {
                        var ReturnedElement = banDatas.FirstOrDefault(banData => banData.key == ban.championId.ToString());

                        if (ReturnedElement != null)
                        {
                            bans++;
                            ReturnedElement.NumBans++;
                        }
                    }
                }
            }



            return new Tuple<int, List<Riot.BanData>>(bans, banDatas);
        }
    }
}
