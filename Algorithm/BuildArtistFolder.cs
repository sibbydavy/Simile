using MusicFactory;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Simile.Algorithm
{
   public class BuildArtistFolder
    {
       public void Process(FileInfo musicFile)
       {
           DirectoryProcess dirProcess = new DirectoryProcess();

           string currentFilePathAndName = "";
           string musicFileName = "";
           string artistName = "";
           TagLib.File tagMusicFile = null;
           tagMusicFile = TagLib.File.Create(musicFile.FullName);
           string artistFolderPath = "";

           musicFileName = musicFile.Name.Trim();
           currentFilePathAndName = musicFile.FullName.Trim();
                 
           List<string> artists = GlobalUserConfig.GetArtist(tagMusicFile);

           foreach (string val in artists)
           {
               if (IsValidArtist(val.Trim()))
               {
                   artistName = string.IsNullOrEmpty(artistName) ? val.Trim()
                       : artistName + " & " + val;
               }

                //Create folder.
               if (!dirProcess.HasFolderExists(GlobalUserConfig.MusicFilesFolder + artistName))
               {
                   System.IO.Directory.CreateDirectory(GlobalUserConfig.MusicFilesFolder + artistName);               
               }

               artistFolderPath = GlobalUserConfig.MusicFilesFolder + artistName + "\\";
             
               //move music files to folder.
               //check file exists.
               if (!File.Exists(artistFolderPath + musicFileName))
               {
                   System.IO.File.Move(currentFilePathAndName, artistFolderPath + musicFileName);
               }             
           }       
       }

       bool IsValidArtist(string artistName)
       {
           bool yes = false;

           yes = !String.IsNullOrEmpty(artistName);


           return yes;
       
       }

      

    }
}
