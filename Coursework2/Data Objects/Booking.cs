using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using Coursework2.Data_Objects;

namespace Coursework2
{
   public class Booking
    {
        //Customer who made the booking. 
        private Customer customer;
        private int localReferenceNumber;  //Reference used only in the booking application, is not passed to the SQL

        private DateTime arrivalDate;
        private DateTime departureDate;

        //Extras for booking.
        List<BookingExtras> extras;

       //Constructor, new booking must have arrival and departure dates.
        public Booking(Customer customer, DateTime arrive, DateTime depart)
        {
            extras = new List<BookingExtras>();
            //check dates
            if (arrive != null)
            {
                arrivalDate = arrive;
            }
            else
            {
                throw new ArgumentException("Arrival date empty");
            }

            //Check if arrival and departure are not the same date
            if (depart != null && depart != arrive)     
            {
                departureDate = depart;
            }
            else
            {
                throw new ArgumentException("Departure date cannot be the same as arrival date.");
            }

            this.customer = customer;
            localReferenceNumber += 1;
        }

        public int ReferenceNumber
        {
            get { return localReferenceNumber; }
        }

        public Customer GetCustomer
        {
            get { return customer; }
        }
        
        public DateTime ArrivalDate
        {
            set
            {
                if (value != null)
                    arrivalDate = value;
                else
                    throw new ArgumentException("Arrival date empty.");
            }
            get { return arrivalDate; }         
         }
        public DateTime DapartureDate
        {
            set
            {
                if (value != null)
                    departureDate = value;
                else
                    throw new ArgumentException("Departure date empty.");
            }
            get { return departureDate; }
        }

        public void AddBreakfast(string dietReq)
        {
            bool exists = false; 
            foreach(BookingExtras bookingExtras in extras)
            {
                if(bookingExtras.GetType() == typeof(ExtraBreakfast))
                {
                    exists = true; 
                }
            }

            if(exists == false)
            {
                extras.Add(new ExtraBreakfast(dietReq));
            }
        }

        public void AddEveningMeal(string dietReq)
        {
            bool exists = false; 

            foreach(BookingExtras bookingExtras in extras)
            {
                if(bookingExtras.GetType() == typeof(ExtraEveningMeal))
                {
                    exists = true; 
                }
            }

            if(exists == false)
            {
                extras.Add(new ExtraEveningMeal(dietReq));
            }
        }

        public void AddCarHire(DateTime start, DateTime end, string name)
        {
            bool exists = false; 
            foreach(BookingExtras bookingExtras in extras)
            {
                if(bookingExtras.GetType() == typeof(ExtraCarHire))
                {
                    exists = true; 
                }
            }

            if(exists == false)
            {
                extras.Add(new ExtraCarHire(name,start,end));
            }
        }

        public List<double> GetBill(List<Guest> guests)
        {
            double breakfastPrice = 5.50;
            double eveningMealPrice = 15.99;
            double carHirePrice = 49.99;

            double roomChild = 28.99;
            double roomAdult = 48.99;

            //Get all prices
            double totalPay = 0; //total for all guests per night
            int totalNights = (departureDate - arrivalDate).Days; // total for number of nights.
         
            double extraBreakfast = 0;    //total for breakfast
            double extraEveningMeal = 0;     //total for evening meal
            double extraExtraCarhire = 0;    //total for Car Hire

            //Calculate cost for guests PER NIGHT
            foreach (Guest guest in guests)
            {
                //check age, if under 18 get discount.
                if (guest.Age < 18)
                {
                    totalPay += roomChild;
                }
                else
                {
                    totalPay += roomAdult;
                }
            }

            foreach(BookingExtras bookingExtras in extras)
            {
                if (bookingExtras.GetType() == typeof(ExtraBreakfast))
                {
                    extraBreakfast = breakfastPrice * totalNights * guests.Count();   //Breakfast for each guest each night.
                }
                else if (bookingExtras.GetType() == typeof(ExtraEveningMeal))
                {
                    extraEveningMeal += eveningMealPrice * totalNights * guests.Count();
                }
                else if(bookingExtras.GetType() == typeof(ExtraCarHire))
                {
                    var carHire = (ExtraCarHire)bookingExtras;

                    extraExtraCarhire = (carHire.EndDate - carHire.StartDate).Days * carHirePrice;
                }
            }

            //List with cost per night of : Guests , breakfast , dinner, carhire , number of nights.
            List<double> total = new List<double> { totalPay, extraBreakfast, extraEveningMeal, extraExtraCarhire, totalNights };
            
            //method returns list, to be used in XAML as invoice.
            return total;
        }
    }
}
