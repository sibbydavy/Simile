using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MusicFactory
{
   public class DirectoryProcess
    {
        /// <summary>
        /// Hold parent directory and its child directories. Key is '_directories' datatype that hold the direcotry information.
        /// Value is 'Boolean' datatype. It will be false at initial time when directory is added. After getting all its children 
        /// directory its value update as true. 
        /// </summary>
        Dictionary<DirectoryInfo, bool> _directories = new Dictionary<DirectoryInfo, Boolean>();

        public List<DirectoryInfo> GetDirectories()
        {
            //Get parent directory.
            DirectoryInfo dirInfo = new DirectoryInfo(GlobalUserConfig.ParentFolderPath);

            //Add parent.
            _directories.Add(dirInfo, false);

            //Get all its child directories.
            GetChildDirectories(dirInfo);

            return this._directories.Keys.ToList();
        }

        /// <summary>
        /// Get all child direcotries. This is a recursive function. Its get called itsself to get all child directory.
        /// This method called if _directories variable has 'false' value ('false' means searching for child directory is not done for that folder). 
        /// If there is no value as 'false' in _directories variable(its mean its checked for child folder for all its parent folder) then
        /// this method go exit.
        /// </summary>
        /// <param name="dirinfo"></param>       
        void GetChildDirectories(DirectoryInfo dirinfo)
        {
            //Get child directories.            
            DirectoryInfo[] directories = dirinfo.GetDirectories();

            //Get a direcotry from the collection.
            KeyValuePair<DirectoryInfo, bool> currentRecord = this._directories.Where(r => r.Key == dirinfo).SingleOrDefault();
            //remove this directory from the collection.
            this._directories.Remove(currentRecord.Key);
            //Again add it denoting its checked by using 'true' value.
            this._directories.Add(currentRecord.Key, true);

            //Iterate thru directories. And add to directory dictionary.
            DirectoryInfo newDir = null;
            foreach (DirectoryInfo dinfo in directories)
            {
                newDir = new DirectoryInfo(dinfo.FullName);
                this._directories.Add(newDir, false);
            }

            //From the _directories list get a direcotry.
            var tmpDirectory = this._directories.Where(r => r.Value == false).FirstOrDefault();

            //If tmpDirectory has no value then, its time to exist from the  recursive function. Else
            //call this method recursively.
            if (tmpDirectory.Key == null)
                return;
            else
                GetChildDirectories(tmpDirectory.Key);
        }

       
        private string GetFileName(string fullName)
        {
            string result = "";
            Char[] splitValue = new Char[50];
            splitValue[0] = Convert.ToChar(@"\");
            var val = fullName.Split(splitValue);

            result = val.Where(r => r.Contains(".mp3")).SingleOrDefault();
            result = (result == null) ? val.Where(r => r.Contains(".MP3")).SingleOrDefault() : result;

            return result;
        }

        public bool HasFolderExists(string folderName)
        {
            bool yes = true;

            yes = Directory.Exists(folderName);

            return yes;
        }

        public FileInfo GetFile(string fileName)
        {
            FileInfo fInfo = null;
            try
            {
                fInfo = new DirectoryInfo(GlobalUserConfig.TempFolder).GetFiles(fileName).SingleOrDefault(); 
            }
            catch (Exception errMsg)
            {
                throw errMsg;
            }
            return fInfo;
        }

  
    }
}
