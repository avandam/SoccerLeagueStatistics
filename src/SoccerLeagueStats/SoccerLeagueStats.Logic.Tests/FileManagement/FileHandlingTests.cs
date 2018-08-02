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
        public void SaveAndLoadClubTest()
        {
            string id = string.Empty;
            try
            {
                Club club = new Club("PSV", "Nederland");
                id = club.Id;
                FileWriter.SaveClub(club);
                Assert.IsTrue(File.Exists(Path.Combine(Settings.BaseDirectoryName, "Clubs", id)));
                Club newClub = FileReader.GetClub(id);
                Assert.AreEqual("PSV", newClub.Name);
                Assert.AreEqual("Nederland", newClub.Country);
                Assert.AreEqual(0, club.AlternativeNames.Count);
            }
            finally
            {
                if (File.Exists(Path.Combine(Settings.BaseDirectoryName, "Clubs", id)))
                {
                    File.Delete(Path.Combine(Settings.BaseDirectoryName, "Clubs", id));
                }
            }
        }

        [TestMethod()]
        public void SaveAndLoadClubWithMainClubTest()
        {
            string id = string.Empty;
            try
            {
                Club club = new Club("PSV", "Nederland");
                club.AddAlternativeName("PSV2");
                club.AddAlternativeName("PSV3");
                id = club.Id;
                FileWriter.SaveClub(club);
                Assert.IsTrue(File.Exists(Path.Combine(Settings.BaseDirectoryName, "Clubs", id)));
                Club newClub = FileReader.GetClub(id);
                Assert.AreEqual("PSV", newClub.Name);
                Assert.AreEqual("Nederland", newClub.Country);
                Assert.AreEqual("PSV2", newClub.AlternativeNames[0]);
                Assert.AreEqual("PSV3", newClub.AlternativeNames[1]);
            }
            finally
            {
                if (File.Exists(Path.Combine(Settings.BaseDirectoryName, "Clubs", id)))
                {
                    File.Delete(Path.Combine(Settings.BaseDirectoryName, "Clubs", id));
                }

            }
        }

        [TestMethod()]
        public void SaveAndLoadCompetitionTest()
        {
            string id = string.Empty;
            try
            {
                Competition competition = new Competition("Eredivisie", "Nederland", 1);
                id = competition.Id;
                FileWriter.SaveCompetition(competition);
                Assert.IsTrue(File.Exists(Path.Combine(Settings.BaseDirectoryName, "Competitions", id)));
                Competition newCompetition = FileReader.GetCompetition(id);
                Assert.AreEqual("Eredivisie", newCompetition.Name);
                Assert.AreEqual("Nederland", newCompetition.Country);
                Assert.AreEqual(1, newCompetition.Level);
                Assert.AreEqual(3, newCompetition.DefaultPointsForAWin);
            }
            finally
            {
                if (File.Exists(Path.Combine(Settings.BaseDirectoryName, "Competitions", id)))
                {
                    File.Delete(Path.Combine(Settings.BaseDirectoryName, "Competitions", id));
                }
            }
        }

        [TestMethod()]
        public void SaveAndLoadSeasonTest()
        {
            string compId = string.Empty;
            string id = string.Empty;
            try
            {
                Competition competition = new Competition("Eredivisie", "Nederland", 1);
                compId = competition.Id;
                FileWriter.SaveCompetition(competition);
                Season season = new Season("2018-2019", 3, competition);
                id = season.Id;
                FileWriter.SaveSeason(season);
                Assert.IsTrue(File.Exists(Path.Combine(Settings.BaseDirectoryName, "Seasons", id)));
                Season newSeason = FileReader.GetSeason(id);
                Assert.AreEqual("2018-2019", newSeason.Name);
                Assert.AreEqual(3, newSeason.PointsForAWin);
                Assert.AreEqual(compId, newSeason.Competition.Id);
            }
            finally
            {
                if (File.Exists(Path.Combine(Settings.BaseDirectoryName, "Competitions", compId)))
                {
                    File.Delete(Path.Combine(Settings.BaseDirectoryName, "Competitions", compId));
                }


                if (File.Exists(Path.Combine(Settings.BaseDirectoryName, "Seasons", id)))
                {
                    File.Delete(Path.Combine(Settings.BaseDirectoryName, "Seasons", id));
                }
            }
        }

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
                FileWriter.SaveClub(club);

                Competition competition = new Competition("Eredivisie", "Nederland", 1);
                compId = competition.Id;
                FileWriter.SaveCompetition(competition);

                Season season = new Season("2018-2019", 3, competition);
                seasonId = season.Id;
                FileWriter.SaveSeason(season);

                Result result = new Result(1, 25, 9, 0, 100, 10, season, club);
                id = result.Id;
                FileWriter.SaveResult(result);
                Assert.IsTrue(File.Exists(Path.Combine(Settings.BaseDirectoryName, "Results", id)));

                Result newResult = FileReader.GetResult(id);
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