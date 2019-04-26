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
            var filePath = Path.Combine(Settings.BaseDirectoryName, "Clubs", club.Id);
            DeleteFileIfExists(filePath);
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

        public static void SaveCompetition(Competition competition)
        {
            CreateSpecificDirectory("Competitions");
            var filePath = Path.Combine(Settings.BaseDirectoryName, "Competitions", competition.Id);
            DeleteFileIfExists(filePath);
            var lines = CreateCompetitionLines(competition);
            File.AppendAllLines(filePath, lines);
        }

        private static List<string> CreateCompetitionLines(Competition competition)
        {
            var lines = new List<string>();
            lines.Add(competition.Id);
            lines.Add(competition.Name);
            lines.Add(competition.Country);
            lines.Add(competition.Level.ToString());
            lines.Add(competition.DefaultPointsForAWin.ToString());
            return lines;
        }

        public static void SaveSeason(Season season)
        {
            CreateSpecificDirectory("Seasons");
            var filePath = Path.Combine(Settings.BaseDirectoryName, "Seasons", season.Id);
            DeleteFileIfExists(filePath);
            var lines = CreateSeasonLines(season);
            File.AppendAllLines(filePath, lines);
        }

        private static List<string> CreateSeasonLines(Season season)
        {
            var lines = new List<string>();
            lines.Add(season.Id);
            lines.Add(season.Name);
            lines.Add(season.PointsForAWin.ToString());
            lines.Add(season.Competition.Id);
            return lines;
        }

        public static void SaveResult(Result result)
        {
            CreateSpecificDirectory("Results");
            var filePath = Path.Combine(Settings.BaseDirectoryName, "Results", result.Id);
            DeleteFileIfExists(filePath);
            var lines = CreateResultLines(result);
            File.AppendAllLines(filePath, lines);
        }

        private static List<string> CreateResultLines(Result result)
        {
            var lines = new List<string>();
            lines.Add(result.Id);
            lines.Add(result.Place.ToString());
            lines.Add(result.Wins.ToString());
            lines.Add(result.Draws.ToString());
            lines.Add(result.Losses.ToString());
            lines.Add(result.GoalsFor.ToString());
            lines.Add(result.GoalsAgainst.ToString());
            lines.Add(result.Season.Id);
            lines.Add(result.Club.Id);
            return lines;
        }
    }
}
