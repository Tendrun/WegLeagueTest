using System;
using System.Collections.Generic;
using System.Linq;
using System.IO;
using System.Text.Json;
using System.Threading.Tasks;

namespace WegLeagueTest.Database
{
    public class DatabaseManager
    {
        const string PlayersDBpath = @"C:\Users\Mikołaj\Downloads\Players.txt";

        public void OverridePlayersDB(List<Riot.ResponseModel.Player> players)
        {
            using (StreamWriter r = new StreamWriter(PlayersDBpath, false))
            {
                r.Write(JsonSerializer.Serialize(players));

                r.Close();
            }
        }

        public List<Riot.ResponseModel.Player> ReadPlayersDB()
        {
            using (StreamReader r = new StreamReader(PlayersDBpath))
            {
                string jsondata = r.ReadToEnd();
                List<Riot.ResponseModel.Player>PlayersDatas = JsonSerializer.Deserialize<List<Riot.ResponseModel.Player>>(jsondata);

                r.Close();

                return PlayersDatas;
            }
        }
    }
}
