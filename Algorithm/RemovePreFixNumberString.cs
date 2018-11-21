using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Simile.Algorithm
{
    public class RemovePreFixNumberString
    {
        char[] obsoleteValues = new char[3];

        public RemovePreFixNumberString()
        {
            this.obsoleteValues[0] = ' ';
            this.obsoleteValues[1] = '.';
            this.obsoleteValues[1] = '_';
        }

        public string GetMusicName(string musicName)
        {
           string newMusicName = "";

           musicName = musicName.Trim();

            bool hasGotIntegerPrefix = false;
            int lengthToRemove = 0;
            GetIndexOfObsoleteValue(musicName, out hasGotIntegerPrefix,out lengthToRemove);

            if (hasGotIntegerPrefix && lengthToRemove > 0)
            {
                newMusicName = RemoveObsoleteValue(musicName, lengthToRemove);
            }
           
            return newMusicName;
        }


        string RemoveObsoleteValue(string musicName, int lengthToRemove)
        {
            string newName = "";

            newName = musicName.Substring(lengthToRemove).Trim();

            return newName;

        }

        public bool HasNameContainPrefixIntegerValue(string name)
        {          
           bool hasGotIntegerPrefix = false;
           int lenthToRemove = 0;

           GetIndexOfObsoleteValue(name,out hasGotIntegerPrefix,out lenthToRemove);

           hasGotIntegerPrefix = hasGotIntegerPrefix && lenthToRemove > 0;

           return hasGotIntegerPrefix;

        }

        void GetIndexOfObsoleteValue(string name, out bool hasGotIntegerPrefix, out int lengthToRemove)
        {
            int startIndex = 0;
            hasGotIntegerPrefix = false;
            lengthToRemove = 0;

            foreach (char val in this.obsoleteValues)
            {
                name = name.Trim();
                startIndex = name.IndexOf(val);
                if (startIndex == -1) continue;
                var tmp = name.Substring(0, startIndex-1);
                var intValue = 0;
                Int32.TryParse(tmp, out intValue);
                if (intValue > 0)
                {
                    hasGotIntegerPrefix = true;
                    lengthToRemove = startIndex;
                    break;
                }
            }

        }

    }
}
