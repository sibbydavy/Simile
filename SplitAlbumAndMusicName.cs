using MusicFactory;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Simile
{
  public  class SplitAlbumAndMusicName
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


                  }

           }

      }

    }
}
