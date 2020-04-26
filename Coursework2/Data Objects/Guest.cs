
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Coursework2
{
    public class Guest
     {
        int bookingReferenceNumber;
        
        public Guest(string name, string passportNumber, int age, int bookingReferenceNumber)
        {
            //Guest must be inicialized with all fields
            //name must not be empty 
            if (name != null)
            {
                this.name = name;
            }
            else
            {
                throw new ArgumentException("Name must not be empty");
            }
            //check if passport is valid
            if (passportNumber.Length < 10 && passportNumber.Length > 1)
            {
                this.passportNo = passportNumber;
            }
            else
            {
                throw new ArgumentException("Passport string length max 10 min 0!");
            }
            //check if age is valid
            if (age < 101 && age > 0)
            {
                this.age = age;
            }
            else
            {
                throw new ArgumentException("Age int range 0-101");
            }

            if (bookingReferenceNumber > 0)
            {
                this.bookingReferenceNumber = bookingReferenceNumber;
            }
            else
            {
                throw new ArgumentException("Invalid booking reference number");
            }
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
        public int BookingReferenceNumber
        {
            get { return bookingReferenceNumber; }
        }
    }
}
