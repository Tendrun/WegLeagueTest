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
        public void OverridePlayersDB(List<Riot.ResponseModel.Player> players)
        {
            using (StreamWriter r = new StreamWriter(@"C:\Users\mikis\Documents\WazneProgramy\LOLDB\Players.txt", false))
            {
                r.Write(JsonSerializer.Serialize(players));

                r.Close();
            }
        }
    }
}
