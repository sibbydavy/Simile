using MusicFactory;
using Simile.Factory;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Simile
{
    public partial class frmExtractMusicFileInfo : Form
    {
        public frmExtractMusicFileInfo()
        {
            InitializeComponent();
        }

        private void btnProcess_Click(object sender, EventArgs e)
        {
            GlobalUserConfig config = new GlobalUserConfig();
            GlobalUserConfig.ParentFolderPath = txtPath.Text.Trim();

            Simile.Factory.MusicFileFactory mFileFactory = new Factory.MusicFileFactory();
            mFileFactory.Process();

            List<string> artistsTagValues = mFileFactory.ArtistsTagValue;
             List<TemplateMusicFileTag> listOfMusicFileTagTempData = mFileFactory.ListOfMusicFileTagTempData;

             dataGridView2.DataSource =  listOfMusicFileTagTempData;


             //IEnumerable<TemplateMusicFileTag> result = from r in listOfMusicFileTagTempData 
             //            orderby r.ArtistTagVale 
             //               select new TemplateMusicFileTag
             //               { ArtistTagVale = r.ArtistTagVale,IsArtist = r.IsArtist, IsAlbum = r.IsAlbum, IsBand = r.IsBand,
             //                   ErrorMessage = r.ErrorMessage, FileName = r.FileName}
             //               ;
             //dataGridView2.DataSource = result.ToArray(); // listOfMusicFileTagTempData;

            dataGridView2.Columns[1].Width = 40;
            dataGridView2.Columns[2].Width = 40;
            dataGridView2.Columns[3].Width = 40;
            dataGridView2.Columns[4].Width = 85;

        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            List<TemplateMusicFileTag> listOfMusicFileTagTempData = null;
            listOfMusicFileTagTempData = (List<TemplateMusicFileTag>)dataGridView2.DataSource;

            MusicAlbum musicAlbum = null;
            MusicArtist musicArtist = null;
            MusicBand musicBand = null;
       
            MusicPlayEntities3 dbContext = new MusicPlayEntities3();

            foreach (TemplateMusicFileTag val in listOfMusicFileTagTempData)
            {
                if (val == null) continue;
                if (val.IsAlbum)
                {
                    using (var modelContext = new MusicPlayEntities3())
                    {
                        musicAlbum = new MusicAlbum();
                        musicAlbum.AlbumName = val.ArtistTagVale.Trim();
                        modelContext.MusicAlbums.Add(musicAlbum);
                        modelContext.SaveChanges();
                    }

                }
                else if (val.IsArtist)
                {
                    using (var modelContext = new MusicPlayEntities3())
                    {
                        musicArtist = new MusicArtist();
                        musicArtist.ArtistName = val.ArtistTagVale.Trim();
                        modelContext.MusicArtists.Add(musicArtist);
                        modelContext.SaveChanges();
                    }
                }
                else if (val.IsBand)
                {
                    using (var modelContext = new MusicPlayEntities3())
                    {
                        musicBand = new MusicBand();
                        musicBand.BandName = val.ArtistTagVale.Trim();
                        modelContext.MusicBands.Add(musicBand);
                        modelContext.SaveChanges();
                    }

                }
            }
        }
    }
}
