using System.Collections.Generic;

namespace Coursework2.Architecture.Interfaces
{
    public interface ICustomerHandler
    {
        void AddCustomer(string name, string address);
        void AmmendCustomer(string name, string address);
        void DeleteCustomer(string name);
        List<Customer> GetCustomerList();
    }
}