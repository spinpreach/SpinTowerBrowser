using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Spinpreach.ITowerBase.database
{
    public class Views
    {
        private Tables tables;

        public Views(Tables tables)
        {
            this.tables = tables;
        }

        public views.Rest rest
        {
            get
            {
                return new views.Rest(tables);
            }
        }

    }
}