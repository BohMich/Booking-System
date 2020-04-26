using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Coursework2.Architecture.Interfaces;

namespace Coursework2.Architecture
{
    class GuestHandler : IGuestHandler
    {
        private List<Guest> guests;

        public GuestHandler()
        {
            guests = new List<Guest>();
        }
        public void AddGuest(string bookingReferenceNumber, string name, string passport, string age)
        {
            int bookingRef = ReservationSystem.RefToInt(bookingReferenceNumber);
            int guestAge = ReservationSystem.RefToInt(age);
            bool guestExists = false;

            Guest newGuest = new Guest(name,passport,guestAge,bookingRef);

            foreach (Guest guest in guests)
            {
                if(guest.PassportNo == passport && guest.BookingReferenceNumber == bookingRef)  //One guest can be assigned to multiple bookings
                {
                    guestExists = true;
                    break;
                }
            }

            if (guestExists == false)
            {
                guests.Add(newGuest);
            }
            else
            {
                throw new ArgumentException("Guest exists");
            }
        }
        public void DeleteGuest(string passport)
        {
            foreach(Guest guest in guests)
            {
                if(guest.PassportNo == passport)
                {
                    guests.Remove(guest);
                }
            }
        }
        public List<Guest> ListGuests(int bookingReferenceNumber)
        {
            List<Guest> temp = new List<Guest>();

            foreach (Guest guest in guests)
            {
                if (guest.BookingReferenceNumber == bookingReferenceNumber)
                {
                    temp.Add(guest);
                }
            }

            return temp;
        }
    }
}
