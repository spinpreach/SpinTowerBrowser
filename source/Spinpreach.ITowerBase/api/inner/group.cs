using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace Spinpreach.ITowerBase.api.inner
{
    public class Group
    {
        public int gno { get; set; }
        public string groupname { get; set; }
        public object status { get; set; }
        public object effect { get; set; }
        [XmlElementAttribute("cardinfo")]
        public List<Cardinfo> cardinfo { get; set; }
        public string starttime { get; set; }
        public string endtime { get; set; }
    }
}