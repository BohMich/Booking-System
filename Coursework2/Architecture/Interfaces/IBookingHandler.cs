using System;
using System.Collections.Generic;

namespace Coursework2.Architecture.Interfaces
{
    public interface IBookingHandler
    {
        List<Booking> GetBookings();
        void AddBooking(Customer customer, string arrive, string depart);
        void AmmendBooking();
        void DeleteBooking(string bookingReference);
        void DeleteBooking(string customerName, string customerAddress);
        List<Booking> GetCustomersBookings(string customareName);
        Booking GetSingleBooking(string referenceNo);
    }
}