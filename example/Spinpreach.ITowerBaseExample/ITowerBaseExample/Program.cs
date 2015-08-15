using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Spinpreach.ITowerBase;

namespace ITowerBaseExample
{
    class Program
    {
        static void Main(string[] args)
        {
            SessionWrapper sw = new SessionWrapper(8892);
            while (true) System.Threading.Thread.Sleep(1000);
        }
    }
}