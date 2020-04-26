using Coursework2;
using Coursework2.Architecture.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Coursework2.Architecture
{
    public class CustomerHandler : ICustomerHandler
    {
        private  List<Customer> customers;

        public CustomerHandler()
        {
            customers = new List<Customer>();
        }
        public void AddCustomer(string name, string address)
        {
            bool customerExists = false;
            Customer newCustomer = new Customer(name, address);

            foreach (Customer customer in customers)
            {
                if(customer.Name == name && customer.Address == address)
                {
                    customerExists = true;
                }
            }

            if (customerExists == false)
            {
                customers.Add(newCustomer);
            }
            else
            {
                throw new ArgumentException("Customer exists");
            }
        }
        public void DeleteCustomer(string name, string address)
        {
            Customer temp = getCustomer(name);
            if (temp != null)
            {
                customers.Remove(temp);
            }
            else
            {
                throw new ArgumentException("Customer can't be deleted: Customer doesn't exist");
            }
        }

        public List<Customer> GetCustomerList()
        {
            return customers;
        }
        private Customer getCustomer(string name)
        {
            foreach (Customer cust in customers)
            {
                if (name == cust.Name)
                {
                    return cust;
                }
            }

            return null;
        }
    }
}
