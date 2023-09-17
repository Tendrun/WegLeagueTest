using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text.Json;
using System.Windows.Forms;
using System.Net;
using System.Net.Http.Headers;
using System.IO;
using System.Threading.Tasks;

namespace WegLeagueTest.Riot
{
    public class Requests
    {
        const string RiotApiKey = "RGAPI-33bb9952-dae0-4c52-a563-cc8b0e0925c1";
        const string LocalHost = "https://127.0.0.1:2999/";

        public enum Division
        {
            IV,
            III,
            II,
            I
        }

        public enum Tier
        {
            DIAMOND,
            EMERALD,
            PLATINUM,
            GOLD,
            SILVER,
            BRONZE,
            IRON
        }

        public enum Queue
        {
            RANKED_SOLO_5x5,
            RANKED_FLEX_SR,
            RANKED_FLEX_TT
        }

        public enum Regions
        {
            br1,
            eun1
        }

        /*
        1./lol/league/v4/entries/{queue}/{tier}/{division} //SummonerId Diament IV SQ EUNE
        2./lol/summoner/v4/summoners/{encryptedSummonerId} Summonerid => puuid
        3./lol/match/v5/matches/by-puuid/{puuid}/ids puuid => MatchHsitory EUN1_3451266300
        4. /lol/match/v5/matches/{matchId} matchid => Allinfo of math
        */

        public void GetPlayers(Division division, Tier tier, Queue queue, Regions region)
        {
            //link
            string GetPlayers = $"https://{region}.api.riotgames.com/lol/league/v4/entries/{queue}/{tier}/{division}";

            //client
            using (HttpClient client = new HttpClient()) {

                //request
                var request = new HttpRequestMessage()
                {
                    RequestUri = new Uri(GetPlayers),
                    Method = HttpMethod.Get,
                };

                //add Header
                request.Headers.Add("X-Riot-Token", RiotApiKey);

                var responseMessage = client.SendAsync(request).Result;
                string responseMessageString = responseMessage.Content.ReadAsStringAsync().Result;

                //List Of players
                var ListOfPlayers = JsonSerializer.Deserialize<List<ResponseModel.LeagueEntryDTO>>(responseMessageString);

                for (int i = 0; i < ListOfPlayers.Count; i++)
                {
                    MessageBox.Show(ListOfPlayers[i].summonerId);
                }

            }
        }
    }
}
