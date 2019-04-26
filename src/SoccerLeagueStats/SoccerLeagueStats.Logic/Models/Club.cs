using System;
using System.Collections.Generic;

namespace SoccerLeagueStats.Logic.Models
{
    public class Club
    {
        /// <summary>
        /// The id of the club
        /// </summary>
        public string Id { get; private set; }
        /// <summary>
        /// The name of the club
        /// </summary>
        public string Name { get; private set; }
        /// <summary>
        /// The country the club is playing in
        /// </summary>
        public string Country { get; private set; }
        /// <summary>
        /// The city the club is playing in
        /// </summary>
        public string City { get; private set; }
        /// <summary>
        /// Alternative names for the club
        /// </summary>
        public List<string> AlternativeNames { get; private set; }

        public Club(string id, string name, string country)
        {
            Id = id;
            Name = name;
            Country = country;
            AlternativeNames = new List<string>();
        }

        public Club(string name, string country)
            : this(Guid.NewGuid().ToString(), name, country)
        {
        }

        public override string ToString()
        {
            return Name;
        }

        public void AddAlternativeName(string alternativeName)
        {
            if (!AlternativeNames.Contains(alternativeName))
            {
                AlternativeNames.Add(alternativeName);
            }
        }
    }
}
