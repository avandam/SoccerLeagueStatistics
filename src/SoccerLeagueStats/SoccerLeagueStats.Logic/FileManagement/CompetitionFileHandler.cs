using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using SoccerLeagueStats.Logic.Models;

namespace SoccerLeagueStats.Logic.FileManagement
{
    public class CompetitionFileHandler
    {
        public static Competition GetCompetition(string id)
        {
            string filePath = Path.Combine(Settings.BaseDirectoryName, "Competitions", id);

            var lines = File.ReadAllLines(filePath).ToList();
            var competition = new Competition(lines[0], lines[1], lines[2], Convert.ToInt32(lines[3]), Convert.ToInt32(lines[4]));

            return competition;
        }

        public static List<Competition> GetAllCompetitions()
        {
            var path = Path.Combine(Settings.BaseDirectoryName, "Competitions");
            return Directory.GetFiles(path).Select(GetCompetition).ToList();
        }

        public static void SaveCompetition(Competition competition)
        {
            FileHelper.CreateSpecificDirectory("Competitions");
            var filePath = Path.Combine(Settings.BaseDirectoryName, "Competitions", competition.Id);
            FileHelper.DeleteFileIfExists(filePath);
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
    }
}
