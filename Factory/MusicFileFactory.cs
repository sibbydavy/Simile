using MusicFactory;
using Simile.DBProcess;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Simile.Factory
{
   public class MusicFileFactory
    {
       List<string> artistsTagValue = new List<string>();
       List<string> album = new List<string>();
       List<string> band = new List<string>();


       List<string> corruptedFiles = new List<string>();

       List<TemplateMusicFileTag> listOfMusicFileTagTempData = null;
       public List<TemplateMusicFileTag> ListOfMusicFileTagTempData
       {
           get {
               return this.listOfMusicFileTagTempData;
           }
       }

       public List<string> ArtistsTagValue
       {
           get
           {
               return this.artistsTagValue;
           }
       }

       public MusicFileFactory()
       {
           corruptedFiles.Add(@"E:\Songs\Managed Music Folder\Greatest Synthesizer Hits\Blue Monday.mp3");
           corruptedFiles.Add(@"E:\Songs\Managed Music Folder\Greatest Synthesizer Hits\Dance me to the End of Love.mp3");
           corruptedFiles.Add(@"E:\Songs\Managed Music Folder\Greatest Synthesizer Hits\Funkytown.mp3");
           corruptedFiles.Add(@"E:\Songs\Managed Music Folder\Greatest Synthesizer Hits\Maid of Orleans (Joan of Arc).mp3");
           corruptedFiles.Add(@"E:\Songs\Managed Music Folder\Greatest Synthesizer Hits\Only You.mp3");
           corruptedFiles.Add(@"E:\Songs\Managed Music Folder\Greatest Synthesizer Hits\Peter Gunn.mp3");
           corruptedFiles.Add(@"E:\Songs\Managed Music Folder\Greatest Synthesizer Hits\Theme from midnight express.mp3");
           corruptedFiles.Add(@"E:\Songs\Managed Music Folder\Sting\Sting - Englishman In New York.mp3");
       }

       /// <summary>
       /// From music file extract album,artist, band.
       /// </summary>
       public void Process()
       {

           FileInfo[] musicFiles = null;
           TagLib.File tagMusicFile = null;
           //string album = "";
           List<string> artists = new List<string>();

           //  DirectoryProcess dProcess = new DirectoryProcess();
           List<DirectoryInfo> dirList = new DirectoryProcess().GetDirectories();

           MusicArtist artist = new MusicArtist();
           this.listOfMusicFileTagTempData = new List<TemplateMusicFileTag>();

           IterateDirectory(dirList);

           ////Iterate parent directory and all its sub directories.
           //foreach (DirectoryInfo dir in dirList)
           //{
           //    //Get music files from the this directory.
           //    musicFiles = dir.GetFiles(GlobalUserConfig.FileType);

           //    //Iterate music files.
           //    foreach (FileInfo musicFile in musicFiles)
           //    {
           //        try
           //        {
           //            //Create tag music file from music file.
           //            tagMusicFile = TagLib.File.Create(musicFile.FullName);

           //            artists = artist.GetArtist(tagMusicFile);

      
           //            foreach (string val in artists)
           //            {
           //                if (!this.artistsTagValue.Contains(val))
           //                {
           //                    if(!IsAlreadyExistsInDB(val.Trim()))
           //                    {
           //                    this.artistsTagValue.Add(val.Trim());
                               
           //                    this.listOfMusicFileTagTempData.Add(
           //                        new TemplateMusicFileTag() { ArtistTagVale = val.Trim(), IsAlbum = false, IsArtist = false, IsBand = false });
           //                    }
           //                }
           //            }

           //        }
           //        catch (Exception errMsg)
           //        {
           //            throw errMsg;
           //        }
           //    }

           //}
       }

       void IterateDirectory(List<DirectoryInfo> dirList)
       {
           List<string> fnames = new List<string>();

           foreach (var dir in dirList)
           {
               FileInfo[] musicFiles = null;

               musicFiles = dir.GetFiles(GlobalUserConfig.FileType);

               IterateMusicFiles(musicFiles);
           }

           //try
           //{
           //    Parallel.ForEach(dirList, dir =>
           //    {
           //        FileInfo[] musicFiles = null;

           //        musicFiles = dir.GetFiles(GlobalUserConfig.FileType);

           //        IterateMusicFiles(musicFiles);


           //    });
           //}
           //catch (Exception errMsg)
           //{
           //    throw errMsg;
           //}
       }

       void IterateMusicFiles(FileInfo[] musicFiles)
       {          
           TagLib.File tagMusicFile = null;
           List<string> artists = new List<string>();

           Simile.DBProcess.MusicArtist artist = new Simile.DBProcess.MusicArtist();

           bool isValueExistsInArtistTagValueArray = false;

           //Iterate music files.
           foreach (FileInfo musicFile in musicFiles)
           {
               if (corruptedFiles.Where(r => r == musicFile.FullName).Count() > 0) continue; 

               try
               {
                   //Create tag music file from music file.
                   tagMusicFile = TagLib.File.Create(musicFile.FullName);
            

                   artists = artist.GetArtist(tagMusicFile);

                   foreach (string val in artists)
                   {

                       if (this.artistsTagValue.Count() > 0)
                       {
                           isValueExistsInArtistTagValueArray = this.artistsTagValue.ConvertAll(r => r.Trim().ToUpper()).Contains(val.Trim().ToUpper());

                         //  IsFun(val);
                           //try
                           //{
                           //    lock (this)
                           //    {
                           //        isValueExistsInArtistTagValueArray = this.artistsTagValue.ConvertAll(r => r.Trim().ToUpper()).Contains(val.Trim().ToUpper());
                           //    }
                           //}
                           //catch (Exception errmsg)
                           //{
                           //    throw errmsg;
                           //}
                       }

                       if (!isValueExistsInArtistTagValueArray && !IsFun(val.Trim())) // !this.artistsTagValue.ConvertAll(r => r.Trim().ToUpper()).Contains(val.Trim().ToUpper()))
                       {
                           if (!IsAlreadyExistsInDB(val.Trim()))
                           {
                               this.artistsTagValue.Add(val.Trim());

                               lock (this)
                               {
                                   this.listOfMusicFileTagTempData.Add(
                                       new TemplateMusicFileTag()
                                       {
                                           ArtistTagVale = val.Trim(),
                                           IsAlbum = false,
                                           IsArtist = false,
                                           IsBand = false,
                                           ErrorMessage = "",
                                           FileName = musicFile.FullName
                                       });
                               }
                           }
                       }
                   }

               }
               catch (TagLib.CorruptFileException errMsg)
               {
                   //if (errMsg.Message == "MPEG audio header not found.")
                   //{
                   //    this.listOfMusicFileTagTempData.Add(
                   //                new TemplateMusicFileTag() { ArtistTagVale = musicFile.FullName.Trim(), IsAlbum = false, IsArtist = false, IsBand = false,
                   //                ErrorMessage = errMsg.Message });
                   //}
               }
               catch (Exception errMsg)
               { 
                   throw errMsg;
               }
           }

       }
       
       bool IsAlreadyExistsInDB(string value)
       {
           bool yes = false;
           try
           {
               using (var modelContext = new MusicPlayEntities3())
               {
                   yes = modelContext.MusicAlbums.Where(r => r.AlbumName.ToUpper() == value.ToUpper()).Count() > 0;
               }

               if (!yes)
               {
                   using (var modelContext = new MusicPlayEntities3())
                   {
                       yes = modelContext.MusicArtists.Where(r => r.ArtistName.ToUpper() == value.ToUpper()).Count() > 0;
                   }
               }
               if (!yes)
               {
                   using (var modelContext = new MusicPlayEntities3())
                   {
                       yes = modelContext.MusicBands.Where(r => r.BandName.ToUpper() == value.ToUpper()).Count() > 0;
                   }
               }
           }
           catch (Exception errMsg)
           {
               throw errMsg;
           }

           return yes;
       }

       bool IsFun(string value)
       {
           char[] splitIncomingValue = new char[value.Length];
           string[] strSplitValue = new string[value.Length];

           //Convert incoming values into character array.
           splitIncomingValue = value.ToUpper().ToCharArray();

           char[] obsoleteCharacter = new char[6];
           obsoleteCharacter[0] = '(';
           obsoleteCharacter[1] = ')';
           obsoleteCharacter[2] = '*';
           obsoleteCharacter[3] = ';';
           obsoleteCharacter[4] = '.';
           obsoleteCharacter[5] = ' ';

           //strSplitValue[0] = "New Artist";

          // ArrayList valueToCompare = new ArrayList();
           List<string> valueToCompare = new List<string>();
           foreach (char charValue in splitIncomingValue)
           {
               if (obsoleteCharacter.Where( r => r == charValue).Count() == 0) //value.Contains(charValue))
               {
                   valueToCompare.Add(charValue.ToString().ToUpper()); // = charValue.ToString();
               }
           }


           ////
          // ArrayList dbvalueToCompare = new ArrayList();
           List<string> dbvalueToCompare = new List<string>();
               var albums = this.artistsTagValue.Select(r => r.ToUpper());
               foreach (var albumName in albums)
               {
                   char[] splitAlbumNames = new char[albumName.Length];
                   splitAlbumNames = albumName.ToUpper().ToCharArray();

                   foreach (char charValue in splitAlbumNames)
                   {
                       if (obsoleteCharacter.Where(r => r == charValue).Count() == 0)
                       {
                           dbvalueToCompare.Add(charValue.ToString().ToUpper()); // = charValue.ToString();
                       }
                   }
              
           }

               bool isLocalSame = valueToCompare.Except(dbvalueToCompare).ToList().Count() == 0;

               List<string> dbvalueToCompare1 = new List<string>();
               using (var modelContext = new MusicPlayEntities3())
               {
                   var albums1 = modelContext.MusicAlbums.Select(r => r.AlbumName.ToUpper());
                   foreach (var albumName in albums1)
                   {
                       char[] splitAlbumNames = new char[albumName.Length];

                       foreach (char charValue in splitAlbumNames)
                       {
                           if (obsoleteCharacter.Where(r => r == charValue).Count() == 0)
                           {
                               dbvalueToCompare1.Add(charValue.ToString().ToUpper()); // = charValue.ToString();
                           }
                       }
                   }
               }

               bool isDbSame = valueToCompare.Except(dbvalueToCompare1).ToList().Count() == 0;
               //valueToCompare.ToArray().Where( r => r.ToString() == dbvalueToCompare.ToArray()) //.Contains(dbvalueToCompare);

           bool isSame = false;

           isSame = (isLocalSame || isDbSame);


           return isSame;
       }
    }
}
