using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Spinpreach.ITowerBase.database.views
{
    public class Rest
    {
        private Tables tables;
        public Rest(Tables tables)
        {
            this.tables = tables;
        }

        public List<Field> view()
        {
            var ret = new List<Field>();
            this.tables.rest.ForEach(x =>
            {
                long ticks = 0;
                var endtime = new DateTime();
                if (DateTime.TryParse(x.restendtime, out endtime))
                {
                    ticks = endtime.Subtract(DateTime.Now).Ticks;
                }
                var add = new Field(new TimeSpan(ticks));
                add.no = x.no.ToString();
                if (ticks > 0)
                {
                    add.name = x.cardinfo.name;
                    add.lv = x.cardinfo.lv.ToString();
                }
                ret.Add(add);
            });
            for (int i = ret.Count; i < 6; i++)
            {
                var add = new Field(new TimeSpan(0));
                add.no = (i + 1).ToString();
                ret.Add(add);
            }
            return ret;
        }

        public class Field
        {

            private TimeSpan ts;
            public string no { get; set; } = string.Empty;
            public string name { get; set; } = string.Empty;
            public string lv { get; set; } = string.Empty;
            public string time
            {
                get
                {
                    if (this.ts.Ticks <= 0) return string.Empty;
                    int hours = ts.Hours + (ts.Days * 24);
                    int minutes = ts.Minutes;
                    int seconds = ts.Seconds;
                    if (hours.Equals(0))
                    {
                        return string.Format("{0}:{1}", minutes.ToString("00"), seconds.ToString("00"));
                    }
                    else
                    {
                        return string.Format("{0}:{1}:{2}", hours.ToString("#0"), minutes.ToString("00"), seconds.ToString("00"));
                    }
                }
            }

            public Field(TimeSpan ts)
            {
                this.ts = ts;
            }
        }

    }
}