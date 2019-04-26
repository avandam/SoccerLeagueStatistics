using System.IO;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SoccerLeagueStats.Logic.FileManagement;
using SoccerLeagueStats.Logic.Models;

namespace SoccerLeagueStats.Logic.Tests.FileManagement
{
    [TestClass()]
    public class ClubFileHandlingTest
    {
        [TestMethod()]
        public void SaveAndLoadClubTest()
        {
            string id = string.Empty;
            try
            {
                Club club = new Club("PSV", "Nederland");
                id = club.Id;
                ClubFileHandler.SaveClub(club);
                Assert.IsTrue(File.Exists(Path.Combine(Settings.BaseDirectoryName, "Clubs", id)));
                Club newClub = ClubFileHandler.GetClub(id);
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
                ClubFileHandler.SaveClub(club);
                Assert.IsTrue(File.Exists(Path.Combine(Settings.BaseDirectoryName, "Clubs", id)));
                Club newClub = ClubFileHandler.GetClub(id);
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
    }
}
