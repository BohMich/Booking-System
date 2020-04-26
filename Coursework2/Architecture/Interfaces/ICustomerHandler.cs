using System.Collections.Generic;

namespace Coursework2.Architecture.Interfaces
{
    public interface ICustomerHandler
    {
        void AddCustomer(string name, string address);
        void DeleteCustomer(string name, string address);
        List<Customer> GetCustomerList();
    }
}