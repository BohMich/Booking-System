﻿using System.Collections.Generic;

namespace Coursework2.Architecture.Interfaces
{
    public interface IReservationSystem
    {
        void AddBooking(string cust_name, string cust_address, string arrival, string depart);
        void AddCustomer(string name, string address);
        void AddGuest(string name, string passNo, string custAge, string bookingRef);
        void AmendBooking(string reference, string arrival, string depart);
        void DeleteBooking(string bookingRef);
        void DeleteCustomer(string name, string address);
        void DeleteGuest(string passNo);
        void ExtraBreakfast(string bookRef, string dietReq);
        void ExtraCarHire(string bookRef, string startHire, string endHire, string name);
        void ExtraEveningMeal(string bookRef, string dietReq);
        Booking GetBooking(string reference);
        List<Booking> ListBookings(string custName);
        List<Booking> GetBooking();
        List<Customer> ListCustomer();
        List<double> ShowPrice(string bookRef);
        List<Guest> ListGuests(string bookingReferenceNumber);
        List<Guest> ListGuests();
    }
}