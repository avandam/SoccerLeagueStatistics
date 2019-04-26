using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using SoccerLeagueStats.Logic.Models;

namespace SoccerLeagueStats.Logic.FileManagement
{
    public class SeasonFileHandler
    {
        public static Season GetSeason(string id)
        {
            string filePath = Path.Combine(Settings.BaseDirectoryName, "Seasons", id);

            var lines = File.ReadAllLines(filePath).ToList();
            var season = new Season(lines[0], lines[1], Convert.ToInt32(lines[2]), CompetitionFileHandler.GetCompetition(lines[3]));

            return season;
        }

        public static List<Season> GetAllSeasons()
        {
            var path = Path.Combine(Settings.BaseDirectoryName, "Seasons");
            return Directory.GetFiles(path).Select(GetSeason).ToList();
        }

        public static void SaveSeason(Season season)
        {
            FileHelper.CreateSpecificDirectory("Seasons");
            var filePath = Path.Combine(Settings.BaseDirectoryName, "Seasons", season.Id);
            FileHelper.DeleteFileIfExists(filePath);
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
    }
}
