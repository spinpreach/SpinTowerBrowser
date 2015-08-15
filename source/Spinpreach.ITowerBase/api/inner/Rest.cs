using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace Spinpreach.ITowerBase.api.inner
{
    public class Rest
    {
        public int no { get; set; }
        public int charid { get; set; }
        public string reststarttime { get; set; }
        public string restendtime { get; set; }
        public Cardinfo cardinfo { get; set; }
    }
}