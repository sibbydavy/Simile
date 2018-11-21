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
   public class TruncateSymbolFactory
    {
      // TruncateMaster truncateMaster = new TruncateMaster();
       public void Process()
       {
           List<DirectoryInfo> lstDirectory = new DirectoryProcess().GetDirectories();
           DirectoryProcess dirProcess = new DirectoryProcess();
          
           foreach (DirectoryInfo dir in lstDirectory)
           {
               FileInfo[] flist = dir.GetFiles("*.mp3");
               foreach (FileInfo finfo in flist)
               {
                   try
                   {
                       //TagLib.File tagFile = TagLib.File.Create(finfo.FullName);
                       
                       //check numeric value at begining of filename.
                       String tmp = finfo.Name.Substring(1, 1);

                       String symbolValue = StretchedMethod.GetFirstCharacter("");// truncateMaster.GetSymbolValue(TruncateMaster.Symbols.UnderscoreHyphen);
                       if (String.Equals (tmp, symbolValue))
                       {
                           String newName = "";
                           newName = finfo.Name.Substring(1, finfo.Name.Length-1);
                           System.IO.File.Move(finfo.Name, newName);

                       }
                       else if (String.Equals(tmp, symbolValue))
                       {
                           String newName = "";
                           newName = finfo.Name.Substring(1, finfo.Name.Length-1);
                           System.IO.File.Move(finfo.Name, newName);
                       }

                   }
                   catch (CorruptFileException errmsg)
                   {
                       throw errmsg;
                   }
               }
           
           }
       }

       public string GetNewName(string fileName,Int16 order, System.IO.FileInfo finfo)
       {
           String newName = "";
           String tmp =  StretchedMethod.GetFirstCharacter(fileName);
           
       //   Simile.TruncateMaster.Symbols symbolValue = truncateMaster.GetSymbolValue(order);

       //    newName = finfo.Name.Substring(1, finfo.Name.Length - 1);
       //    if (String.Equals(tmp, symbolValue))
       //    {
            
       //        newName = finfo.Name.Substring(1, finfo.Name.Length - 1);
       //        System.IO.File.Move(finfo.Name, newName);

       //    }
       //    else if (String.Equals(tmp, symbolValue))
       //    {
           
       //        newName = finfo.Name.Substring(1, finfo.Name.Length - 1);
       //        System.IO.File.Move(finfo.Name, newName);
       //    }
           return "";
       }

    }
}
