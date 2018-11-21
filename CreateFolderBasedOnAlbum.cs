using MusicFactory;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Simile
{
   public class CreateFolderBasedOnAlbum
    {
       public void process()
       {
           List<DirectoryInfo> lstDirectory = new DirectoryProcess().GetDirectories();
           DirectoryProcess dirProcess = new DirectoryProcess();
           foreach (DirectoryInfo dir in lstDirectory)
           {
               FileInfo[] flist = dir.GetFiles(GlobalUserConfig.SearchPattern); // "*.MP3");
               bool isValid = false;
               string albumName = "";
               foreach (FileInfo finfo in flist)
               {
                   isValid = false;
                   albumName = "";
                   try
                   {
                        TagLib.File tagFile = TagLib.File.Create(finfo.FullName);
                     
                        if (tagFile.Tag != null)
                        {
                            isValid = IsValidAlbumName(tagFile);
                            albumName = tagFile.Tag.Album.Trim();
                            if (!IsFolderExist(albumName))
                            {
                                System.IO.Directory.CreateDirectory(@"E:\Songs\Managed Music Folder\" + albumName);
                            }

                        }
                   }
                   catch (Exception errmsg)
                   {
                       throw errmsg;
                   }
               }

           }
       
       }

       bool IsValidAlbumName(TagLib.File tagFile)
       {
           bool isValid = false;

           isValid = tagFile.Tag != null;
           isValid = isValid ? !string.IsNullOrEmpty(tagFile.Tag.Album) : false;

             

           return isValid;
       
       }

       bool IsFolderExist(string albumName)
       {
           bool yes = true;

           yes = Directory.Exists(@"E:\Songs\Managed Music Folder\" + albumName);

           return yes;
       }

    }
}
