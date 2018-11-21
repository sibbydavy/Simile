using MusicFactory;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Simile.DBProcess
{
   public class MusicBand
    {
       string[] obsoleteValues = null;


       public MusicBand()
       {
           this.obsoleteValues = new string[2];
          // BuildInValidArtist();
       }

       /// <summary>
       /// Get band from the music tag file.
       /// </summary>
       /// <param name="tagMusicFile"></param>
       /// <returns>Return list of artists</returns>
       public List<string> GetBand(TagLib.File tagMusicFile)
       {
           List<string> artists = new List<string>();

           //Tag has two properties such that 'AlbumArtists' and 'Artists'.
           // Identifies in which tag artists details contains.
           foreach (var val in obsoleteValues)
           {
               if (tagMusicFile.Tag != null && tagMusicFile.Tag.AlbumArtists.Count() > 0)
               {
                   artists = tagMusicFile.Tag.AlbumArtists.Where(r => r.ToUpper() != val).ToList();
               }
               if (tagMusicFile.Tag.Artists != null && tagMusicFile.Tag.Artists.Count() > 0)
               {
                   artists = tagMusicFile.Tag.Artists.Where(r => r.ToUpper() != val).ToList();
               }
           }

           return artists;
       }

       public bool HasBandInDB(string bandName)
       {
           bool yes = false;

           using (var modelContext = new MusicPlayEntities3())
           {
               yes = modelContext.MusicBands.Where(r => r.BandName.ToUpper() == bandName.ToUpper()).Count() > 0;
           }
           return yes;
       }

       //public bool IsValidArtistTag(TagLib.File musicFile)
       //{
       //    //check tag contain band.
       //    bool isValidBandTag = false;
       //    isValidBandTag = (musicFile.Tag. != null && musicFile.Tag.AlbumArtists.Count() > 0);
       //    isValidBandTag = !isValidBandTag ? (musicFile.Tag.Artists != null && musicFile.Tag.Artists.Count() > 0) : false;

       //    return isValidBandTag;
       //}
    
    }
}
