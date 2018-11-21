using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Simile.Algorithm
{

    public class RemoveSpecialCharacter
    {
        char[] obsoleteValues = new char[3];

        public RemoveSpecialCharacter()
        {
            this.obsoleteValues[0] = '.';
            this.obsoleteValues[1] = '_';
        }

       public string RemoveSpeicalChar(string name)
        {
            int indexOfSpecialChar = -1;
            string newName = "";
            foreach (char val in this.obsoleteValues)
            {
                indexOfSpecialChar = name.IndexOf(val);
                newName =  name.Remove(0, indexOfSpecialChar);
               
            }

            foreach (char val in this.obsoleteValues)
            {
                if (newName.Contains(val))
                {
                    RemoveSpeicalChar(newName);
                    break;
                }
            }
            return newName;
        }

        void GetIndexOfObsoleteValue(string name, out bool hasGotSpecialCharPrefix, out int lengthToRemove)
        {
            int startIndex = 0;
            hasGotSpecialCharPrefix = false;
            lengthToRemove = 0;

            foreach (char val in this.obsoleteValues)
            {
                name = name.Trim();
                startIndex = name.IndexOf(val);
                if (startIndex == -1) continue;
                var tmp = name.Substring(0, startIndex - 1);
                var intValue = 0;
                Int32.TryParse(tmp, out intValue);
                if (intValue > 0)
                {
                    hasGotSpecialCharPrefix = true;
                    lengthToRemove = startIndex;
                    break;
                }
            }

        }
    }
}
