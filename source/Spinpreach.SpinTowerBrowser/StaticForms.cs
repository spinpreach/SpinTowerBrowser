using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Spinpreach.ITowerBase;

namespace Spinpreach.SpinTowerBrowser
{
    public class StaticForms
    {
        private Database database;
        
        public StaticForms(Database database)
        {
            this.database = database;
        }

        #region RestForm

        private RestForm _RestForm = null;
        public void RestForm_Click()
        {
            if ((this._RestForm == null) || this._RestForm.IsDisposed)
            {
                this._RestForm = new RestForm(this.database);
                this._RestForm.Show();
            }
            else
            {
                this._RestForm.TopMost = true;
                this._RestForm.TopMost = false;
                if (this._RestForm.WindowState == FormWindowState.Minimized)
                {
                    this._RestForm.WindowState = FormWindowState.Normal;
                }
            }
        }

        #endregion
    }
}