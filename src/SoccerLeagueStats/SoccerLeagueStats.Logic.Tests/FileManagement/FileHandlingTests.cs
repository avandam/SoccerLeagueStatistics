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
                Assert.IsNull(newClub.MainClub);
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
            string childId = string.Empty;
            try
            {
                Club club = new Club("PSV", "Nederland");
                Club childClub = new Club("PSV2", "Nederland", club);
                id = club.Id;
                childId = childClub.Id;
                FileWriter.SaveClub(club);
                FileWriter.SaveClub(childClub);
                Assert.IsTrue(File.Exists(Path.Combine(Settings.BaseDirectoryName, "Clubs", id)));
                Club newClub = FileReader.GetClub(childId);
                Assert.AreEqual("PSV2", newClub.Name);
                Assert.AreEqual("Nederland", newClub.Country);
                Assert.AreEqual("PSV", newClub.MainClub.Name);
            }
            finally
            {
                if (File.Exists(Path.Combine(Settings.BaseDirectoryName, "Clubs", id)))
                {
                    File.Delete(Path.Combine(Settings.BaseDirectoryName, "Clubs", id));
                }

                if (File.Exists(Path.Combine(Settings.BaseDirectoryName, "Clubs", childId)))
                {
                    File.Delete(Path.Combine(Settings.BaseDirectoryName, "Clubs", childId));
                }
            }
        }
    }
}