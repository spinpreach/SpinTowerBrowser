using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

using MetroFramework.Forms;
using MetroFramework;

using Spinpreach.ITowerViewer;

namespace Spinpreach.SpinTowerBrowser
{
    public partial class LoginForm : MetroForm
    {

        public LoginInfo LoginData { get; set; } = new LoginInfo();

        public LoginForm()
        {
            InitializeComponent();
            this.Style = MetroColorStyle.Yellow;
        }

        private void LoginButton_Click(object sender, EventArgs e)
        {
            if (string.Empty.Equals(this.UseridTextBox.Text.Trim()))
            {
                MetroMessageBox.Show(this, "DMMアカウントを入力してください。", "", MessageBoxButtons.OK, MessageBoxIcon.Information);
                this.UseridTextBox.Focus();
                return;
            }
            if (string.Empty.Equals(this.PasswordTextBox.Text.Trim()) )
            {
                MetroMessageBox.Show(this, "パスワードを入力してください。", "", MessageBoxButtons.OK, MessageBoxIcon.Information);
                this.PasswordTextBox.Focus();
                return;
            }

            this.LoginData.UserID = this.UseridTextBox.Text.Trim();
            this.LoginData.PassWord = this.PasswordTextBox.Text.Trim();

            this.DialogResult = DialogResult.OK;
        }

    }
}
