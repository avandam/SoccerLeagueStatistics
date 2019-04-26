using System.IO;

namespace SoccerLeagueStats.Logic.FileManagement
{
    public static class FileHelper
    {
        public  static void CreateDirectory(string path)
        {
            if (!Directory.Exists(path))
            {
                Directory.CreateDirectory(path);
            }
        }

        public static void CreateSpecificDirectory(string name)
        {
            CreateDirectory(Settings.BaseDirectoryName);
            CreateDirectory(Path.Combine(Settings.BaseDirectoryName, name));
        }

        public static void DeleteFileIfExists(string path)
        {
            if (File.Exists(path))
            {
                File.Delete(path);
            }
        }
    }
}
