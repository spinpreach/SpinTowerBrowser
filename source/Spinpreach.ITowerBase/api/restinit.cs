using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace Spinpreach.ITowerBase.api
{
    [XmlRootAttribute("restinit", Namespace = "", IsNullable = false)]
    public class Restinit
    {
        public object status { get; set; }
        public object userinfo { get; set; }
        public object useriteminfo { get; set; }
        [XmlArrayItemAttribute("rest", IsNullable = false)]
        public List<inner.Rest> restlist { get; set; }
        public object svtime { get; set; }
    }
}