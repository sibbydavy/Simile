using Simile.DBProcess;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Simile.Algorithm
{
   public  class DistilTitle
    {
       char[] obsoleteValues = new char[3];

       public DistilTitle()
       {
          this.obsoleteValues[0] = '-';
          this.obsoleteValues[1] = '_';
          this.obsoleteValues[2] = ' ';
       }

       public void Process(TagLib.File musicFile,out string newTitle, out string artistOrBandName,out Simile.SimileEnums.GroupCategory grpCategory)
       {
           bool isValidTag = false;
           newTitle = "";
           artistOrBandName = "";
           grpCategory = SimileEnums.GroupCategory.Unknown;

           Simile.DBProcess.MusicArtist musicArtist = new Simile.DBProcess.MusicArtist();

           //check tag contain artist.
           isValidTag = musicArtist.IsValidArtistTag(musicFile);


           List<string> splitedTitle = new List<string>();

           //If there is no valid artist tag in music file. Then check title is contain artist.
           if (!isValidTag)
           {
               foreach (char obsVal in this.obsoleteValues)
               {
                   string[] tmp = musicFile.Tag.Title.Split(obsVal);
                   if (tmp.Count() > 1)
                   {
                         splitedTitle.AddRange(tmp.Where(r => !string.IsNullOrEmpty(r)));
                   }
               }
           }

          /*Most pbly artist/band will included, before or after the title. so to speed up the process
            first get the word from start index of an array. if its not identified as artist/band in db, then took last index
           * from an array and check with db.
           * TODO:- at present i am assuming that artist/band will be first or last in array. If not find values in db then
           * i assume title contain no artist/band.
            */
           string assumedValue = "";
           bool isAssumedValueOk = false;

           isAssumedValueOk = IsAssumedValueAnArtist(splitedTitle,out assumedValue);

           if (isAssumedValueOk)
           {
               grpCategory = SimileEnums.GroupCategory.Artist;
               string temp = "";
               int index = musicFile.Tag.Title.IndexOf(assumedValue);
               if (index != -1)
               {
                   temp = musicFile.Tag.Title.Remove(0, assumedValue.Length);
                   artistOrBandName = assumedValue.Trim();
               }
           }

           isAssumedValueOk = (!isAssumedValueOk) ? IsAssumedValueBand(splitedTitle, out assumedValue) : false;

           if (isAssumedValueOk)
           {
               grpCategory = SimileEnums.GroupCategory.Band;
               int index = musicFile.Tag.Title.IndexOf(assumedValue);
               if (index != -1)
               {
                   newTitle = musicFile.Tag.Title.Remove(0, assumedValue.Length);
                   artistOrBandName = assumedValue.Trim();
               }
           }
       }

       public void Process(FileInfo musicFile, out string newTitle, out string artistOrBandName, out Simile.SimileEnums.GroupCategory grpCategory)
       {
          
           newTitle = "";
           artistOrBandName = "";
           grpCategory = SimileEnums.GroupCategory.Unknown;

           Simile.DBProcess.MusicArtist musicArtist = new Simile.DBProcess.MusicArtist();

          
           List<string> splitedTitle = new List<string>();

               foreach (char obsVal in this.obsoleteValues)
               {
                   string[] tmp = musicFile.Name.Split(obsVal);
                   if (tmp.Count() > 1)
                   {
                       splitedTitle.AddRange(tmp.Where(r => !string.IsNullOrEmpty(r)));
                   }
               }

           /*Most pbly artist/band will included, before or after the title. so to speed up the process
             first get the word from start index of an array. if its not identified as artist/band in db, then took last index
            * from an array and check with db.
            * TODO:- at present i am assuming that artist/band will be first or last in array. If not find values in db then
            * i assume title contain no artist/band.
             */
           string assumedValue = "";
           bool isAssumedValueOk = false;

           isAssumedValueOk = IsAssumedValueAnArtist(splitedTitle, out assumedValue);

           if (isAssumedValueOk)
           {
               grpCategory = SimileEnums.GroupCategory.Artist;
               string temp = "";
               int index = musicFile.Name.IndexOf(assumedValue);
               if (index != -1)
               {
                   temp = musicFile.Name.Remove(0, assumedValue.Length);
                   artistOrBandName = assumedValue.Trim();
               }
           }

           isAssumedValueOk = (!isAssumedValueOk) ? IsAssumedValueBand(splitedTitle, out assumedValue) : false;

           if (isAssumedValueOk)
           {
               grpCategory = SimileEnums.GroupCategory.Band;
               int index = musicFile.Name.IndexOf(assumedValue);
               if (index != -1)
               {
                   newTitle = musicFile.Name.Remove(0, assumedValue.Length);
                   artistOrBandName = assumedValue.Trim();
               }
           }
       }

       bool IsAssumedValueAnArtist(List<string> splitedTitle, out string assumedValue)
       {
           //bool yes = false;
           assumedValue = "";
           bool isAssumedValueAnArtist = false;
           Simile.DBProcess.MusicArtist musicArtist = new Simile.DBProcess.MusicArtist();

           if (splitedTitle.Count > 0)
           {
               assumedValue = splitedTitle[0];
               isAssumedValueAnArtist = musicArtist.HasArtistInExistsInDB(assumedValue);
           }

           if (!isAssumedValueAnArtist)
           {
               if (splitedTitle.Count > 0)
               {
                   assumedValue = splitedTitle[splitedTitle.Count() - 1];
                   isAssumedValueAnArtist = musicArtist.HasArtistInExistsInDB(assumedValue);
               }
           }

           return isAssumedValueAnArtist;
       }

       bool IsAssumedValueBand(List<string> splitedTitle, out string assumedValue)
       {
           //bool yes = false;
           assumedValue = "";
           bool isAssumedValueBand = false;
           Simile.DBProcess.MusicBand musicBand = new Simile.DBProcess.MusicBand();

           if (splitedTitle.Count > 0)
           {
               assumedValue = splitedTitle[0];
               isAssumedValueBand = musicBand.HasBandInDB(assumedValue);
           }

           if (!isAssumedValueBand)
           {
               if (splitedTitle.Count > 0)
               {
                   assumedValue = splitedTitle[splitedTitle.Count() - 1];
                   isAssumedValueBand = musicBand.HasBandInDB(assumedValue);
               }
           }

           return isAssumedValueBand;
       }
   
    }
}
