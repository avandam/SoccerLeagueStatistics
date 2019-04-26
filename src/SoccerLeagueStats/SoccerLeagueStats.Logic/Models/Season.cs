using System;

namespace SoccerLeagueStats.Logic.Models
{
    public class Season
    {
        public string Id { get; private set; }
        public string Name { get; private set; }
        public int PointsForAWin { get; private set; }
        public Competition Competition { get; private set; }

        public Season(string id, string name, int pointsForAWin, Competition competition)
        {
            Id = id;
            Name = name;
            PointsForAWin = pointsForAWin;
            Competition = competition;
        }

        public Season(string name, int pointsForAWin, Competition competition)
            : this(Guid.NewGuid().ToString(), name, pointsForAWin, competition)
        {
        }
    }
}
