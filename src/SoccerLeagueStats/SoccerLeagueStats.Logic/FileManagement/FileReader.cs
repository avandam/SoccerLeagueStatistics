using SoccerLeagueStats.Logic.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SoccerLeagueStats.Logic.FileManagement
{
    public static class FileReader
    {
        public static Club GetClub(string id)
        {
            string filePath = Path.Combine(Settings.BaseDirectoryName, "Clubs", id);

            List<string> lines = File.ReadAllLines(filePath).ToList();
            Club club;
            if (lines[3] == "None")
            {
                club = new Club(lines[0], lines[1], lines[2]);
            }
            else
            {
                club = new Club(lines[0], lines[1], lines[2], GetClub(lines[3]));
            }

            return club;
        }

        public static List<Club> GetAllClubs(string countryName)
        {
            List<Club> clubs = new List<Club>();
            string path = Path.Combine(Settings.BaseDirectoryName, "Clubs");
            foreach (string clubId in Directory.GetFiles(path))
            {
                clubs.Add(GetClub(clubId));
            }
            return clubs;
        }
    }
}
