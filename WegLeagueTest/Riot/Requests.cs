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
using System.Threading;

namespace WegLeagueTest.Riot
{
    public class Requests
    {
        const string RiotApiKey = Key.KeyValue;
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

        public enum WorldRegions
        {
            europe
        }

        /*
        1./lol/league/v4/entries/{queue}/{tier}/{division} //SummonerId Diament IV SQ EUNE
        2./lol/summoner/v4/summoners/{encryptedSummonerId} Summonerid => puuid
        3./lol/match/v5/matches/by-puuid/{puuid}/ids puuid => MatchHsitory EUN1_3451266300
        4./lol/match/v5/matches/{matchId} matchid => Allinfo of math
        */

        public void CreatePlayerDB(Regions region, WorldRegions worldregion)
        {
            List<Riot.ResponseModel.Player> players = new List<ResponseModel.Player>();

            //iterate through all tiers and division, queue is SoloQ
            Queue queue = Queue.RANKED_FLEX_SR;

            //tier
            //foreach (Tier tier in Enum.GetValues(typeof(Tier)))
            {
                Tier tier = Tier.EMERALD;

                //division
                //foreach (Division division in Enum.GetValues(typeof(Division)))
                {
                    Division division = Division.I;
                    List<ResponseModel.LeagueEntryDTO> SummonersIds = GetSummonerIds(division, tier, queue, region);
                    List<ResponseModel.SummonerDTO> Puuids = GetPuuids(SummonersIds, region);
                    //by summonerIds.summonerid -> SummonerDTO.id add puuid

                    //find in db of match repeats if not add,
                    //if player doesnt exit in db add him


                    players = (from summoner in SummonersIds
                               join puuid in Puuids
                               on summoner.summonerId equals puuid.id
                               select new Riot.ResponseModel.Player
                               {
                                   leagueEntryDTO = summoner,
                                   summonerDTO = puuid,
                                   matchDtos = ReturnMatchesId(puuid, worldregion)
                               }).ToList();
                }
            }

            Database.DatabaseManager database = new Database.DatabaseManager();

            database.OverridePlayersDB(players);

            MessageBox.Show("Database completed");
        }

        public List<ResponseModel.LeagueEntryDTO> GetSummonerIds(Division division, Tier tier, Queue queue, Regions region)
        {


            //List Of players
            List<ResponseModel.LeagueEntryDTO> ListOfPlayers = new List<ResponseModel.LeagueEntryDTO>();
            //Change tihs value to 1, in Release
            int i = 1;

            while (true)
            {
                //link
                //increment through all pages
                string GetPlayers = $"https://{region}.api.riotgames.com/lol/league/v4/entries/{queue}/{tier}/{division}";
                GetPlayers += $"?page={i}";
                ++i;




                //client
                using (HttpClient client = CreateClient())
                {
                    //request
                    var request = CreatehttpRequestMessage(new Uri(GetPlayers), HttpMethod.Get);

                    //send query for ever pages of summoners
                    //var responseMessage = client.SendAsync(request).Result;
                    var responseMessage = SendRequest(client, request, nameof(GetSummonerIds));
                    string responseMessageString = responseMessage.Content.ReadAsStringAsync().Result;


                    int PlayerCount = 0;

                    //add to list summoners
                    
                    PlayerCount = JsonSerializer.Deserialize<List<ResponseModel.LeagueEntryDTO>>(responseMessageString).Count;
                    


                    for (int j = 0; j < PlayerCount; j++)
                    {
                        ListOfPlayers.Add(JsonSerializer.Deserialize<List<ResponseModel.LeagueEntryDTO>>(responseMessageString)[j]);
                    }


                    //checks for now only 10 pages
                    // on reales delte 10
                    if (responseMessageString == "[]" || i == 10)
                    {
                        break;
                    }

                }
            }

            return ListOfPlayers;
        }


        public List<ResponseModel.SummonerDTO> GetPuuids(List<ResponseModel.LeagueEntryDTO> LeagueEntries, Regions region)
        {
            //List Of players
            List<ResponseModel.SummonerDTO> ListOfPuuids = new List<ResponseModel.SummonerDTO>();

            int j = 0;

            //Change 10 to LeagueEntries.Count on Release
            for (int i = 0; i < LeagueEntries.Count; i++)
            {
                //link
                //Send query based on summonerid

                string GetPlayers = $"https://{region}.api.riotgames.com/lol/summoner/v4/summoners/{LeagueEntries[i].summonerId}";

                ++j;

                //client
                using (HttpClient client = CreateClient())
                {

                    //request
                    var request = CreatehttpRequestMessage(new Uri(GetPlayers), HttpMethod.Get);

                    //send query
                    var responseMessage = SendRequest(client, request, nameof(GetPuuids));

                    string responseMessageString = responseMessage.Content.ReadAsStringAsync().Result;

                    //add Summoner to list
                    ListOfPuuids.Add(JsonSerializer.Deserialize<ResponseModel.SummonerDTO>(responseMessageString));
                }

                if (j == 10)
                {
                    break;
                }

            }


            return ListOfPuuids;
        }

        public List<string> ReturnMatchesId(ResponseModel.SummonerDTO summonerDTO, WorldRegions region)
        {
            int count = 2;
            string Type = "ranked";

            List<string> ReturnMatches = new List<string>();

            //link
            //Send query based on puuid

            //count from 1 to 100 max
            string GetPlayers = $"https://{region}.api.riotgames.com/lol/match/v5/matches/by-puuid/{summonerDTO.puuid}/ids?type={Type}&count={count}";

            //client
            using (HttpClient client = CreateClient())
            {

                //request
                var request = CreatehttpRequestMessage(new Uri(GetPlayers), HttpMethod.Get);

                //send query
                var responseMessage = SendRequest(client, request, nameof(ReturnMatchesId));
                string responseMessageString = responseMessage.Content.ReadAsStringAsync().Result;

                List<string> ListOfMtaches = JsonSerializer.Deserialize<List<string>>(responseMessageString);               

                // Deserialize each string in the list one by one
                foreach (string jsonString in ListOfMtaches)
                {                   
                    ReturnMatches.Add(jsonString);                                  
                }
            }
            

            return ReturnMatches;
        }

        public List<ResponseModel.MatchDto> ReturnMatchInfo(List<string> matchId, WorldRegions region)
        {
            List<ResponseModel.MatchDto> ListOfmatchtes = new List<ResponseModel.MatchDto>();

            for (int i = 0; i < matchId.Count; i++)
            {
                //link
                //Send query to get match info
                string GetPlayers = $"https://{region}.api.riotgames.com/lol/match/v5/matches/{matchId[i]}";

                //client
                using (HttpClient client = CreateClient())
                {
                    var request = CreatehttpRequestMessage(new Uri(GetPlayers), HttpMethod.Get);

                    //send query
                    var responseMessage = SendRequest(client, request, nameof(ReturnMatchesId));
                    string responseMessageString = responseMessage.Content.ReadAsStringAsync().Result;

                    ResponseModel.MatchDto match = JsonSerializer.Deserialize<ResponseModel.MatchDto>(responseMessageString);



                    ListOfmatchtes.Add(match);                   
                }
            }

            return ListOfmatchtes;
        }

        //Create HttpRequestMessage
        HttpRequestMessage CreatehttpRequestMessage(Uri uri, HttpMethod method)
        {
            //create request
            HttpRequestMessage message = new HttpRequestMessage()
            {
                RequestUri = uri,
                Method = method
            };

            //add header
            message.Headers.Add("X-Riot-Token", RiotApiKey);

            return message;
        }

        HttpClient CreateClient()
        {
            return new HttpClient();
        }

        //send query
        HttpResponseMessage SendRequest(HttpClient client, HttpRequestMessage message, string MethodName)
        {
            HttpResponseMessage responseMessage = new HttpResponseMessage();

            try
            {
                responseMessage = client.SendAsync(message).Result;

                if (responseMessage.StatusCode == HttpStatusCode.OK)
                {
                    
                }

                else if (responseMessage.StatusCode == HttpStatusCode.TooManyRequests)
                {
                    //resend message
                    //send until response will be ok

                    responseMessage.Headers.TryGetValues("Retry-After", out var values);
                    int seconds = int.Parse(values.FirstOrDefault());

                    //Retry
                    int MaxRetries = 10;

                    for (int i = 0; i < MaxRetries; i++)
                    {
                        Thread.Sleep(seconds * 1000);


                        message = CreatehttpRequestMessage(message.RequestUri, message.Method);
                        
                        responseMessage = client.SendAsync(message).Result;                       
                       

                        if (responseMessage.IsSuccessStatusCode)
                        {
                            string responseMessageString = responseMessage.Content.ReadAsStringAsync().Result;
                            break;
                        }
                    }
                }
                else if (responseMessage.StatusCode == HttpStatusCode.Forbidden)
                {
                    MessageBox.Show("Change Riot Api Key", "Riot Api Key is invalid", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }

                else
                {
                    MessageBox.Show("responseMessage.Content.ToString() = " + responseMessage.Content.ReadAsStringAsync().Result + "\nresponseMessage.Headers.ToString() = " + responseMessage.Headers.ToString());
                    MessageBox.Show($"Unkown error from method{MethodName} = " + responseMessage.StatusCode.ToString());
                }
            }
            catch (Exception e)
            {
                Console.WriteLine($"Network error: {e.Message}");
            }

            return responseMessage;
        }
    }

    
}
