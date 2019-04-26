using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using SoccerLeagueStats.Logic.Models;

namespace SoccerLeagueStats.Logic.FileManagement
{
    public class ResultFileHandler
    {
        public static Result GetResult(string id)
        {
            string filePath = Path.Combine(Settings.BaseDirectoryName, "Results", id);

            var lines = File.ReadAllLines(filePath).ToList();
            var result = new Result(lines[0], Convert.ToInt32(lines[1]), Convert.ToInt32(lines[2]), Convert.ToInt32(lines[3]), Convert.ToInt32(lines[4]), Convert.ToInt32(lines[5]), Convert.ToInt32(lines[6]), SeasonFileHandler.GetSeason(lines[7]), ClubFileHandler.GetClub(lines[8]));

            return result;
        }

        public static List<Result> GetAllResults()
        {
            var path = Path.Combine(Settings.BaseDirectoryName, "Results");
            return Directory.GetFiles(path).Select(GetResult).ToList();
        }

        public static void SaveResult(Result result)
        {
            FileHelper.CreateSpecificDirectory("Results");
            var filePath = Path.Combine(Settings.BaseDirectoryName, "Results", result.Id);
            FileHelper.DeleteFileIfExists(filePath);
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
