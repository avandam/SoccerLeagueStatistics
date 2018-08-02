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
