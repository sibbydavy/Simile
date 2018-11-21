using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Simile
{
   public static class StretchedMethod
    {
       public static String GetFirstCharacter(String value)
       {
           String result = "";
           if (!String.IsNullOrEmpty(value) && value.Length >= 2 )
           {
               result = value.Substring(1, value.Length - 1);
           }
           return result;
       }
    }
}
