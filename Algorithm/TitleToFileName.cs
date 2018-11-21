using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Simile.Algorithm
{
   public class TitleToFileName
    {
        string[] obsoleteValues = new string[1];

        public TitleToFileName()
        {
            this.obsoleteValues[0] = "Track";
        }

        public System.IO.FileInfo Process(System.IO.FileInfo musicFile)
        {
            System.IO.FileInfo newMusicFile = null;
           var tagMusicFile = TagLib.File.Create(musicFile.FullName);
           var musicFileName = musicFile.Name.Trim();
           var title = !string.IsNullOrEmpty(tagMusicFile.Tag.Title) ?  tagMusicFile.Tag.Title.Trim() : string.Empty;
           var tmpFolderForRename =  MusicFactory.GlobalUserConfig.TempFolder;

           if (IsInvalidFileName(musicFileName) && IsValidTitle(title))
           {               
               var newFileName = tmpFolderForRename + title + MusicFactory.GlobalUserConfig.FileExtention;
               System.IO.File.Copy(musicFile.FullName, newFileName);
               newMusicFile = new MusicFactory.DirectoryProcess().GetFile( title + MusicFactory.GlobalUserConfig.FileExtention);
           }
           else
           {
               newMusicFile = musicFile;
           }


           return newMusicFile;
        }

        bool IsInvalidFileName(string fileName)
        {
            bool isInvalid = false;
            
            foreach (var val in this.obsoleteValues)
            {
                if (fileName.Contains(val))
                {
                    isInvalid = true;
                    break;
                }
            }

            return isInvalid;
        
        }

        bool IsValidTitle(string titleName)
        {
            bool isValid = true;

            isValid = !String.IsNullOrEmpty(titleName);

            if (isValid)
            {
                foreach (var val in this.obsoleteValues)
                {
                    if (titleName.Contains(val))
                    {
                        isValid = false;
                        break;
                    }
                }
            }

            return isValid;

        }
    }
}
