using SoccerLeagueStats.Logic.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace SoccerLeagueStats.Logic.FileManagement
{
    public static class FileReader
    {
        //TODO: Use All* variables to ensure uniqueness
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

        public static Season GetSeason(string id)
        {
            string filePath = Path.Combine(Settings.BaseDirectoryName, "Seasons", id);

            var lines = File.ReadAllLines(filePath).ToList();
            var season = new Season(lines[0], lines[1], Convert.ToInt32(lines[2]), GetCompetition(lines[3]));

            return season;
        }

        public static List<Season> GetAllSeasons()
        {
            var path = Path.Combine(Settings.BaseDirectoryName, "Seasons");
            return Directory.GetFiles(path).Select(GetSeason).ToList();
        }

        public static Result GetResult(string id)
        {
            string filePath = Path.Combine(Settings.BaseDirectoryName, "Results", id);

            var lines = File.ReadAllLines(filePath).ToList();
            var result = new Result(lines[0], Convert.ToInt32(lines[1]), Convert.ToInt32(lines[2]), Convert.ToInt32(lines[3]), Convert.ToInt32(lines[4]), Convert.ToInt32(lines[5]), Convert.ToInt32(lines[6]), GetSeason(lines[7]), GetClub(lines[8]));

            return result;
        }

        public static List<Result> GetAllResults()
        {
            var path = Path.Combine(Settings.BaseDirectoryName, "Results");
            return Directory.GetFiles(path).Select(GetResult).ToList();
        }
    }
}
