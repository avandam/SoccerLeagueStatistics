using System.IO;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SoccerLeagueStats.Logic.FileManagement;
using SoccerLeagueStats.Logic.Models;

namespace SoccerLeagueStats.Logic.Tests.FileManagement
{
    [TestClass()]
    public class FileHandlingTests
    {
        [TestMethod()]
        public void SaveAndLoadResultTest()
        {
            string compId = string.Empty;
            string seasonId = string.Empty;
            string clubId = string.Empty;
            string id = string.Empty;

            try
            {
                Club club = new Club("PSV", "Nederland");
                clubId = club.Id;
                ClubFileHandler.SaveClub(club);

                Competition competition = new Competition("Eredivisie", "Nederland", 1);
                compId = competition.Id;
                CompetitionFileHandler.SaveCompetition(competition);

                Season season = new Season("2018-2019", 3, competition);
                seasonId = season.Id;
                SeasonFileHandler.SaveSeason(season);

                Result result = new Result(1, 25, 9, 0, 100, 10, season, club);
                id = result.Id;
                ResultFileHandler.SaveResult(result);
                Assert.IsTrue(File.Exists(Path.Combine(Settings.BaseDirectoryName, "Results", id)));

                Result newResult = ResultFileHandler.GetResult(id);
                Assert.AreEqual(1, newResult.Place);
                Assert.AreEqual(25, newResult.Wins);
                Assert.AreEqual(9, newResult.Draws);
                Assert.AreEqual(0, newResult.Losses);
                Assert.AreEqual(100, newResult.GoalsFor);
                Assert.AreEqual(10, newResult.GoalsAgainst);
                Assert.AreEqual(seasonId, newResult.Season.Id);
                Assert.AreEqual(clubId, newResult.Club.Id);
            }
            finally
            {
                if (File.Exists(Path.Combine(Settings.BaseDirectoryName, "Competitions", compId)))
                {
                    File.Delete(Path.Combine(Settings.BaseDirectoryName, "Competitions", compId));
                }

                if (File.Exists(Path.Combine(Settings.BaseDirectoryName, "Seasons", seasonId)))
                {
                    File.Delete(Path.Combine(Settings.BaseDirectoryName, "Seasons", seasonId));
                }

                if (File.Exists(Path.Combine(Settings.BaseDirectoryName, "Clubs", clubId)))
                {
                    File.Delete(Path.Combine(Settings.BaseDirectoryName, "Clubs", clubId));
                }

                if (File.Exists(Path.Combine(Settings.BaseDirectoryName, "Results", id)))
                {
                    File.Delete(Path.Combine(Settings.BaseDirectoryName, "Results", id));
                }
            }
        }

    }
}