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
    /// <summary>
    /// Truncate number from file name;
    /// ex: if file name is "04__TAKE_ME_AWAY__INTO_THE_" after truncate the name becomes "TAKE_ME_AWAY__INTO_THE_"
    /// </summary>
   public class TruncasteNumber
    {
        public void Process()
        {
            List<DirectoryInfo> lstDirectory = new DirectoryProcess().GetDirectories();
            DirectoryProcess dirProcess = new DirectoryProcess();
            foreach (DirectoryInfo dir in lstDirectory)
            {
                FileInfo[] flist = dir.GetFiles("*.MP3");

                foreach (FileInfo finfo in flist)
                {
                    try
                    {
                        TagLib.File tagFile = TagLib.File.Create(finfo.FullName);

                        int firstIndex = finfo.Name.IndexOf('_');
                        string result = finfo.Name.Substring(0, firstIndex);
                         int value = 0;
                         Int32.TryParse(result, out value);

                         string newName = "";
                        //if value is > 0 then, first letter in file name has integer value. so remove it.
                         if (value > 0)
                         {
                             try 
                             {
                                 newName = RemoveDownHyphenFromBeginingOfName(-1, finfo.Name);
                                
                               //  newName = finfo.Name.Remove(0, firstIndex + 1);

                                 if (!string.IsNullOrEmpty(newName))
                                 {
                                     System.IO.File.Move(@"E:\Songs\ToProcess\" + finfo.Name, @"E:\Songs\Processed\" + newName + ".mp3");
                                 }
                             }
                             catch (Exception errmsg)
                             {
                                 if (errmsg.Message.Trim() == "Cannot create a file when that file already exists.")
                                 {
                                     System.IO.File.Move(@"E:\Songs\ToProcess\" + finfo.Name, @"E:\Songs\ProcessDelete\" + newName + ".mp3");//"ProcessDelete","");
                                 }
                                 string errMesg = errmsg.Message;
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



        string  RemoveDownHyphenFromBeginingOfName(int previousIndex, string fileName)
        {
            string newName = "";

            int value = 0;
            value = fileName.IndexOf('_');

           

            //value should be next number from previous. This checking prevent removing all hyphen.
            //ex: 01__ABC_hi. in this case this function only remove hyphen till ABC.
            if (value >= 0)
            {
                if ((value == 0) || (previousIndex < 0) || (previousIndex > 0 && value == previousIndex + 1))
                {
                    newName = fileName.Substring(value + 1);

                    int nextIndex = newName.IndexOf('_');
                    if ( (nextIndex == 0) || (nextIndex >= 0 && nextIndex == value + 1))
                    {
                        RemoveDownHyphenFromBeginingOfName(value + 1, newName);
                    }
                }
                else
                {

                    return newName;
                }

            }
            else
            {
                return newName;
            }

            return newName;
        }
       
    }
}
