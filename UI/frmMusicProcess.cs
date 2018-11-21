using MusicFactory;
using Simile.Algorithm;
using Simile.ViewModel;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Simile.UI
{
    public partial class frmMusicProcess : Form
    {
        public frmMusicProcess()
        {
            InitializeComponent();
        }

        private void btnGo_Click(object sender, EventArgs e)
        {
            DistilTitle distilTitle = new DistilTitle();
            TitleExtract titleExtract = new TitleExtract();

            GlobalUserConfig config = new GlobalUserConfig();
            GlobalUserConfig.ParentFolderPath = txtPath.Text.Trim();
            DirectoryProcess dProcess = new DirectoryProcess();
            List<DirectoryInfo> dirList = dProcess.GetDirectories();
            FileInfo[] musicFiles = null;
            TagLib.File tagMusicFile = null;

            Simile.Algorithm.AlgorithmFactory algFactory = new Algorithm.AlgorithmFactory();

            //string newTitle = "";
           // string artistOrBandName = "";
          //  Simile.SimileEnums.GroupCategory grpCategory = SimileEnums.GroupCategory.Unknown;
            Simile.DBProcess.MusicBand musicBand = new Simile.DBProcess.MusicBand();
            Simile.DBProcess.MusicArtist musicArtist = new Simile.DBProcess.MusicArtist();

            List<MusicFileViewModel> listMusicFileViewModel = new List<MusicFileViewModel>();
            MusicFileViewModel musicFileViewModel = null;

            List<clsErrorRpt> errorRpts = new List<clsErrorRpt>();
            clsErrorRpt errR = null;
            foreach (DirectoryInfo dir in dirList)
            {

                musicFiles = dir.GetFiles(GlobalUserConfig.FileType);

               

                foreach (FileInfo finfo in musicFiles)
                {
                    try
                    {
                      

                        FileInfo musicFile = finfo;

                        //
                          errR = new clsErrorRpt();
                          errR.FileName = musicFile.Name;
                          errR.Path = musicFile.FullName;

                        //

                        tagMusicFile = TagLib.File.Create(musicFile.FullName);

                        //if tag is null then avoid the file and get next file.
                        if (tagMusicFile.Tag == null) continue;
                        


                        musicFileViewModel = new MusicFileViewModel();
                        musicFileViewModel.Title = tagMusicFile.Tag.Title.Trim(); // newTitle.Trim();
                        musicFileViewModel.FileName = musicFile.Name.Trim();
                        musicFileViewModel.Path = musicFile.DirectoryName.Trim();

                        var artistName = "";
                        var bandName = "";

                        
                        //----------------------------
                        if (tagMusicFile.Tag.AlbumArtists != null && tagMusicFile.Tag.AlbumArtists.Count() > 0)
                        {
                            string tmpAlbArtists = "";
                            var test = tagMusicFile.Tag.AlbumArtists;
                            foreach (var val in test)
                              {
                                  tmpAlbArtists += val.Trim();
                            }

                            if (!string.IsNullOrEmpty(tmpAlbArtists))
                            {
                                using (var modelContext = new MusicPlayEntities3())
                                {
                                    Simile.MusicArtist tmpArtist = modelContext.MusicArtists.Where(r => r.ArtistName.ToUpper() == tmpAlbArtists.ToUpper()).FirstOrDefault();
                                    if (tmpArtist != null)
                                    {
                                        musicFileViewModel.Artist = tmpArtist.ArtistName.Trim();
                                        musicFileViewModel.ArtistId = tmpArtist.Id;
                                    }
                                }

                                using (var modelContext = new MusicPlayEntities3())
                                {
                                    Simile.MusicBand tmpBand = modelContext.MusicBands.Where(r => r.BandName.ToUpper() == tmpAlbArtists.ToUpper()).FirstOrDefault();
                                    if (tmpBand != null)
                                    {
                                        musicFileViewModel.Band = tmpBand.BandName.Trim();
                                        musicFileViewModel.BandId = tmpBand.Id;
                                    }
                                }


                            }
                        }


                        //-----------------------------


                        
                        //////////////////



                        if (musicFileViewModel.BandId == 0 )
                        {
                            var tmpBandName = musicFile.Directory.Name.Trim();

                            using (var modelContext = new MusicPlayEntities3())
                            {
                                Simile.MusicBand tmpBand = modelContext.MusicBands.Where(r => r.BandName.ToUpper() == tmpBandName.ToUpper()).FirstOrDefault();
                                if (tmpBand != null)
                                {
                                    musicFileViewModel.Band = tmpBand.BandName.Trim();
                                    musicFileViewModel.BandId = tmpBand.Id;
                                }
                            }

                        }

                        if (musicFileViewModel.ArtistId == 0 )
                        {
                            var tmpArtistName = musicFile.Directory.Name.Trim();
                            using (var modelContext = new MusicPlayEntities3())
                            {
                                Simile.MusicArtist tmpArtist = modelContext.MusicArtists.Where(r => r.ArtistName.ToUpper() == tmpArtistName.ToUpper()).FirstOrDefault();
                                if (tmpArtist != null)
                                {
                                    musicFileViewModel.Artist = tmpArtist.ArtistName.Trim();
                                    musicFileViewModel.ArtistId = tmpArtist.Id;
                                }
                            }

                        }

                        ////////////////////

                        if (!string.IsNullOrEmpty(tagMusicFile.Tag.Comment))
                        {
                            var splitComments = tagMusicFile.Tag.Comment.Split(';');

                            foreach (var val in splitComments)
                            {
                                var childSplit = val.ToString().Split('=');
                                if (childSplit.Length == 2)
                                {
                                    if (childSplit[0].Trim() == "Artist")
                                    {
                                        artistName = childSplit[1].Trim();
                                    }

                                    if (childSplit[0].Trim() == "Band")
                                    {
                                        bandName = childSplit[1].Trim();
                                    }
                                }
                            }
                           
                        }

                        if (musicFileViewModel.BandId == 0 && !string.IsNullOrEmpty(bandName))
                        {
                            using (var modelContext = new MusicPlayEntities3())
                            {
                                Simile.MusicBand tmpBand = modelContext.MusicBands.Where(r => r.BandName.ToUpper() == bandName.ToUpper()).FirstOrDefault();
                                musicFileViewModel.Band = tmpBand.BandName.Trim();
                                musicFileViewModel.BandId = tmpBand.Id;
                            }
                        }

                        if (musicFileViewModel.ArtistId == 0 && !string.IsNullOrEmpty(artistName))
                        {
                            using (var modelContext = new MusicPlayEntities3())
                            {
                                Simile.MusicArtist tmpArtist = modelContext.MusicArtists.Where(r => r.ArtistName.ToUpper() == artistName.ToUpper()).FirstOrDefault();
                                musicFileViewModel.Artist = tmpArtist.ArtistName.Trim();
                                musicFileViewModel.ArtistId = tmpArtist.Id;
                            }
                        }

                      

                      listMusicFileViewModel.Add(musicFileViewModel);

                    }
                    catch (Exception errMsg)
                    {
                        errR.Error = errMsg.Message;
                        errorRpts.Add(errR);
                        MessageBox.Show(errMsg.Message);
                    }
                }// end of for loop

               
            }//end of dir for loop

            dgvMusicFiles.DataSource = listMusicFileViewModel;
        }
    }
}