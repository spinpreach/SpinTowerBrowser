using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Spinpreach.ITowerBase.database.tables;

namespace Spinpreach.ITowerBase.database
{
    public class Tables
    {
        public List<Team> team { get; set; } = new List<Team>();
        public List<Rest> rest { get; set; } = new List<Rest>();
    }
}