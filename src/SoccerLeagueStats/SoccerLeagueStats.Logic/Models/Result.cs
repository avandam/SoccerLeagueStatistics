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
        public Season Season { get; private set; }
        public Club Club { get; private set; }
        public int Place { get; private set; }
        public int Wins { get; private set; }
        public int Draws { get; private set; }
        public int Losses { get; private set; }

        public int GoalsFor { get; private set; }
        public int GoalsAgainst { get; private set; }

        public Result(string id, Season season, Club club, int place, int wins, int draws, int losses, int goalsFor, int goalsAgainst)
        {
            Id = id;
            Season = season;
            Club = club;
            Place = place;
            Wins = wins;
            Draws = draws;
            Losses = losses;
            GoalsFor = goalsFor;
            GoalsAgainst = goalsAgainst;
        }

        public Result(Season season, Club club, int place, int wins, int draws, int losses, int goalsFor, int goalsAgainst)
            : this(Guid.NewGuid().ToString(), season, club, place, wins, draws, losses, goalsFor, goalsAgainst)
        {
        }
    }
}
