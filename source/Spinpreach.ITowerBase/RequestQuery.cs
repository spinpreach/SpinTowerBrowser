using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Spinpreach.ITowerBase
{
    public class RequestQuery
    {

        protected List<string> req { get; private set; } = new List<string>();

        public RequestQuery(string value)
        {
            if (value.StartsWith("/?"))
            {
                value = value.Replace("/?", string.Empty);
                req = value.Split(new string[] { "&" }, StringSplitOptions.None).ToList();
            }
        }

        public string GetCtl(string name)
        {
            string ret = string.Empty;
            string value = string.Format("{0}=", name);
            if (!this.req.Where(x => x.StartsWith(value)).Count().Equals(0))
            {
                ret = this.req.Where(x => x.StartsWith(value)).First().Replace(value, string.Empty);
            }
            return (ret);
        }

    }
}
