using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Coursework2
{
    class ExtraBreakfast
    {
        //class used by booking class. 
        //used to store information aobut breakfast option
        string diatryReq;

        public string DiatryReq
        {
            get { return diatryReq; }
            set { diatryReq = value; }
        }

        public ExtraBreakfast(string dReq)
        {
            //constructor which saves diatry requirements as string.
            diatryReq = dReq;
        }
    }
}
