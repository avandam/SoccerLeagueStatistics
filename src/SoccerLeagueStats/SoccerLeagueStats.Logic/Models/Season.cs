using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SoccerLeagueStats.Logic.Models
{
    public class Season
    {
        public string Id { get; private set; }
        public Competition Competition { get; private set; }
        public int PointsForAWin { get; private set; }

        public Season(string id, Competition competition, int pointsForAWin)
        {
            Id = id;
            Competition = competition;
            PointsForAWin = pointsForAWin;
        }

        public Season(Competition competition, int pointsForAWin)
            : this(Guid.NewGuid().ToString(), competition, pointsForAWin)
        {
        }
    }
}
