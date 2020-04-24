using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Data.SqlClient;
using System.Data.SQLite;

namespace Coursework2
{
    public class DataBase : IDataBase
    {
        //establish sql connection
        private SQLiteConnection sqlite_conn;
        private bool setupDone; 

        public DataBase()
        {
            sqlite_conn = CreateConnection();
        }

        public bool SetUp()
        {

            if (!setupDone)
            {
                try
                {
                    CreateTable(sqlite_conn);
                    return true;
                }
                catch (Exception ex)
                {
                    return false;
                }
            }
            else return false;
        }

        public void InsertLocalData(List<Customer> customers)
        {
            SQLiteCommand sqlite_cmd;
            string command;
            foreach (Customer cust in customers)
            {
                //Add the customer, then add all bookings registered with that customer.
                sqlite_cmd = sqlite_conn.CreateCommand();
                command = "INSERT INTO Customer(Name, Address) " +
                            "SELECT '" + cust.Name + "' , '" + cust.Address + "' " +
                              "WHERE NOT EXISTS(SELECT 1 FROM Customer WHERE Name = '" + cust.Name + "' AND Address = '" + cust.Address + "' ); ";  //check if customer already exists

                sqlite_cmd.CommandText = command;
                sqlite_cmd.ExecuteNonQuery();

                //access and insert bookings into the database. 
                foreach (Booking book in cust.GetBookings())
                {
                    //add guests that are in that booking. 
                    foreach (Guest guest in book.ListGuests())
                    {
                        command = "INSERT INTO Guest(Age, Name, BookingNo, PassportNo) " +
                             "SELECT " + guest.Age + " , '" + guest.Name + "' , " + book.ReferenceNo + " , " + guest.PassportNo + " " +
                               "WHERE NOT EXISTS(SELECT 1 FROM Guest WHERE PassportNo = " + guest.PassportNo + " ); ";  //check if guest already exists

                        sqlite_cmd.CommandText = command;
                        sqlite_cmd.ExecuteNonQuery();
                    }

                    //add booking. booking extras have a 1.1 relationship.
                    command = "INSERT INTO Booking(CustomerRefNo, ArrivalDate, DepartureDate) " +
                             "VALUES ('" + cust.Name.ToString() + "' , '" + book.ArrivalDate.ToString() + "' , '" + book.DapartureDate.ToString() + "' ) ";

                    sqlite_cmd.CommandText = command;
                    sqlite_cmd.ExecuteNonQuery();
                }
            }
        }

        public Customer LoadData()
        {
            //temp single row 
            string name = "poop";
            string address = "poop";

            List<Customer> customers;

            SQLiteDataReader sqlite_datareader;
            SQLiteCommand sqlite_cmd;

            sqlite_cmd = sqlite_conn.CreateCommand();
            sqlite_cmd.CommandText = "SELECT * FROM Customer";

            sqlite_datareader = sqlite_cmd.ExecuteReader();
            while (sqlite_datareader.Read())
            {
                name = sqlite_datareader.GetString(0);
                address = sqlite_datareader.GetString(1);
            }


            Customer temp = new Customer(name, address);

            return temp;
        }

        private static SQLiteConnection CreateConnection()
        {

            SQLiteConnection sqlite_conn;
            // Create a new database connection:
            sqlite_conn = new SQLiteConnection("Data Source=BookingDataBase._db; New = True; Compress = True;");
            // Open the connection:
            try
            {
                sqlite_conn.Open();
            }
            catch (Exception ex)
            {

            }
            return sqlite_conn;
        }

        static void CreateTable(SQLiteConnection conn)
        {

            SQLiteCommand sqlite_cmd;

            string CreateCustomer = "CREATE TABLE Customer(" +
                                   "Name TEXT PRIMARY KEY," +
                                   "Address TEXT NOT NULL)";

            string CreateGuest = "CREATE TABLE Guest( " +
                                 "Age INT, " +
                                 "Name TEXT, " +
                                 "BookingNo INTEGER references Booking(BookingNo), " +
                                 "PassportNo INTEGER PRIMARY KEY)";

            string CreateBooking = "CREATE TABLE Booking(" +
                            "BookingNo INTEGER PRIMARY KEY AUTOINCREMENT, " +
                            "CustomerRefNo TEXT references Customer(Name), " +
                            "ArrivalDate TEXT NOT NULL, " +
                            "DepartureDate TEXT NOT NULL)";

            sqlite_cmd = conn.CreateCommand();

            sqlite_cmd.CommandText = CreateCustomer;
            sqlite_cmd.ExecuteNonQuery();

            sqlite_cmd.CommandText = CreateGuest;
            sqlite_cmd.ExecuteNonQuery();

            sqlite_cmd.CommandText = CreateBooking;
            sqlite_cmd.ExecuteNonQuery();

        }

        public bool DeleteTable()
        {
            SQLiteCommand sqlite_cmd;

            string DeleteCustomer = "DROP TABLE Customer";

            string DeleteGuest = "DROP TABLE Guest";

            string DeleteBooking = "DROP TABLE Booking";

            try
            {
                sqlite_cmd = sqlite_conn.CreateCommand();

                sqlite_cmd.CommandText = DeleteBooking;
                sqlite_cmd.ExecuteNonQuery();

                sqlite_cmd.CommandText = DeleteCustomer;
                sqlite_cmd.ExecuteNonQuery();

                sqlite_cmd.CommandText = DeleteGuest;
                sqlite_cmd.ExecuteNonQuery();

                return true;
            }
            catch
            {
                return false;
            }
        }

        static void InsertData(SQLiteConnection conn)
        {
            SQLiteCommand sqlite_cmd;
            sqlite_cmd = conn.CreateCommand();
            sqlite_cmd.CommandText = "INSERT INTO SampleTable (Col1, Col2) VALUES('Test Text ', 1); ";
            sqlite_cmd.ExecuteNonQuery();
            sqlite_cmd.CommandText = "INSERT INTO SampleTable (Col1, Col2) VALUES('Test1 Text1 ', 2); ";
            sqlite_cmd.ExecuteNonQuery();
            sqlite_cmd.CommandText = "INSERT INTO SampleTable (Col1, Col2) VALUES('Test2 Text2 ', 3); ";
            sqlite_cmd.ExecuteNonQuery();


            sqlite_cmd.CommandText = "INSERT INTO SampleTable1 (Col1, Col2) VALUES('Test3 Text3 ', 3); ";
            sqlite_cmd.ExecuteNonQuery();

        }

        static void ReadData(SQLiteConnection conn)
        {
            SQLiteDataReader sqlite_datareader;
            SQLiteCommand sqlite_cmd;
            sqlite_cmd = conn.CreateCommand();
            sqlite_cmd.CommandText = "SELECT * FROM SampleTable";

            sqlite_datareader = sqlite_cmd.ExecuteReader();
            while (sqlite_datareader.Read())
            {
                string myreader = sqlite_datareader.GetString(0);
                Console.WriteLine(myreader);
            }
            conn.Close();
        }

    }


}