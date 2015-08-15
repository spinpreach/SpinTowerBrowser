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
using Spinpreach.ITowerBase;

namespace Spinpreach.SpinTowerBrowser
{
    public partial class RestForm : MetroForm
    {
        private Database database;
        private System.Timers.Timer OneTimer = new System.Timers.Timer(1000);
        private event Action action;
        public RestForm(Database database)
        {
            InitializeComponent();
            this.database = database;
            action = () => { this.Invoke(new Action(Rest_Changed)); };
            this.database.Rest_Changed += action;
            this.OneTimer.Elapsed += (sender, e) => { this.Invoke(new Action(Rest_Changed)); };
            this.OneTimer.Start();
            this.RestView.Style();
        }

        private void Rest_Changed()
        {
            if (!database.tables.rest.Count.Equals(0))
            {
                this.RestView.DataSource = database.views.rest.view();
            }
        }

        private void RestForm_Shown(object sender, EventArgs e)
        {
            this.Rest_Changed();
        }

        private void RestForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            this.database.Rest_Changed -= action;
        }

    }
}