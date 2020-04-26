using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Coursework2
{
   public class Customer
    {   
        //  private values
        private string name;
        private string address;
   
        //  Constructor, customer may only be added with address and name
        public Customer(string name, string address)
        {
            //check if name box not empty
            if (name.Length > 1)
            {
                this.name = name;
            }
            else
            {
                throw new ArgumentException("Name field empty");
            }
                
            if (address.Length > 1)   //check if address box not empty.
            {
                this.address = address;
            }
            else
            {
                throw new ArgumentException("Address field empty");
            }
                
        }

        //  Get/Set accessors 
        public string Name
        {
            set
            {
                if (value.Length > 1)
                    name = value;
                else
                    throw new ArgumentException("Name field empty");
            }
            get { return name; }

        }
        public string Address
        {
           set
            {
                if (value.Length > 1)
                    address = value;
                else
                    throw new ArgumentException("address field empty");
            }

            get { return address; }
        }
    }
}
