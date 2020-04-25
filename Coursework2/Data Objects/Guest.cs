
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Coursework2
{
    public class Guest
     {
        private Booking booking;
        
        public Guest(string newName, string newPassport, int newAge)
        {
            //Guest must be inicialized with all fields
            //name must not be empty 
            if (newName != null)
                name = newName;
            else throw new ArgumentException("Name must not be empty");
            //check if passport is valid
            if (newPassport.Length < 10 && newPassport.Length > 1)
                passportNo = newPassport;
            else
                throw new ArgumentException("Passport string length max 10 min 0!");
            //check if age is valid
            if (newAge < 101 && newAge > 0)
                age = newAge;
            else
                throw new ArgumentException("Age int range 0-101");
        }

        //stores information about guest who stays in the hotel.
        //private values
        private string name;
        private string passportNo;
        private int age;

        //Accessors
        public string Name
        {
            set
            {
                if (value != null)
                    name = value;
                else
                    throw new ArgumentException("Name must not be empty");

            }
            get { return name; }
        }
        public string PassportNo
        {
            set
            {
                if (value.Length < 10 || value.Length > 1)
                    passportNo = value;
                else
                    throw new ArgumentException("Passport string length max 10 min 0!");
            }
            get { return passportNo; }
        }
        public int Age
        {
            set
            {
                if (value < 101 || value > 0)
                    age = value;
                else
                    throw new ArgumentException("Age int range 0-101");
            }
            get
            {
                return age;
            }
        }

       
    }
}
