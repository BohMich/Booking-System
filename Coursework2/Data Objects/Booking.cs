using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Coursework2
{
   public class Booking
    {
        //Customer who made the booking. 
        private Customer customer;
        private int clientReferenceNumber;  //Reference used only in the booking application, is not passed to the SQL

        private DateTime arrivalDate;
        private DateTime departureDate;

        //Extras for booking.
        private ExtraBreakfast breakfast = null;
        private ExtraEveningMeal meal = null;
        private ExtraCarHire carhire = null;

       //Constructor, new booking must have arrival and departure dates.
        public Booking(Customer customer, DateTime arrive, DateTime depart)
        {
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
            clientReferenceNumber += 1;
        }

        public int ReferenceNumber
        {
            get { return clientReferenceNumber; }
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
        
        /*public void AddGuest(string name, string passport, int age)
        {
            //Adds guest to list of guests
            //Check if guest count is acceptable
            if (guests.Count <= 3)
            {
                try
                {   
                    foreach(Guest guest in guests)
                    { 
                        //passport number = primary key
                        if(guest.PassportNo == passport)
                        {
                            MessageBox.Show("Guest with this passport number already exists");
                            return;
                        }
                    }
                    //Exiting foreach loop == guest not present in the list
                    //Add new guest
                    guests.Add(new Guest(name, passport, age));
                }
                catch
                {
                    MessageBox.Show("Invalid Guest information");
                }
            }
            else
                MessageBox.Show("Maximum number of guests reached");
        }*/
       /* public void DeleteGuest(string passport)
        { 
            //deletes a guests from the guest list
            //iterate thorugh guests
            //primary key == passport
            foreach(Guest guest in guests)
            {
                if(guest.PassportNo == passport)
                {
                    guests.Remove(guest);
                    return;
                }         
            }
            MessageBox.Show("Customer not found");
        }   */  
       /* public void AmendGuest(string oldpassport, string nName, string nPassport, int nAge)
        {
            //Changes the values of the guest in the list.
            foreach(Guest guest in guests)
            {
                //Find existing guest
                if(guest.PassportNo == oldpassport)
                {   
                    //Fill in new details
                    try
                    {
                        guest.Name = nName;
                        guest.PassportNo = nPassport;
                        guest.Age = nAge;

                        return;
                    }
                    catch
                    {
                        MessageBox.Show("Invalid guest information");
                    }
                }
                
            }
            MessageBox.Show(" Cannot amend, guest not found");
        }*/
     /*   public Guest ShowGuest(string passport)
        {
            //returns a specific guest, primary key = passport
            foreach(Guest guest in guests)
            {
                if (guest.PassportNo == passport)
                    return guest;
            }

            return null;
        }
        public bool GuestExists(string name)
        {
            //returns true if guest exists. 
            foreach (Guest  guest in guests)
            {
                if (guest.Name == name)
                    return true;
            }
            return false;
        }*/

        public void AddBreakfast(string dietReq)
        {
            //Changes the bookings breakfast reference from null, to a new instance.
            //check if booking already has extra breakfast give option to amend or delete.
            if (breakfast != null)
            {
                //Case : amend the breakfast.
                if (MessageBox.Show("Booking already has breakfast. Amend?", " ", MessageBoxButton.YesNo, MessageBoxImage.Warning) == MessageBoxResult.Yes)
                {
                    //breakfast.DiatryReq = dietReq;
                    return;
                }
                //Case : delete breakfast
                else if (MessageBox.Show("Delete ?", " ", MessageBoxButton.YesNo, MessageBoxImage.Warning) == MessageBoxResult.Yes)
                {
                    breakfast = null;
                }
                else return;
            }
            else
            {   //breakfast points to the new instance.
                breakfast = new ExtraBreakfast(dietReq);
            }
        }

        public void AddEveningMeal(string dietReq)
        {
            //check if booking already has extra meal give option to amend or delete.
            if (meal != null)
            {
                //Case : amend the meal.
                if (MessageBox.Show("Booking already has breakfast. Amend?", " ", MessageBoxButton.YesNo, MessageBoxImage.Warning) == MessageBoxResult.Yes)
                {
                    meal.DietaryRequirements = dietReq;
                    return;
                }
                //Give option to delete the meal
                else if (MessageBox.Show("Delete ?", " ", MessageBoxButton.YesNo, MessageBoxImage.Warning) == MessageBoxResult.Yes)
                {
                    meal = null;
                }
                else return;
            }
            else
            {
                meal = new ExtraEveningMeal(dietReq);
            }
        }

        public void AddCarHire(DateTime start, DateTime end, string name)
        {
            /*//check if dates provided are legitimate. 
            if (start >= arrivalDate && end <= departureDate)
            {
                //check if name provided is in the guest list.
                if (GuestExists(name))
                {
                    //give options to Amend, delete.
                    if(carhire != null)
                    {
                        if (MessageBox.Show("Booking already has a car hired. Amend?", " ", MessageBoxButton.YesNo, MessageBoxImage.Warning) == MessageBoxResult.Yes)
                        {
                            carhire.DriverName = name;
                            carhire.StartDate = start;
                            carhire.EndDate = end;
                            return;
                        }
                        //Give option to delete the meal
                        else if (MessageBox.Show("Delete ?", " ", MessageBoxButton.YesNo, MessageBoxImage.Warning) == MessageBoxResult.Yes)
                        {
                            carhire = null;
                        }
                        else return;
                    }
                    else
                    {
                        carhire = new ExtraCarHire(name, start, end);
                    }
                }
                else MessageBox.Show("Please provide a legitimate name of the guest who will be the driver.");
               
            }
            else MessageBox.Show("Please provide hire dates within the duration of stay.");
           
            */
            
        }

        public List<int> GetBill()
        {
            //Get all prices
            int guestsPerNight = 0; //total for all guests per night
            int noOfNights = (departureDate - arrivalDate).Days; // total for number of nights.
         
            int extraBF = 0;    //total for breakfast
            int extraM = 0;     //total for evening meal
            int extraCH = 0;    //total for Car Hire

          
            //Calculate cost for guests PER NIGHT
            /*foreach(Guest guest in guests)
            {
                //check age, if under 18 get discount.
                if (guest.Age < 18)
                    guestsPerNight += 30;
                else guestsPerNight += 50;
            }*/

            //Assuming Extras will be shown not per day but total.
            if (breakfast != null)
                extraBF = 5*noOfNights;
            if (meal != null)
                extraM = 15*noOfNights;
            if (carhire != null)
            {
                //get the number of days car was hired * 50
                DateTime end = carhire.EndDate;
                DateTime start = carhire.StartDate;
                extraCH = (end - start).Days * 50;
            }

            //List with cost per night of : Guests , breakfast , dinner, carhire , number of nights.
            List<int> total = new List<int> { guestsPerNight, extraBF, extraM, extraCH, noOfNights };
            
            //method returns list, to be used in XAML as invoice.
            return total;
        }
    }
}
