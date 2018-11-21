using MusicFactory;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Simile.DBProcess
{
    public class MusicArtist
    {
        string[] obsoleteValues = null;

        public MusicArtist()
        {
            this.obsoleteValues = new string[2];
            BuildInValidArtist();
        }

        /// <summary>
        /// Get artists from the music tag file.
        /// </summary>
        /// <param name="tagMusicFile"></param>
        /// <returns>Return list of artists</returns>
        public List<string> GetArtist(TagLib.File tagMusicFile)
        {
            List<string> artists = new List<string>();

            //Tag has two properties such that 'AlbumArtists' and 'Artists'.
            // Identifies in which tag artists details contains.
            foreach (var val in obsoleteValues)
            {
                if (tagMusicFile.Tag.AlbumArtists != null && tagMusicFile.Tag.AlbumArtists.Count() > 0)
                {
                    artists = tagMusicFile.Tag.AlbumArtists.Where(r => r.ToUpper() != val).ToList();
                }
                if (tagMusicFile.Tag.Artists != null && tagMusicFile.Tag.Artists.Count() > 0)
                {
                    artists = tagMusicFile.Tag.Artists.Where(r => r.ToUpper() != val).ToList();
                }
            }

            return artists;
        }

        public void SaveArtists()
       {            

            FileInfo[] musicFiles = null;
            TagLib.File tagMusicFile = null;
            //string album = "";
            List<string> artists = new List<string>();

           //  DirectoryProcess dProcess = new DirectoryProcess();
            List<DirectoryInfo> dirList = new DirectoryProcess().GetDirectories();

            //Iterate parent directory and all its sub directories.
            foreach (DirectoryInfo dir in dirList)
            {
                //Get music files from the this directory.
                musicFiles = dir.GetFiles(GlobalUserConfig.FileType);

                //Iterate music files.
                foreach (FileInfo musicFile in musicFiles)
                {
                    try
                    {            
                        //Create tag music file from music file.
                        tagMusicFile = TagLib.File.Create(musicFile.FullName);

                        artists = GetArtist(tagMusicFile);

                    }
                    catch (Exception errMsg)
                    {
                        throw errMsg;
                    }
                }
                
            }
       }

        void BuildInValidArtist()
        {
            this.obsoleteValues[0] = "UNKNOWN ARTIST";
            this.obsoleteValues[1] = "ARTIST";
        }

        public bool HasArtistInExistsInDB(string artistName)
        {
            bool yes = false;

            using (var modelContext = new MusicPlayEntities3())
            {
                try
                {
                    yes = modelContext.MusicArtists.Where(r => r.ArtistName.ToUpper() == artistName.ToUpper()).Count() > 0;
                }
                catch (Exception errMsg)
                {
                    throw errMsg;
                }
            }
            return yes;
        }

        public bool IsValidArtistTag(TagLib.File musicFile)
        {
            //check tag contain artist.
            bool isValidArtistTag = false;
            isValidArtistTag = (musicFile.Tag.AlbumArtists != null && musicFile.Tag.AlbumArtists.Count() > 0);
            isValidArtistTag = !isValidArtistTag ? (musicFile.Tag.Artists != null && musicFile.Tag.Artists.Count() > 0) : false;

            return isValidArtistTag;
        }

     
         

    }
}
