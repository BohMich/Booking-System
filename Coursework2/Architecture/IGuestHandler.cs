namespace Coursework2.Architecture
{
    public interface IGuestHandler
    {
        void AddGuest(Booking booking, string name, string passport, int age);
        void DeleteGuest(string passport);
    }
}