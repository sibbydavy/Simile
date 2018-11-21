using MusicFactory;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TagLib;

namespace Simile
{
    public class TagNameForFileName
    {
        public void Process()
       {
           List<DirectoryInfo> lstDirectory = new DirectoryProcess().GetDirectories();
           DirectoryProcess dirProcess = new DirectoryProcess();          
           foreach (DirectoryInfo dir in lstDirectory)
           {
               FileInfo[] flist = dir.GetFiles(GlobalUserConfig.SearchPattern); // "*.MP3");

               foreach (FileInfo finfo in flist)
               {
                   try
                   {
                       TagLib.File tagFile = TagLib.File.Create(finfo.FullName);                       
                       if(tagFile.Tag != null &&  !string.IsNullOrEmpty(tagFile.Tag.Title) &&
                           !string.IsNullOrEmpty(tagFile.Tag.Title.Trim()) && !tagFile.Tag.Title.Equals(finfo.Name) )
                       {
                           bool bOk = !tagFile.Tag.Title.ToUpper().Contains("TRACK");// ? true : false;//.Contains("track"));
                          
                           bOk = bOk == true && !tagFile.Tag.Title.ToUpper().Contains("AUDIO TRACK");//  ? true : false;//.Contains("track"));

                           if (bOk)
                           {                                                          
                               String newName = "";  string album = "";  string[] artist;
                            album = tagFile.Tag.Album;  artist = tagFile.Tag.AlbumArtists.ToArray();  newName = tagFile.Tag.Title.Trim();
                               string[] contriArtist;    contriArtist = tagFile.Tag.Artists.ToArray();
                               try //AudioTrack 15
                               {
                                   //if (IsFileNameAlbumInAlbum(finfo) == true)
                                   //{
                                   //    System.IO.File.Move(@"E:\Songs\Sibby\New order - The best of\" + finfo.Name, @"E:\Songs\Sibby\New order - The best of\" + newName + ".mp3");
                                   //}
                                   //else 
                                   {
                                       newName = TitleToFileNameRemovingAlbumName(tagFile).ToString();
                                       if (!string.IsNullOrEmpty(newName))
                                       {
                                           System.IO.File.Move(@"E:\Songs\1Refactored Songs\SIBBY\80s Compilation CDs\80s Compilation CD 1\New folder\" + finfo.Name,
                                               @"E:\Songs\1Refactored Songs\SIBBY\80s Compilation CDs\80s Compilation CD 1\New folder\" + newName + ".mp3");

                                           //tagFile.Tag.Title = newName;
                                           //tagFile.Save();
                                          

                                       }
                                   }
                               }
                               catch (Exception errmsg)
                               {
                                   if(errmsg.Message.Trim() == "Cannot create a file when that file already exists.")
                                   {
                                       //  System.IO.File.Move(@"E:\Songs\Sibby\The_Righteous_Brothers-Unchained_Melody-(Retail)-1990-HHI\" + finfo.Name, @"E:\Songs\ProcessDelete\" + newName + ".mp3");//"ProcessDelete","");
                                   }
                                  string errMesg = errmsg.Message;
                               }
                           }
                       }
                    
                   }
                   catch (CorruptFileException errmsg)
                   {
                       throw errmsg;
                   }
               }
           
           }

       }

        bool IsFileNameAlbumInAlbum(FileInfo fileName)
        {
            bool yes = false;
            string result = "";
            Char[] splitValue = new Char[5];
            splitValue[0] = Convert.ToChar(@"-");
            TagLib.File tagFile = TagLib.File.Create(fileName.FullName);

            var val = fileName.Name.Split(splitValue).Select(r => r.Trim().ToUpper());
            if (val.Count() > 1)
            {
                if (tagFile.Tag.AlbumArtists.Count() > 0)
                {
                    result = val.Where(r => r.Contains(tagFile.Tag.AlbumArtists.Take(tagFile.Tag.AlbumArtists.Count()).First().Trim().ToUpper())).SingleOrDefault();
                }
                else if (result == null || result.Count() < 1)
                {
                    result = val.Where(r => r.Contains(tagFile.Tag.Artists.Take(tagFile.Tag.Artists.Count()).FirstOrDefault().Trim().ToUpper())).SingleOrDefault();

                }

                //use dot has split for artist.
                splitValue[0] = Convert.ToChar(@".");
                var splitedArtist = fileName.Name.Split(splitValue).Select(r => r.Trim().ToUpper()).Where(sv => sv.ToUpper().Equals("MP3") == false);
                if(splitedArtist.Count() > 1)
                {
                    if (tagFile.Tag.AlbumArtists.Count() > 0)
                    {
                        result = splitedArtist.Where(r => r.Contains(tagFile.Tag.AlbumArtists.Take(tagFile.Tag.AlbumArtists.Count()).First().Trim().ToUpper())).SingleOrDefault();
                    }
                    if (result == null || result.Count() < 1)
                    {
                        result = splitedArtist.Where(r => r.Contains(tagFile.Tag.Artists.Take(tagFile.Tag.Artists.Count()).FirstOrDefault().Trim().ToUpper())).SingleOrDefault();

                    }
                 }
                     //--------------------




                yes = result != null;
                yes &= yes == true && result.Count() > 0;
            }
            else
            {
                yes = true;
            }

            return yes;

        }

        string TitleToFileNameRemovingNumericValue(TagLib.File tagFile)
        {
            string title = "";

            bool yes = false;
            string result = "";
            Char[] splitValue = new Char[2];
            splitValue[0] = Convert.ToChar(@"-");
            splitValue[0] = Convert.ToChar(@" ");
            var val = tagFile.Tag.Title.Split(splitValue).Select(r => r.Trim());

            if (val.Count() > 1)
            {
                int intValue = splitValue[0];

                if (intValue > 0)
                {
                    if (!tagFile.Tag.Title.ToUpper().Contains("TRACK"))
                    {
                        int n;
                        var newval = from t in val
                                     where !String.IsNullOrEmpty(t) && int.TryParse(t, out n) == false
                                     select t;


                        title = newval.SingleOrDefault().ToString().Trim();

                    }
                }

            }
            else
            {
                title = tagFile.Tag.Title.Trim();
            }

          
            return title;
        }

        string TitleToFileNameRemovingAlbumName(TagLib.File tagFile)
        {
            string title = "";

            if (!tagFile.Tag.Title.ToUpper().Contains("TRACK"))
            {
                title = tagFile.Tag.Title.Trim();
            }
           

            return title;
        }

    }
}
