using System;
using System.Collections.Generic;

namespace SoccerLeagueStats.Logic.Models
{
    public class Competition
    {
        /// <summary>
        /// Guid for the competition
        /// </summary>
        public string Id { get; private set; }
        /// <summary>
        /// The country the competition is held in
        /// </summary>
        public string Country { get; private set; }
        /// <summary>
        /// The name of the competition
        /// </summary>
        public string Name { get; private set; }
        /// <summary>
        /// The level of the competition (where the highest level is 1)
        /// </summary>
        public int Level { get; private set; }
        /// <summary>
        ///  The default number of points for a win, usually 3
        /// </summary>
        public int DefaultPointsForAWin { get; private set; }

        public List<Season> Seasons { get; private set; }

        public Competition(string id, string name, string country, int level, int defaultPointsForAWin)
        {
            Id = id;
            Country = country;
            Name = name;
            Level = level;
            DefaultPointsForAWin = defaultPointsForAWin;
            Seasons = new List<Season>();
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
