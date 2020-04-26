
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
        string dietaryRequirements;

        public string DietaryRequirements
        {
            get { return dietaryRequirements; }
            set { dietaryRequirements = value; }
        }

        public ExtraEveningMeal(string requirements)
        { 
            dietaryRequirements = requirements;
        }
    }
}
