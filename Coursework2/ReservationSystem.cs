using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Coursework2
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
    public class ReservationSystem
    {

        List<Customer> customers = new List<Customer>();

        public List<Customer> ListCustomer()
        {
            return customers;
        }
        public List<Booking> ListBookings(string custName)
        {
            foreach (Customer cust in customers)
            {
                if (custName == cust.Name)
                    return cust.GetBookings();
            }

            return null;

        }


        //CUSTOMER METHODS 
        public void AddCustomer(string name, string address)
        {
            if (CustomerExists(name) == false)
            {
                try
                {
                    customers.Add(new Customer(name, address));

                }
                catch (Exception excep)
                {
                    MessageBox.Show("Invalid customer information");
                }
            }
            else MessageBox.Show("Customer already exists");

        }
        public void AmendCustomer(string reference, string name, string address)
        {


            int newRef = 0;
            int.TryParse(reference, out newRef);
            if (newRef != 0)
            {

                //Find and replace data of the specific customer.
                foreach (Customer cust in customers)
                {
                    if (cust.CustomerRefNo == newRef)
                    {
                        cust.Name = name;
                        cust.Address = address;

                        return;
                    }

                }
                MessageBox.Show("Invalid Reference number");
            }
            else MessageBox.Show("Invalid Reference number");




        }
        public bool CustomerExists(string newname)
        {

            bool exists = false;

            foreach (Customer a in customers)
            {
                if (newname == a.Name)
                    exists = true;
                else exists = false;
            }

            return exists;

        }
        public void DeleteCustomer(string name)
        {

            foreach (Customer cust in customers)
            {
                if (cust.Name == name)
                {
                    if (cust.CountBooking() == 0)
                    {
                        if (MessageBox.Show("Delete this Customer?", " ", MessageBoxButton.YesNo, MessageBoxImage.Warning) == MessageBoxResult.Yes)
                        {
                            customers.Remove(cust);
                            return;
                        }
                        else return;
                    }
                    else MessageBox.Show("Error: Cannot delete customer with bookings in progress. "
                                            + "Customer currently has " + cust.CountBooking() + " bookings");
                    return;
                }

            }

            MessageBox.Show("Error Invalid Customer Name.");
        }

        //BOOKING METHODS 
        public void AddBooking(string cust_name, string cust_address, string arrival, string depart)
        {

            //turn strings to DateTime format. Use d/M because system will adapt to either single or double int formating
            try
            {
                DateTime arr = DateTime.ParseExact(arrival, "d/M/yyyy", CultureInfo.InvariantCulture);
                DateTime dep = DateTime.ParseExact(depart, "d/M/yyyy", CultureInfo.InvariantCulture);

                //Check if the customer already exists.
                if (CustomerExists(cust_name))
                {
                    foreach (Customer customer in customers)
                    {
                        if (cust_name == customer.Name)
                        {
                            customer.AddBooking(arr, dep);
                        }
                    }

                }
                else
                {

                    AddCustomer(cust_name, cust_address);
                    foreach (Customer customer in customers)
                    {
                        if (cust_name == customer.Name)
                        {
                            customer.AddBooking(arr, dep);
                        }
                    }
                }

            }
            catch
            {
                MessageBox.Show("Error: Invalid date format. Please Use DD/MM/YYYY");
            }



        }
        public void AmendBooking(string reference, string arrival, string depart)
        {

            try
            {
                DateTime arr = DateTime.ParseExact(arrival, "d/M/yyyy", CultureInfo.InvariantCulture);
                DateTime dep = DateTime.ParseExact(depart, "d/M/yyyy", CultureInfo.InvariantCulture);

                int tempRef = 0;
                int.TryParse(reference, out tempRef);

                foreach (Customer cust in customers)
                {
                    foreach (Booking book in cust.GetBookings())
                    {
                        if (book.ReferenceNo == tempRef)
                        {
                            book.ArrivalDate = arr;
                            book.DapartureDate = dep;
                            return;
                        }
                    }
                }

                MessageBox.Show("Invalid booking numer");
            }
            catch
            {
                MessageBox.Show("Invalid date format");
            }
        }
        public Booking GetBooking(string reference)
        {
            try
            {
                int bookingRefNo = int.Parse(reference);
                foreach (Customer cust in customers)
                {
                    foreach (Booking book in cust.GetBookings())
                    {
                        if (bookingRefNo == book.ReferenceNo)
                        {
                            return book;
                        }

                    }
                }
            }
            catch
            {
                MessageBox.Show("Invalid reference format.");
            }

            return null;
        }
        public void DeleteBooking(string bookingRef)
        {

            List<Booking> temp;

            int tempRef = 0;
            int.TryParse(bookingRef, out tempRef);

            foreach (Customer cust in customers)
            {
                temp = cust.GetBookings();
                foreach (Booking booking in temp)
                    if (booking.ReferenceNo == tempRef)
                    {
                        cust.DeleteBooking(booking.ReferenceNo);
                        return;
                    }
                    else
                    {
                        MessageBox.Show("Invalid reference number");
                    }
            }

        }
        public bool BookingExists(string bookingRef)
        {
            //change string to int
            int Ref = 0;
            int.TryParse(bookingRef, out Ref);

            foreach (Customer cust in customers)
            {
                if (cust.ContainsBooking(Ref))
                    return true;
            }
            return false;
        }

        //GUEST METHODS
        public void AddGuest(string name, string passNo, string custAge, string bookingRef)
        {
            int bookRef;
            int.TryParse(bookingRef, out bookRef);

            int age;
            int.TryParse(custAge, out age);

            foreach (Customer cust in customers)
            {
                foreach (Booking book in cust.GetBookings())
                {
                    if (book.ReferenceNo == bookRef)
                    {

                        book.AddGuest(name, passNo, age);
                        return;

                    }

                }

            }
            MessageBox.Show("Booking number not found, please provide a valid booking number");
        }
        public void DeleteGuest(string passNo, string bookingRef)
        {
            int bookRef;
            int.TryParse(bookingRef, out bookRef);

            foreach (Customer cust in customers)
            {
                foreach (Booking book in cust.GetBookings())
                {
                    if (book.ReferenceNo == bookRef)
                    {

                        book.DeleteGuest(passNo);
                        return;

                    }
                }
            }

            MessageBox.Show("Could not delete guest, invalid information.");
        }
        public void AmendGuest(string bookingRef, string oldPassNo, string newName, string newPassNo, string newAge)
        {
            int bookRef;
            int.TryParse(bookingRef, out bookRef);

            int age;
            int.TryParse(newAge, out age);

            if (oldPassNo != "" && newName != "" && newPassNo != "" && newAge != "")
            {
                foreach (Customer cust in customers)
                {
                    foreach (Booking book in cust.GetBookings())
                    {
                        if (book.ReferenceNo == bookRef)
                        {
                            book.AmendGuest(oldPassNo, newName, newPassNo, age);
                            return;
                        }
                    }
                }
            }
            else MessageBox.Show("Please fill in all fields");

            
        }


        //EXTRAS METHODS
        public void ExtraBreakfast(string bookRef, string dietReq)
        {
            int Ref;
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
            MessageBox.Show("Invalid booking reference");
        }
        public void ExtraEveningMeal(string bookRef, string dietReq)
        {
            int Ref;
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
            MessageBox.Show("Invalid booking reference");
        }
        public void ExtraCarHire(string bookRef, string startHire, string endHire, string name)
        {
            //convert to appropriate format
           
            try {

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

            }

        }

        //Other
        public List<int> ShowPrice(string bookRef)
        {
            int Ref;
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
            return null;
        }
    }
}
