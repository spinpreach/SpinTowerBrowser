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
using System.Reflection;

using MetroFramework.Forms;
using MetroFramework;

using Spinpreach.ITowerBase;
using Spinpreach.ITowerViewer;

namespace Spinpreach.SpinTowerBrowser
{
    public partial class MainForm : MetroForm
    {

        private Database database;
        private StaticForms form;

        public MainForm(Database database)
        {
            InitializeComponent();
            this.database = database;
            this.form = new StaticForms(database);
            this.ITowerBrowser.LoginCompletedEvent += this.ITowerBrowser_LoginCompleted;
            this.ITowerBrowser.LoginErrorEvent += this.ITowerBrowser_LoginError;
            this.ITowerBrowser.MuteChangedEvent += (isMute) => { this.Invoke(new Action<bool>(ITowerBrowser_MuteImageChange), isMute); };
            Version version = Assembly.GetExecutingAssembly().GetName().Version;
            this.Text = string.Format("回転建機 ver {0}.{1}.{2}", version.Major, version.Minor, version.Build);
        }

        private void MainForm_Shown(object sender, EventArgs e)
        {
            this.ITowerBrowser.Start();
        }

        private void ReloadButton_Click(object sender, EventArgs e)
        {
            string message = "再読み込みしてもいいですか？";
            string title = string.Empty;
            if (MetroMessageBox.Show(this, message, title, MessageBoxButtons.OKCancel, MessageBoxIcon.Warning) == DialogResult.OK)
            {
                this.ITowerBrowser.Start();
            }
        }

        private void SettingButton_Click(object sender, EventArgs e)
        {
            var frm = new LoginForm();
            if (frm.ShowDialog() == DialogResult.OK)
            {
                LoginInfo.Save(frm.LoginData);
                string message = "入力されたアカウントは次回起動時から使用されます。";
                string title = string.Empty;
                MetroMessageBox.Show(this, message, title, MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void MuteButton_Click(object sender, EventArgs e)
        {
            this.ITowerBrowser.ToggleMute();
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

        private void ITowerBrowser_LoginCompleted()
        {
            var isMuted = this.ITowerBrowser.IsMute();
            if (isMuted == null) { return; }
            this.ITowerBrowser_MuteImageChange((bool)isMuted);
        }

        private void ITowerBrowser_LoginError(Exception ex)
        {
            StringBuilder msg = new StringBuilder();
            msg.AppendLine("ログインに失敗しました。");
            msg.AppendLine("");
            msg.AppendLine("※サーバが重い可能性があります。");
            msg.AppendLine("　何度か[再読み込み]をためしてみてください。");
            MessageBox.Show(msg.ToString(), "エラー", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

        private void ITowerBrowser_MuteImageChange(bool isMute)
        {
            switch (isMute)
            {
                case true: this.MuteButton.BackgroundImage = Properties.Resources.MuteonImage; break;
                case false: this.MuteButton.BackgroundImage = Properties.Resources.MuteoffImage; break;
            }
        }

        private void RestButton_Click(object sender, EventArgs e)
        {
            this.form.RestForm_Click();
        }
    }
}