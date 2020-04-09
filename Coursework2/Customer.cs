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
        private int customerRefNo;

        private static int customerCount = 1;   //autoincrementing through the constructor
        private List<Booking> bookings = new List<Booking>(); //Each customer has a list of bookings. 

       
        //  Constructor, customer may only be added with address and name
        public Customer(string n, string a)
        {
            if (n.Length > 1)   //check if name box not empty
                name = n;
            else
                throw new ArgumentException("Name field empty");

            if (a.Length > 1)   //check if address box not empty.
            {
                address = a;
                customerRefNo = customerCount;
                customerCount++;    //autoincrement the static value.
            }
            else
                throw new ArgumentException("Address field empty");
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
        public int CustomerRefNo
        {
            //read only
            get { return customerRefNo; } 
        }
        

        //  Methods
        public List<Booking> GetBookings()
        {
            //return booking list.
            return bookings;
        }  
        public void AddBooking(DateTime a, DateTime d)
        {
            try
            {
                bookings.Add(new Booking(a, d));
            }
            catch
            {
                MessageBox.Show("Invalid booking dates");
            }
        }
        public void DeleteBooking(int bookingRef)
        {
            //find booking using the reference number
            foreach(Booking booking in bookings)
            {
                if(booking.ReferenceNo == bookingRef)
                {
                    //check if user wants to delete the booking.
                    if(MessageBox.Show("Delete this Booking?", " ", MessageBoxButton.YesNo, MessageBoxImage.Warning) == MessageBoxResult.Yes)
                    {
                        //run the remove method from booking class.
                        bookings.Remove(booking);
                        return;
                    }
                }
            }
            MessageBox.Show("Invalid Booking Number");
        }
        public int CountBooking()
        {
            //return number of bookings 
            return bookings.Count();
        }
        public bool ExistsBooking(Booking booking)
        {
            //check if booking exists in the list
            if (bookings.Contains(booking))
                return true;
            else return false;
        }
        public bool ContainsBooking(int bookingRef)
        {   
            //check if booking reference is present
            foreach(Booking book in bookings)
            {
                if (book.ReferenceNo == bookingRef)
                    return true;
            }

            return false;
        }
    }
}
