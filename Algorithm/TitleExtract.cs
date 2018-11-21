using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Simile.Algorithm
{
   public class TitleExtract
    {
       char[] obsoleteValues = new char[3];

       public TitleExtract()
       {
          this.obsoleteValues[0] = '-';
          this.obsoleteValues[1] = '_';
          this.obsoleteValues[2] = ' ';
       }

       public string  Process(string title)
       { 
         //remove unwatned special character from title.
           List<string> splitedTitle = new List<string>();
           string newTitle = "";
           foreach (char obsVal in this.obsoleteValues)
           {
               string[] tmp = title.Split(obsVal);

               if (tmp.Count() > 1)
               {
                   splitedTitle.AddRange(tmp.Where(r => !string.IsNullOrEmpty(r)));
               }
           }

           foreach (var val in splitedTitle)
           {
               var  tmp = val.ToLower().Trim().ToArray();
               var tmp1 = "";
               if (tmp.Length > 0)
               {
                   newTitle += !string.IsNullOrEmpty(newTitle) ? " " : "";
                   foreach (var t in tmp)
                   {
                       tmp1 += string.IsNullOrEmpty(tmp1) ? Convert.ToString(t).ToUpper() : Convert.ToString(t);
                   }
                   newTitle += tmp1;
               }
              
           }

           return newTitle;
       }
    }
}
