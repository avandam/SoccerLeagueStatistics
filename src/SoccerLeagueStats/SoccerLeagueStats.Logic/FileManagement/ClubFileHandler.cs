using System.Collections.Generic;
using System.IO;
using System.Linq;
using SoccerLeagueStats.Logic.Models;

namespace SoccerLeagueStats.Logic.FileManagement
{
    public static class ClubFileHandler
    {
        public static Club GetClub(string id)
        {
            string filePath = Path.Combine(Settings.BaseDirectoryName, "Clubs", id);

            var lines = File.ReadAllLines(filePath).ToList();
            var club = new Club(lines[0], lines[1], lines[2]);

            string[] alternativeNames = lines[3].Split(';');
            foreach (string alternativeName in alternativeNames)
            {
                club.AddAlternativeName(alternativeName);
            }

            return club;
        }

        public static List<Club> GetAllClubs()
        {
            var path = Path.Combine(Settings.BaseDirectoryName, "Clubs");
            return Directory.GetFiles(path).Select(GetClub).ToList();
        }

        public static void SaveClub(Club club)
        {
            FileHelper.CreateSpecificDirectory("Clubs");
            var filePath = Path.Combine(Settings.BaseDirectoryName, "Clubs", club.Id);
            FileHelper.DeleteFileIfExists(filePath);
            var lines = CreateClubLines(club);
            File.AppendAllLines(filePath, lines);
        }

        private static List<string> CreateClubLines(Club club)
        {
            var lines = new List<string>();
            lines.Add(club.Id);
            lines.Add(club.Name);
            lines.Add(club.Country);
            lines.Add(string.Join(";", club.AlternativeNames));
            return lines;
        }

    }
}
