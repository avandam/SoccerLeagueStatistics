using System.Collections.Generic;
using System.IO;
using SoccerLeagueStats.Logic.Models;

namespace SoccerLeagueStats.Logic.FileManagement
{
    public static class FileWriter
    {
        private static void CreateDirectory(string path)
        {
            if (!Directory.Exists(path))
            {
                Directory.CreateDirectory(path);
            }
        }

        private static void CreateSpecificDirectory(string name)
        {
            CreateDirectory(Settings.BaseDirectoryName);
            CreateDirectory(Path.Combine(Settings.BaseDirectoryName, name));
        }

        private static void DeleteFileIfExists(string path)
        {
            if (File.Exists(path))
            {
                File.Delete(path);
            }
        }

        public static void SaveClub(Club club)
        {
            CreateSpecificDirectory("Clubs");
            string filePath = Path.Combine(Settings.BaseDirectoryName, "Clubs", club.Id);
            DeleteFileIfExists(filePath);
            List<string> lines = CreateClubLines(club);
            File.AppendAllLines(filePath, lines);
        }

        private static List<string> CreateClubLines(Club club)
        {
            List<string> lines = new List<string>();
            lines.Add(club.Id);
            lines.Add(club.Name);
            lines.Add(club.Country);
            lines.Add(club.MainClub?.Id ?? "None");
            return lines;
        }
    }
}
