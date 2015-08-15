using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Threading;
using Spinpreach.ITowerBase;
using Spinpreach.ITowerViewer;

namespace Spinpreach.SpinTowerBrowser
{
    static class Program
    {
        /// <summary>
        /// アプリケーションのメイン エントリ ポイントです。
        /// </summary>
        [STAThread]
        static void Main()
        {

            bool run = true;

#if !DEBUG
            // 二重起動を防止する
            using (Mutex mutex = new Mutex(false, Application.ProductName))
            {
                run = mutex.WaitOne(0, false);
            }
#endif

            if (run)
            {
                Application.EnableVisualStyles();
                Application.SetCompatibleTextRenderingDefault(false);

                var login = LoginInfo.Load();
                if (!login.IsExists())
                {
                    var frm = new LoginForm();
                    if (frm.ShowDialog() == DialogResult.OK)
                    {
                        login = frm.LoginData;
                        LoginInfo.Save(login);
                    }
                }

                if (login.IsExists())
                {
                    var sw = new SessionWrapper(8892);
                    var database = new Database(sw);
                    Application.Run(new MainForm(database));
                }
            }

        }
    }

}
