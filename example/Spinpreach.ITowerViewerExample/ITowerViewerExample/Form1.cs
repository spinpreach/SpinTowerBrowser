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

namespace Spinpreach.ITowerViewerExample
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            this.ITowerBrowser.LoginCompletedEvent += ITowerBrowser_LoginCompleted;
            this.ITowerBrowser.MuteChangedEvent += (isMute) => { this.Invoke(new Action<bool>(ITowerBrowser_MuteImageChange), isMute); };
        }

        private void Form1_Shown(object sender, EventArgs e)
        {
            this.ITowerBrowser.Start();
        }

        private void StartButton_Click(object sender, EventArgs e)
        {
            this.ITowerBrowser.Start();
        }

        private void ScreenShotButton_Click(object sender, EventArgs e)
        {
            this.ScreenShotButton.Enabled = false;

            string directory = string.Format(@"{0}\{1}", Directory.GetCurrentDirectory(), "ScreenShot");
            if (!Directory.Exists(directory)) { Directory.CreateDirectory(directory); }
            string path = string.Format(@"{0}\{1}.{2}", directory, DateTime.Now.ToString("yyyyMMdd-HHmmss.fff"), "png");

            this.ITowerBrowser.ScreenShot(path);

            this.ScreenShotButton.Enabled = true;
        }

        private void ToggleMuteButton_Click(object sender, EventArgs e)
        {
            this.ITowerBrowser.ToggleMute();
        }

        private void ITowerBrowser_LoginCompleted()
        {
            var IsMute = this.ITowerBrowser.IsMute();
            if (IsMute == null) { return; }
            this.ToggleMuteButton.Text = (bool)IsMute ? "×" : "●";
        }

        private void ITowerBrowser_MuteImageChange(bool isMute)
        {
            this.ToggleMuteButton.Text = isMute ? "×" : "●";
        }

    }
}
