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
        const string JsonPath = @"C:\Users\mikis\Downloads\dragontail-13.18.1\13.18.1\data\en_US\champion.json";


        //Read data about champions on creating instance DataAnalyzer
        public DataAnalyzer()
        {
            using (StreamReader r = new StreamReader(JsonPath))
            {
                string jsondata = r.ReadToEnd();
                ChampionDatas = JsonSerializer.Deserialize<Riot.ChampionData>(jsondata);
            }
        }

        public Riot.BanData CountBanRate(List<Riot.ResponseModel.MatchDto> MatchesInfo)
        {
            //create list of champions to count bans for every invidual
            Riot.BanData banData = new Riot.BanData();

            foreach (var data in ChampionDatas.data.Values)
            {
                Riot.ChampionBan championBan = new Riot.ChampionBan
                {
                    Id = data.id,
                    key = data.key
                };

                foreach (Riot.ChampionPosition position in Enum.GetValues(typeof(Riot.ChampionPosition)))
                {
                    championBan.pickChampionDatas[position] = new Riot.PickChampionData();
                }

                banData.ChampionBans.Add(championBan);
            }

            //Get Champion who is banning and what lane
            //then get what champion he is banning
            foreach (var match in MatchesInfo)
            {
                // This is MetadataDto

                //This is InfoDto
                foreach (var participant in match.info.participants)
                {
                    //Champion pick rate
                    var ReturnedElement = banData.ChampionBans.FirstOrDefault(banData => banData.Id == participant.championName);

                    if (ReturnedElement != null)
                    {
                        //if element found add match
                        banData.NumbOfMatches++;
                        //find in dictionary proper lane
                        var element = ReturnedElement.pickChampionDatas.FirstOrDefault(data => data.Key.ToString() == participant.teamPosition);

                        //if found add
                        if (element.Value != null)
                        {
                            
                            element.Value.Matches++;
                            if(participant.win == true) element.Value.wins++;
                        }
                        //if not add to dictionary key
                        else
                        {
                            Enum.TryParse(participant.teamPosition, out Riot.ChampionPosition position);
                            ReturnedElement.pickChampionDatas.Add(position, new Riot.PickChampionData());
                            element = ReturnedElement.pickChampionDatas.FirstOrDefault(data => data.Key.ToString() == participant.teamPosition);
                            element.Value.Matches++;
                            if (participant.win == true) element.Value.wins++;
                        }
                        
                    }

                }

                //Champion Ban Rate
                foreach (var team in match.info.teams)
                {
                    foreach (var ban in team.bans)
                    {
                        //info.teams.bans.championid pickTurn
                        var ReturnedElement = banData.ChampionBans.FirstOrDefault(banData => banData.key == ban.championId.ToString());

                        if (ReturnedElement != null)
                        {
                            banData.NumofAllBans++;
                            ReturnedElement.NumbChampionBans++;
                        }
                    }
                }
            }


            //count ban percentage
            foreach (var ban in banData.ChampionBans)
            {
                if (ban.NumbChampionBans == 0) continue;
                ban.BanRate = Math.Round((float)ban.NumbChampionBans / (float)banData.NumofAllBans, 3, MidpointRounding.AwayFromZero) * 100;
            }

            //Pick Rate
            foreach (var pick in banData.ChampionBans)
            {
                foreach (var p in pick.pickChampionDatas.Values)
                {
                    if (p.wins == 0) continue;
                    p.Winrate = Math.Round((float)p.wins / (float)banData.NumbOfMatches, 3, MidpointRounding.AwayFromZero) * 100;
                }
            }

            return banData;
        }
    }
}
