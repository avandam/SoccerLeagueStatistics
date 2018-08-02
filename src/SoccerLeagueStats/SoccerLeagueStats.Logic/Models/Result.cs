using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SoccerLeagueStats.Logic.Models
{
    public class Result
    {
        public string Id { get; private set; }
        public int Place { get; private set; }
        public int Wins { get; private set; }
        public int Draws { get; private set; }
        public int Losses { get; private set; }
        public int GoalsFor { get; private set; }
        public int GoalsAgainst { get; private set; }
        public Season Season { get; private set; }
        public Club Club { get; private set; }

        public Result(string id, int place, int wins, int draws, int losses, int goalsFor, int goalsAgainst, Season season, Club club)
        {
            Id = id;
            Place = place;
            Wins = wins;
            Draws = draws;
            Losses = losses;
            GoalsFor = goalsFor;
            GoalsAgainst = goalsAgainst;
            Season = season;
            Club = club;
        }

        public Result(int place, int wins, int draws, int losses, int goalsFor, int goalsAgainst, Season season, Club club)
            : this(Guid.NewGuid().ToString(), place, wins, draws, losses, goalsFor, goalsAgainst, season, club)
        {
        }

        public Result(int wins, int draws, int losses, int goalsFor, int goalsAgainst, Club club)
        {
            Wins = wins;
            Draws = draws;
            Losses = losses;
            GoalsFor = goalsFor;
            GoalsAgainst = goalsAgainst;
            Club = club;
        }

        public int GetPoints(bool useCompetitionDefault)
        {
            int pointsForAWin = useCompetitionDefault ? Season.Competition.DefaultPointsForAWin : Season.PointsForAWin;
            return pointsForAWin * Wins + Draws;
        }
        public int GetGoalDifference()
        {
            return GoalsFor - GoalsAgainst;
        }

        public int GetNumberOfMatches()
        {
            return Wins + Draws + Losses;
        }

        public void SetPlace(int place)
        {
            Place = place;
        }
    }
}
