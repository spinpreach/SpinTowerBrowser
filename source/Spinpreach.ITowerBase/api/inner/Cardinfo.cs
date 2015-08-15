using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Spinpreach.ITowerBase.api.inner
{
    public class Cardinfo
    {
        public int id { get; set; }
        public int cardid { get; set; }
        public int lv { get; set; }
        public int exp { get; set; }
        public int nextexp { get; set; }
        public int baseexp { get; set; }
        public int nowhp { get; set; }
        public int maxhp { get; set; }
        public int strength { get; set; }
        public int vitality { get; set; }
        public int agile { get; set; }
        public int technic { get; set; }
        public int luck { get; set; }
        public int hoken { get; set; }
        public int condition { get; set; }
        public string reststarttime { get; set; }
        public string restendtime { get; set; }
        public int buzz { get; set; }
        public int bookid { get; set; }
        public string type { get; set; }
        public int group { get; set; }
        public string name { get; set; }
        public string nickname { get; set; }
        public string rarity { get; set; }
        public int trend { get; set; }
        public int img { get; set; }
        public string imgdate { get; set; }
        public int atnumber { get; set; }
        public string skillid1 { get; set; }
        public string skillid2 { get; set; }
        public string skillid3 { get; set; }
        public object skillid4 { get; set; }
        public int block { get; set; }
        public object equipinfo { get; set; }
        public int charstarthp { get; set; }
        public int charendhp { get; set; }
        public int charstarttone { get; set; }
        public int charendtone { get; set; }
    }
}
