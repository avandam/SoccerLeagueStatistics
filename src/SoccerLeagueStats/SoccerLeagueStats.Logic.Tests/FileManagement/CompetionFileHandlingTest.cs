using System.IO;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SoccerLeagueStats.Logic.FileManagement;
using SoccerLeagueStats.Logic.Models;

namespace SoccerLeagueStats.Logic.Tests.FileManagement
{
    [TestClass()]
    public class CompetionFileHandlingTest
    {
        [TestMethod()]
        public void SaveAndLoadCompetitionTest()
        {
            string id = string.Empty;
            try
            {
                Competition competition = new Competition("Eredivisie", "Nederland", 1);
                id = competition.Id;
                CompetitionFileHandler.SaveCompetition(competition);
                Assert.IsTrue(File.Exists(Path.Combine(Settings.BaseDirectoryName, "Competitions", id)));
                Competition newCompetition = CompetitionFileHandler.GetCompetition(id);
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
    }
}
