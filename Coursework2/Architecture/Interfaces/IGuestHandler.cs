using System.Collections.Generic;

namespace Coursework2.Architecture
{
    public interface IGuestHandler
    {
        void AddGuest(string bookingReferenceNo, string name, string passport, string age);
        void DeleteGuest(string passport);
        List<Guest> ListGuests(string bookingReferenceNumber);
        List<Guest> ListGuests();
    }
}