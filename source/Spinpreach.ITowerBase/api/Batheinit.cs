using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace Spinpreach.ITowerBase.api
{
    [XmlRootAttribute("batheinit", Namespace = "", IsNullable = false)]
    public class Batheinit
    {
        public object status { get; set; }
        public object userinfo { get; set; }
        [XmlArrayItemAttribute("group", IsNullable = false)]
        public List<inner.Group> groupinfo { get; set; }
        public object useriteminfo { get; set; }
        public object bathlist { get; set; }
        public object batheinfo { get; set; }
        public object svtime { get; set; }
    }
}