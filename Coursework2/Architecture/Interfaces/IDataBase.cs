using System.Collections.Generic;

namespace Coursework2.Architecture.Interfaces
{
    public interface IDataBase
    {
        bool DeleteTable();
        void InsertLocalData(List<Customer> customers);
        Customer LoadData();
        bool SetUp();
    }
}