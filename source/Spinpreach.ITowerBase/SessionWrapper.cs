using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using Nekoxy;
using Spinpreach.ITowerBase.api;

namespace Spinpreach.ITowerBase
{

    public class SessionWrapper
    {

        public Action<Batheinit> Batheinit_Complete;
        public Action<Cardchange> Cardchange_Complete;
        public Action<Searchnext> Searchnext_Complete;
        public Action<Groupexit> Groupexit_Complete;
        public Action<Cardmultidel> Cardmultidel_Complete;
        public Action<Restset> Restset_Complete;
        public Action<Restinit> Restinit_Complete;

        public int ProxyPort { get; }
        private Searchnext searchnext = null;

        public SessionWrapper(int ProxyPort)
        {
            this.ProxyPort = ProxyPort;
            Proxy.AfterSessionComplete_Text += this.AfterSessionComplete_Debug;
            Proxy.AfterSessionComplete_Text += this.AfterSessionComplete_Text;
            Proxy.Startup(ProxyPort);
        }

        private void AfterSessionComplete_Debug(Session s)
        {
            var query = new RequestQuery(s.Request.PathAndQuery);
            var apiname = query.GetCtl("ctl");
            if (string.Empty.Equals(apiname)) return;

            var xmldoc = new XmlDocument();
            xmldoc.LoadXml(Encoding.UTF8.GetString(Utilities.Unzip(s.Response.Body)));

#if DEBUG
            Utilities.HistoryWriter(apiname, xmldoc);
#endif

            Utilities.XmlWriter(apiname, xmldoc);

            if (!ApisExtension.List().Contains(apiname))
            {
                Console.WriteLine("******************************************************");
                Console.WriteLine(string.Format("未登録のAPIが来ました。: apiname = {0}", apiname));
                Console.WriteLine(s.Request.PathAndQuery);
                Console.WriteLine("******************************************************");
            }
        }

        private void AfterSessionComplete_Text(Session s)
        {

            try
            {
                var query = new RequestQuery(s.Request.PathAndQuery);
                var apiname = query.GetCtl("ctl");
                if (string.Empty.Equals(apiname)) return;

                var apis = ApisExtension.List();
                if (!apis.Contains(apiname)) return; // 未登録のAPIの場合は抜けちゃうよ。

                //小細工用「searchnext」
                if (this.searchnext != null)
                {
                    Searchnext_Complete?.Invoke(this.searchnext);
                    this.searchnext = null;
                }

                Console.WriteLine(string.Format("apiname = {0}", apiname));
                switch (ApisExtension.Value(apiname))
                {
                    case APIS.batheinit: Batheinit_Complete?.Invoke(Utilities.Deserialize<Batheinit>(s.Response.Body)); break;
                    case APIS.cardchange: Cardchange_Complete?.Invoke(Utilities.Deserialize<Cardchange>(s.Response.Body)); break;
                    case APIS.searchnext: this.searchnext = Utilities.Deserialize<Searchnext>(s.Response.Body); break;
                    case APIS.groupexit: Groupexit_Complete?.Invoke(Utilities.Deserialize<Groupexit>(s.Response.Body)); break;
                    case APIS.cardmultidel: Cardmultidel_Complete?.Invoke(Utilities.Deserialize<Cardmultidel>(s.Response.Body)); break;
                    case APIS.restset: Restset_Complete?.Invoke(Utilities.Deserialize<Restset>(s.Response.Body)); break;
                    case APIS.restinit: Restinit_Complete?.Invoke(Utilities.Deserialize<Restinit>(s.Response.Body)); break;
                    default: break;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
        }

    }
}
