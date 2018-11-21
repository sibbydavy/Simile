using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Simile.Factory
{
   public  class TemplateMusicFileTag
    {
       public string ArtistTagVale
       {
           get;
           set;
       }

       public bool IsArtist
       {
           get;
           set;
       }
       public bool IsAlbum
       {
           get;
           set;
       }
       public bool IsBand
       {
           get;
           set;
       }

       public string ErrorMessage
       {
           get;
           set;
       }

       public string FileName
       {
           get;
           set;
       }
    }
}
