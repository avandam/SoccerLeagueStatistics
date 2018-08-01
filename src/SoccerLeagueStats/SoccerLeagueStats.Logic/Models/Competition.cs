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

        public Competition(string id, string country, string name, int level)
        {
            Id = id;
            Country = country;
            Name = name;
            Level = level;
            DefaultPointsForAWin = 3;
        }

        public Competition(string country, string name, int level) 
            : this (Guid.NewGuid().ToString(), country, name, level)
        {
        }

        public override string ToString()
        {
            return Name;
        }
    }
}
