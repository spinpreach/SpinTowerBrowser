using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Spinpreach.ITowerBase.database.tables
{
    public class Rest : api.inner.Rest
    {
        internal static List<Rest> Insert(List<api.inner.Rest> value)
        {
            var ret = new List<Rest>();
            try
            {
                value.ForEach(x =>
                {
                    var add = new Rest();
                    add.no = x.no;
                    add.charid = x.charid;
                    add.reststarttime = x.reststarttime;
                    add.restendtime = x.restendtime;
                    add.cardinfo = x.cardinfo;
                    ret.Add(add);
                });
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
            return ret;
        }
    }
}