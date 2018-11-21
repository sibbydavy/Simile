using MusicFactory;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Simile.Algorithm
{
   public class AlgorithmFactory
    {

       public string GetPrefixNumberRemoveTitle(string title)
       {
           string newTitle = "";
           Simile.Algorithm.RemovePreFixNumberString removPreFixNumString = new Algorithm.RemovePreFixNumberString();
         
           //check title has any prefix number value.
           bool hasGotIntegerPrefix = removPreFixNumString.HasNameContainPrefixIntegerValue(title);

           //if file name has prefix number value, then remove it.
           if (hasGotIntegerPrefix)
           {
               //after removing prefix value from file name, then assign to title.
               newTitle = removPreFixNumString.GetMusicName(title);
           }
           else
           {
               newTitle = title;
           }


           return newTitle;
       }

       public string GetMusicTitle( FileInfo musicFile,TagLib.File tagMusicFile)
       {
           string title = "";
           string fileName = "";

           fileName = musicFile.Name.Trim();
           title = fileName;

           Simile.Algorithm.RemovePreFixNumberString removPreFixNumString = new Algorithm.RemovePreFixNumberString();
           
           //check file name has any prefix number value.
           bool hasGotIntegerPrefix = removPreFixNumString.HasNameContainPrefixIntegerValue(fileName);

           //if file name has prefix number value, then remove it.
           if (hasGotIntegerPrefix)
           {
               //after removing prefix value from file name, then assign to title.
               title = removPreFixNumString.GetMusicName(fileName);
           }

           //If file name has prefix artist or band name remove it.
           TagLib.File result = RemovePrefixArtistOrBand(musicFile,tagMusicFile);

           if (!string.IsNullOrEmpty(result.Tag.Title))
           {
               title = result.Tag.Title.Trim();
           }
           //Remoe speical character.
           //RemoveSpecialCharacter remSplChar = new RemoveSpecialCharacter();
           //remSplChar.RemoveSpeicalChar(tagMusicFile.Tag.Title.Trim());

           //else
           //{
           //    title = fileName;
           //}

           title = title.First().ToString().ToUpper() + String.Join("", title.Skip(1));

           return title;       
       }

       public FileInfo TitleToFileName(FileInfo musicFile)
       { 
               FileInfo mFile = null;
               TitleToFileName titleToFileName = new TitleToFileName();
               mFile = titleToFileName.Process(musicFile);
               return mFile;
       }

       public void CreateArtistFolderAndMoveFile(FileInfo musicFile)
       {
           BuildArtistFolder bldArtFolder = new BuildArtistFolder();
           
           bldArtFolder.Process(musicFile);


       }

       public string GetFileNameRemovingFileExtention(string musicFileName)
       {
           //from music file name remove file extention.
           var fileExtentionIndex = musicFileName.IndexOf(GlobalUserConfig.FileExtention);
           if (fileExtentionIndex > 0)
           {
               musicFileName = musicFileName.Substring(0, fileExtentionIndex);
           }

           return musicFileName;
       }

       public TagLib.File RemovePrefixArtistOrBandFromTitle(TagLib.File tagMusicFile)
       {
         
           DistilTitle distilTitle = new DistilTitle();
           TitleExtract titleExtract = new TitleExtract();


           string newTitle = "";
           string artistOrBandName = "";
           Simile.SimileEnums.GroupCategory grpCategory = SimileEnums.GroupCategory.Unknown;
           Simile.DBProcess.MusicBand musicBand = new Simile.DBProcess.MusicBand();
           Simile.DBProcess.MusicArtist musicArtist = new Simile.DBProcess.MusicArtist();

           distilTitle.Process(tagMusicFile, out newTitle, out artistOrBandName, out grpCategory);

           newTitle = titleExtract.Process(newTitle);

           if (!string.IsNullOrEmpty(newTitle))
           {
               tagMusicFile.Tag.Title = newTitle.Trim();
           }

           if (grpCategory == SimileEnums.GroupCategory.Artist)
           {
               tagMusicFile.Tag.AlbumArtists[0] = artistOrBandName.Trim();
               tagMusicFile.Tag.Comment += "Artist=" + artistOrBandName.Trim() + ";" + System.Environment.NewLine ;
               tagMusicFile.Save();

           }
           else if (grpCategory == SimileEnums.GroupCategory.Band)
           {
               tagMusicFile.Tag.Comment += "Band=" + artistOrBandName.Trim() + ";" + System.Environment.NewLine;
               tagMusicFile.Save();
           }

           return tagMusicFile;
       }

       public TagLib.File RemovePrefixArtistOrBand(FileInfo musicFile,TagLib.File tagMusicFile)
       {

           DistilTitle distilTitle = new DistilTitle();
           TitleExtract titleExtract = new TitleExtract();


           string newTitle = "";
           string artistOrBandName = "";
           Simile.SimileEnums.GroupCategory grpCategory = SimileEnums.GroupCategory.Unknown;
           Simile.DBProcess.MusicBand musicBand = new Simile.DBProcess.MusicBand();
           Simile.DBProcess.MusicArtist musicArtist = new Simile.DBProcess.MusicArtist();

           distilTitle.Process(musicFile, out newTitle, out artistOrBandName, out grpCategory);

           newTitle = titleExtract.Process(newTitle);

           tagMusicFile.Tag.Title = newTitle.Trim();

           if (grpCategory == SimileEnums.GroupCategory.Artist)
           {
               tagMusicFile.Tag.AlbumArtists[0] = artistOrBandName.Trim();
               tagMusicFile.Tag.Comment += "Artist=" + artistOrBandName.Trim() + ";" + System.Environment.NewLine;
               tagMusicFile.Save();

           }
           else if (grpCategory == SimileEnums.GroupCategory.Band)
           {
               tagMusicFile.Tag.Comment += "Band=" + artistOrBandName.Trim() + ";" + System.Environment.NewLine;
               tagMusicFile.Save();
           }

           return tagMusicFile;
       }
   }
}
