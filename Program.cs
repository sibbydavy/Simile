using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Simile
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            MusicFactory.GlobalUserConfig globalUserConfig = new MusicFactory.GlobalUserConfig();
           // Application.Run(new  Form1()); // Simile.UI.frmMusicProcess()); // frmExtractMusicFileInfo());
           // Application.Run(new Simile.UI.frmMusicProcess()); 
            Application.Run(new Form1()); 
        }
    }
}
