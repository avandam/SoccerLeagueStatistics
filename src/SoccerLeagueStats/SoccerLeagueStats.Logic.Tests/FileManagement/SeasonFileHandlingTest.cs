using System.IO;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SoccerLeagueStats.Logic.FileManagement;
using SoccerLeagueStats.Logic.Models;

namespace SoccerLeagueStats.Logic.Tests.FileManagement
{
    [TestClass()]
    public class SeasonFileHandlingTest
    {
        [TestMethod()]
        public void SaveAndLoadSeasonTest()
        {
            string compId = string.Empty;
            string id = string.Empty;
            try
            {
                Competition competition = new Competition("Eredivisie", "Nederland", 1);
                compId = competition.Id;
                CompetitionFileHandler.SaveCompetition(competition);
                Season season = new Season("2018-2019", 3, competition);
                id = season.Id;
                SeasonFileHandler.SaveSeason(season);
                Assert.IsTrue(File.Exists(Path.Combine(Settings.BaseDirectoryName, "Seasons", id)));
                Season newSeason = SeasonFileHandler.GetSeason(id);
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
    }
}
