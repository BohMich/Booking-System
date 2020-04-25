using Coursework2;
using Coursework2.Architecture.Interfaces;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Coursework2.Architecture
{
    public class BookingHandler : IBookingHandler
    {
        private List<Booking> bookings;
        public BookingHandler()
        {
            bookings = new List<Booking>();
        }

        
        public void AddBooking(Customer customer, string arrive, string depart)
        {
            DateTime arr;
            DateTime dep;

            //parse date string into DateTime 
            try
            {
                arr = DateTime.ParseExact(arrive, "d/M/yyyy", CultureInfo.InvariantCulture);
                dep = DateTime.ParseExact(depart, "d/M/yyyy", CultureInfo.InvariantCulture);
            }
            catch
            {
                throw new ArgumentException("Invalid date format");
            }

            Booking newBooking = new Booking(customer, arr, dep);
            
            if(bookings.Contains(newBooking) == false)
            {
                bookings.Add(newBooking);
            }
            else
            {
                throw new ArgumentException("This Booking Already Exists");
            }
        }

        public void DeleteBooking(string bookingReference)
        {
            int refNo = RefToInt(bookingReference);

           foreach(Booking booking in bookings)
            {
                if (booking.ReferenceNumber == refNo)
                {
                    bookings.Remove(booking);
                    return;
                }
            }
            throw new ArgumentException("Booking doesn't exist");
        }

        public List<Booking> GetCustomersBookings(string customerName)
        {
            List<Booking> customerBookings = new List<Booking>();

            foreach (Booking booking in bookings)
            {
                var tempCustomer = booking.GetCustomer;

                //compare booking's customer 
                if (tempCustomer.Name == customerName)
                {
                    customerBookings.Add(booking);
                }
            }
            return customerBookings;
        }
        public void AmmendBooking()
        {
            //TODO
        }
        public Booking GetSingleBooking(string bookingReference)
        {
            int refNo = RefToInt(bookingReference);

            foreach (Booking booking in bookings)
            {
                if(booking.ReferenceNumber == refNo)
                {
                    return booking;
                }
            }
            return null;
        }
        public List<Booking> GetBookings()
        {
            return bookings;
        }
        private int RefToInt(string reference)
        {
            int refNo;
            try
            {
                refNo = int.Parse(reference);
            }
            catch
            {
                throw new ArgumentException("Invalid bookning reference number.");
            }

            return refNo;
        }
    }
}
