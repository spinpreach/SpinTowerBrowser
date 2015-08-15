using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace Spinpreach.ITowerBase.api
{
    [XmlRootAttribute("searchnext", Namespace = "", IsNullable = false)]
    public class Searchnext
    {
        public object status { get; set; }
        public object userinfo { get; set; }
        [XmlArrayItemAttribute("group", IsNullable = false)]
        public List<inner.Group> groupinfo { get; set; }
        public object search { get; set; }
        public object treasure { get; set; }
        public object battlelog { get; set; }
        public object svtime { get; set; }
    }
}