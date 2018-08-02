using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SoccerLeagueStats.Logic.Models
{
    public class Competition
    {
        public string Id { get; private set; }
        public string Country { get; private set; }
        public string Name { get; private set; }
        public int Level { get; private set; }
        public int DefaultPointsForAWin { get; private set; }

        public Competition(string id, string name, string country, int level, int defaultPointsForAWin)
        {
            Id = id;
            Country = country;
            Name = name;
            Level = level;
            DefaultPointsForAWin = defaultPointsForAWin;
        }

        public Competition(string name, string country, int level) 
            : this (Guid.NewGuid().ToString(), name, country, level, 3)
        {
        }

        public override string ToString()
        {
            return Name;
        }
    }
}
