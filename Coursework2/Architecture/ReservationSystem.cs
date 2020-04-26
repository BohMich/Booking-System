﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using Coursework2.Architecture.Interfaces;

namespace Coursework2.Architecture
{
    /// <summary>
    /// ReservationSystem uses the facade design pattern to isolate the system from the WPF designer/programmer. 
    /// It uses the singleton design pattern to prevent the client from saving booking data to more than one booking system.
    /// It contains al neccessary methods to create/edit/delete all aspects of the booking system structure. 
    /// NOTE: It is doing so without becoming a god object. All interaction with the data is done through accessing the information using 
    /// the customer instances that handle customer details internally. No global access is given to either customer/booking or guest objects. 
    /// </summary>
    //ReservationSystem uses the facade design pattern to interact with other 
    //It contains all neccessary methods to create/edit/delete all aspects of the system.
    //it holds a list of all customers.
    public class ReservationSystem : IReservationSystem
    {
        ICustomerHandler _customerHandler;
        IBookingHandler _bookingHandler;
        IGuestHandler _guestHandler;

        public ReservationSystem(ICustomerHandler c, IBookingHandler b, IGuestHandler g)
        {
            _customerHandler = c;
            _bookingHandler = b;
            _guestHandler = g;
        }

        public List<Customer> ListCustomer()
        {
            return _customerHandler.GetCustomerList();
        }
        public List<Booking> ListBookings(string customareName)
        {
            return _bookingHandler.GetCustomersBookings(customareName);
        }

        //CUSTOMER METHODS 
        public void AddCustomer(string name, string address)
        {
            _customerHandler.AddCustomer(name, address);
        }

        public void DeleteCustomer(string name, string address)
        {
            _bookingHandler.DeleteBooking(name, address); //remove bookings attached to the customer.
            _customerHandler.DeleteCustomer(name, address);
        }

        //BOOKING METHODS 
        public void AddBooking(string customerName, string customerAddress, string arrival, string departure)
        {
            Customer tempCustomer = new Customer(customerName, customerAddress);

            //check if customer is new. 
            if(_customerHandler.GetCustomerList().Contains(tempCustomer) == false)
            {
                _customerHandler.AddCustomer(tempCustomer.Name, tempCustomer.Address);
            }

            //Customers values are important in this situation.
            _bookingHandler.AddBooking(tempCustomer, arrival, departure);
        }
        public void AmendBooking(string reference, string arrival, string depart)
        {
            //_bookingHandler.AmmendBooking(reference, arrival, depart)
        }
        public Booking GetBooking(string reference)
        {
            return _bookingHandler.GetSingleBooking(reference);
        }
        public void DeleteBooking(string bookingReference)
        {
            _bookingHandler.DeleteBooking(bookingReference);
        }
       

        //GUEST METHODS
        public void AddGuest(string name, string passNo, string custAge, string bookingRef)
        {
           
        }
        public void DeleteGuest(string passNo, string bookingRef)
        {
         
        }
        public void AmendGuest(string bookingRef, string oldPassNo, string newName, string newPassNo, string newAge)
        {
          
        }


        //EXTRAS METHODS
        public void ExtraBreakfast(string bookRef, string dietReq)
        {
           /* int Ref;
            int.TryParse(bookRef, out Ref);

            foreach (Customer cust in customers)
            {
                foreach (Booking booking in cust.GetBookings())
                    if (booking.ReferenceNo == Ref)
                    {
                        booking.AddBreakfast(dietReq);
                        return;
                    }

            }
            MessageBox.Show("Invalid booking reference");*/
        }
        public void ExtraEveningMeal(string bookRef, string dietReq)
        {
            /*int Ref;
            int.TryParse(bookRef, out Ref);

            foreach (Customer cust in customers)
            {
                foreach (Booking booking in cust.GetBookings())
                    if (booking.ReferenceNo == Ref)
                    {
                        booking.AddEveningMeal(dietReq);
                        return;
                    }

            }
            MessageBox.Show("Invalid booking reference");*/
        }
        public void ExtraCarHire(string bookRef, string startHire, string endHire, string name)
        {
           /* //convert to appropriate format

            try
            {

                int Ref;
                int.TryParse(bookRef, out Ref);

                DateTime start = DateTime.ParseExact(startHire, "d/M/yyyy", CultureInfo.InvariantCulture);
                DateTime end = DateTime.ParseExact(endHire, "d/M/yyyy", CultureInfo.InvariantCulture);

                foreach (Customer cust in customers)
                    foreach (Booking booking in cust.GetBookings())
                        if (booking.ReferenceNo == Ref)
                        {
                            booking.AddCarHire(start, end, name);
                            return;
                        }

                MessageBox.Show("Invalid booking reference");
            }
            catch
            {
                MessageBox.Show("Invalid information provided ");

            }*/

        }

        //Other
        public List<int> ShowPrice(string bookRef)
        {
            /*int Ref;
            int.TryParse(bookRef, out Ref);

            foreach (Customer cust in customers)
            {
                foreach (Booking book in cust.GetBookings())
                {
                    if (book.ReferenceNo == Ref)
                    {
                        return book.GetBill();
                    }
                }
            }
            return null;*/
            return null;
        }
        public static int RefToInt(string reference)
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
