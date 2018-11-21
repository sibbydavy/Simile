using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Simile.Algorithm
{
    public class FileNameToTitle
    {

        string[] obsoleteValues = new string[1];

        public FileNameToTitle()
        {
            this.obsoleteValues[0] = "Abs olute Dance-";
        }

        public void Process(string fileName)
        {
            //remove unwanted string. ex: if file name is like 'Absolute Dance - Diana King',
            // then make it as 'Diana King'.
            string tmpName = "";
            foreach (string val in this.obsoleteValues)
            {
                var index = fileName.IndexOf(val);
                if (index > 0)
                {
                    tmpName = fileName.Substring(index);
                }
            }


        }
    }
}
