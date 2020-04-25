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
        public  List<Customer> customers;

        public CustomerHandler()
        {
            customers = new List<Customer>();
        }
        public void AddCustomer(string name, string address)
        {
            Customer newCustomer = new Customer(name, address);

            if (customers.Contains(newCustomer) == false)
            {
                customers.Add(newCustomer);
            }
            else
            {
                throw new ArgumentException("Customer exists");
            }
        }
        public void DeleteCustomer(string name)
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
        public void AmmendCustomer(string name, string address)
        {
            
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
