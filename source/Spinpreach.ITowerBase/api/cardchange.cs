﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace Spinpreach.ITowerBase.api
{
    [XmlRootAttribute("cardchange", Namespace = "", IsNullable = false)]
    public class Cardchange
    {
        public object status { get; set; }
        public object userinfo { get; set; }
        [XmlArrayItemAttribute("group", IsNullable = false)]
        public List<inner.Group> groupinfo { get; set; }
        public object svtime { get; set; }
    }
}