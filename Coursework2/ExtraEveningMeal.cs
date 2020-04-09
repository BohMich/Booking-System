using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Coursework2
{
    class ExtraEveningMeal
    {
        //Class used by booking class. 
        //used to store information aobut dinner options.
        string diatryReq;

        public string DiatryReq
        {
            get { return diatryReq; }
            set { diatryReq = value; }
        }

        public ExtraEveningMeal(string dReq)
        { 
            diatryReq = dReq;
        }
    }
}
