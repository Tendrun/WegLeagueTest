using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WegLeagueTest.Riot.ResponseModel
{
    public class InfoDto
    {
        public long gameCreation { get; set; }
        public long gameDuration { get; set; }
        public long gameEndTimestamp { get; set; }
        public long gameId { get; set; }
        public string gameMode { get; set; }
        public string gameName { get; set; }
        public long gameStartTimestamp { get; set; }
        public string gameType { get; set; }
        public string gameVersion { get; set; }
        public int mapId { get; set; }
        public List<MatchV5.ParticipantDto> participants { get; set; }
        public string platformId { get; set; }
        public int queueId { get; set; }
        /// <summary>
        /// Team index
        /// 0 - red side,
        /// 1 - blue side 
        /// </summary>
        public List<MatchV5.TeamDto> teams { get; set; }
        public string tournamentCode { get; set; }

    }
}
