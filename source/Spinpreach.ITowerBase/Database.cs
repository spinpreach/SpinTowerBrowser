using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Spinpreach.ITowerBase.database;

namespace Spinpreach.ITowerBase
{
    public class Database
    {

        public Tables tables { get; set; } = new Tables();
        public Views views
        {
            get
            {
                return new Views(this.tables);
            }
        }

        private SessionWrapper sw;

        public Database() { }
        public Database(SessionWrapper sw)
        {
            this.sw = sw;
            sw.Batheinit_Complete += this.Batheinit_Complete;
            sw.Cardchange_Complete += this.Cardchange_Complete;
            sw.Searchnext_Complete += this.Searchnext_Complete;
            sw.Groupexit_Complete += this.Groupexit_Complete;
            sw.Cardmultidel_Complete += this.Cardmultidel_Complete;
            sw.Restset_Complete += this.Restset_Complete;
            sw.Restinit_Complete += this.Restinit_Complete;
        }

        public event Action Team_Changed;
        public event Action Rest_Changed;

        private void Batheinit_Complete(api.Batheinit value)
        {
            this.tables.team = database.tables.Team.Insert(value.groupinfo);
            this.Team_Changed?.Invoke();
        }

        private void Cardchange_Complete(api.Cardchange value)
        {
            this.tables.team = database.tables.Team.Insert(value.groupinfo);
            this.Team_Changed?.Invoke();
        }

        private void Searchnext_Complete(api.Searchnext value)
        {
            this.tables.team = database.tables.Team.Insert(value.groupinfo);
            this.Team_Changed?.Invoke();
        }

        private void Groupexit_Complete(api.Groupexit value)
        {
            this.tables.team = database.tables.Team.Insert(value.groupinfo);
            this.Team_Changed?.Invoke();
        }

        private void Cardmultidel_Complete(api.Cardmultidel value)
        {
            this.tables.team = database.tables.Team.Insert(value.groupinfo);
            this.Team_Changed?.Invoke();
        }

        private void Restset_Complete(api.Restset value)
        {
            this.tables.rest = database.tables.Rest.Insert(value.restlist);
            this.Rest_Changed?.Invoke();
        }

        private void Restinit_Complete(api.Restinit value)
        {
            this.tables.rest = database.tables.Rest.Insert(value.restlist);
            this.Rest_Changed?.Invoke();
        }

    }
}