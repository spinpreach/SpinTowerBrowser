using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Spinpreach.ITowerBase.database.tables
{
    public class Team : api.inner.Group
    {
        internal static List<Team> Insert(List<api.inner.Group> value)
        {
            var ret = new List<Team>();
            try
            {
                value.ForEach(x =>
                {
                    var add = new Team();
                    add.gno = x.gno;
                    add.groupname = x.groupname;
                    add.status = x.status;
                    add.effect = x.effect;
                    add.cardinfo = x.cardinfo;
                    add.starttime = x.starttime;
                    add.endtime = x.endtime;
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