using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MusicFactory
{
    public class GlobalUserConfig
    {
        static String parentFolderPath;
        static String searchPattern;
        static string tmpFolder;
        static string fileType;
        static string fileExtention;
        static string musicFilesFolder;
        static string tempFolder;

        public enum ArtisTagLookUp
        {
            Unknown = 0,
            AlbumArtists = 1,
            Artists = 2
        }

        public static String TempFolder
        {
            get { return tempFolder; }
            set { tempFolder = value; }
        }

        public static String ParentFolderPath
        {
            get { return parentFolderPath; }
            set { parentFolderPath = value; }
        }

          [Obsolete]
        public static String SearchPattern
        {
            get { return searchPattern; }
        }

      
        public GlobalUserConfig()
        {
            GlobalUserConfig.searchPattern = "*.wma";
            GlobalUserConfig.fileType = "*.wma";
            GlobalUserConfig.fileExtention = ".wma";
            GlobalUserConfig.musicFilesFolder = @"D:\song\Songs\Wow I am here\";
            GlobalUserConfig.tempFolder = AppDomain.CurrentDomain.BaseDirectory + @"Tmp\";

        }

        public static string FileType
        {
            get
            {
                return fileType;
            }
        }

        public static string FileExtention
        {
            get
            {
                return fileExtention;
            }
        }

        public static string MusicFilesFolder
        {
            get
            {
                return musicFilesFolder;
            }
        }

        public static List<string> GetArtist(TagLib.File tagMusicFile)
        {
            
            string[] obsoleteValues = new string[2];
            obsoleteValues[0] = "UNKNOWN ARTIST";
            obsoleteValues[1] = "ARTIST";

            bool isAlbumArtist =  tagMusicFile.Tag.AlbumArtists != null;            
            isAlbumArtist = isAlbumArtist ? tagMusicFile.Tag.AlbumArtists.Count() > 0 : false;

            bool isArtist = tagMusicFile.Tag.Artists != null;
            isArtist = isArtist ? tagMusicFile.Tag.Artists.Count() > 0 : false;

            List<string> artists = new List<string>();

            foreach (var val in obsoleteValues)
            {
                if (isAlbumArtist)
                {
                    artists = tagMusicFile.Tag.AlbumArtists.Where(r => r.ToUpper() != val).ToList();
                }
                if (isArtist)
                {
                    artists = tagMusicFile.Tag.Artists.Where(r => r.ToUpper() != val).ToList();
                }
            }


            return artists;
        
        }
    }
}
