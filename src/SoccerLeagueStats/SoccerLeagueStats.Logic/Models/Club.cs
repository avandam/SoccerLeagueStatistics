using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SoccerLeagueStats.Logic.Models
{
    public class Club
    {
        public string Id { get; private set; }
        public string Name { get; private set; }
        public string Country { get; private set; }
        public Club MainClub { get; private set; }

        public Club()
        {
            
        }

        public Club(string id, string name, string country, Club mainClub)
        {
            Id = id;
            Name = name;
            Country = country;
            MainClub = mainClub;
        }

        public Club(string id, string name, string country)
            : this (id, name, country, null)
        {
        }

        public Club(string name, string country, Club mainClub)
            : this(Guid.NewGuid().ToString(), name, country, mainClub)
        {
        }

        public Club(string name, string country)
            : this(Guid.NewGuid().ToString(), name, country, null)
        {
        }

        public override string ToString()
        {
            return MainClub?.ToString() ?? Name;
        }
    }
}
