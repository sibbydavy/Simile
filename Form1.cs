using DiscogsNet.User;
using MusicFactory;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Simile
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
                     
        }

        private void btnGo_Click(object sender, EventArgs e)
        {
            GlobalUserConfig config = new GlobalUserConfig();
            GlobalUserConfig.ParentFolderPath = txtPath.Text.Trim();
           // DirectoryProcess dprocess = new DirectoryProcess();

            ProcessMusicFiles processMFiles = new ProcessMusicFiles();
            processMFiles.Process();



            //TruncateSymbolFactory symbolFactory = new TruncateSymbolFactory();
            //symbolFactory.Process();
           // TagNameForFileName obj = new TagNameForFileName();
           // obj.Process();
            //TruncasteNumber obj = new TruncasteNumber();
            //obj.Process();

        }

        private void btnFolderName_Click(object sender, EventArgs e)
        {
            GlobalUserConfig config = new GlobalUserConfig();
            GlobalUserConfig.ParentFolderPath = txtPath.Text.Trim();

             List<DirectoryInfo> dir  = null;
             DirectoryProcess dirProcess = new DirectoryProcess();
             dir = dirProcess.GetDirectories();

             List<string> fName = new List<string>();

             StringBuilder strBuilder = new StringBuilder();
             foreach (var folderName in dir)
             {
                 strBuilder.Append("insert into MusicBand (FolderName) values ('");
                 strBuilder.Append(folderName.Name.Trim());
                 strBuilder.Append("');");
                 strBuilder.AppendLine("");
             }
             txtMusicFolderName.Text = strBuilder.ToString();
             //listBox1.DataSource = fName;
            
        }
    }
}
