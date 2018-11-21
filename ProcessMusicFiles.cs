using MusicFactory;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Simile
{
    public class ProcessMusicFiles
    {
        public void Process()
        {
            DirectoryProcess dProcess = new DirectoryProcess();
            List<DirectoryInfo> dirList = dProcess.GetDirectories();
            FileInfo[] musicFiles = null;
            TagLib.File tagMusicFile = null;

            Simile.Algorithm.AlgorithmFactory algFactory = new Algorithm.AlgorithmFactory();

            string musicTitle = "";
            string musicFileName = "";
            foreach (DirectoryInfo dir in dirList)
            {


                musicFiles = dir.GetFiles(GlobalUserConfig.FileType);
                             

                foreach (FileInfo finfo in musicFiles)
                {
                    try
                    {
                        FileInfo musicFile = finfo;
                        tagMusicFile = TagLib.File.Create(musicFile.FullName);
                        musicFileName = musicFile.Name.Trim(); // tagMusicFile.Name.Trim();
                        
                        //if tag is null then avoid the file and get next file.
                        if (tagMusicFile.Tag == null) continue;

                        //from music file name remove file extention.
                        musicFileName = algFactory.GetFileNameRemovingFileExtention(musicFileName);

                        var songName = (!string.IsNullOrEmpty(tagMusicFile.Tag.Title)) ? tagMusicFile.Tag.Title.Trim() : musicFileName;

                        if (!string.IsNullOrEmpty(songName)) //tagMusicFile.Tag.Title))
                        {
                            //if title has prefix number then remove it.
                            tagMusicFile.Tag.Title = algFactory.GetPrefixNumberRemoveTitle(songName); //tagMusicFile.Tag.Title.Trim());

                            ////if title has prefix artist or band name then remove it.                        
                            tagMusicFile = algFactory.RemovePrefixArtistOrBandFromTitle(tagMusicFile);
                        }

                        //if there is no title, then assign file name to title.                     
                        if (string.IsNullOrEmpty(tagMusicFile.Tag.Title))
                        {
                            tagMusicFile.Tag.Title = algFactory.GetMusicTitle(musicFile,tagMusicFile);
                            tagMusicFile.Save();
                        }
                        
                        #region obsolete
                        ////assign title to file name.
                        // musicFile = algFactory.TitleToFileName(musicFile);
                        // tagMusicFile = TagLib.File.Create(musicFile.FullName);
                        // musicFileName = musicFile.Name.Trim();
                        // //from music file name remove file extention.
                        // musicFileName = algFactory.GetFileNameRemovingFileExtention(musicFileName);

                        //if artist included in file name then remove artist, and keep music title as file name.



                        //Create folder for artist and move files.
                         algFactory.CreateArtistFolderAndMoveFile(musicFile);
                        #endregion
                        
                    }
                    catch (Exception errMsg)
                    {
                        throw errMsg;
                    }


                }
            }
        }


    }
}
