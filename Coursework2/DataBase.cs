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
    public class DataBase
    {
        //establish sql connection
        private SQLiteConnection sqlite_conn;
        private bool setupDone;

        public DataBase()
        {
            sqlite_conn = CreateConnection();
            //InsertData(sqlite_conn);
            //ReadData(sqlite_conn);
        }

        public bool SetUpDB()
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
       
        private static SQLiteConnection CreateConnection()
        {

            SQLiteConnection sqlite_conn;
            // Create a new database connection:
            sqlite_conn = new SQLiteConnection("Data Source=BookingDataBase.db; New = True; Compress = True;");
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
                                   "ReferenceNo INT PRIMARY KEY," +
                                   "Name TEXT NOT NULL," +
                                   "Address TEXT NOT NULL)";

            string CreateGuest = "CREATE TABLE Guest ( " +
                                 "Age INT, " +
                                 "Name TEXT, " +
                                 "PassportNo INT PRIMARY KEY)";

            string CreateBreakfast = "CREATE TABLE Breakfast ( " +
                                    "BreakfastId INTEGER PRIMARY KEY AUTOINCREMENT, " +
                                    "DietaryRequirements TEXT)";

            string CreateExtraCarHire = "CREATE TABLE ExtraCarHire (" +
                                        "CarId INTEGER PRIMARY KEY AUTOINCREMENT, " +
                                        "StartDate TEXT, " +
                                        "EndDate TEXT)";

            string CreateEveningMeal = "CREATE TABLE EveningMeal (" +
                                  "MealId INTEGER PRIMARY KEY AUTOINCREMENT, " +
                                  "DietaryRequirements TEXT)";


            string CreateBooking = "CREATE TABLE Booking (" +
                            "BookingNo INTEGER PRIMARY KEY AUTOINCREMENT, " +
                            "CustomerRefNo INT references Customer(ReferenceNo), " +
                            "ArrivalDate TEXT NOT NULL, " +
                            "DepartureDate TEXT NOT NULL, " +
                            "BreakfastId INTEGER references Breakfast(BreakfastId), " +
                            "CarHireId INTEGER references ExtraCarHire(CarId), " +
                            "EveningMealId INTEGER references EveningMeal(MealId), " +
                            "GuestId INT references Guest(PassportNo))";

            sqlite_cmd = conn.CreateCommand();

            sqlite_cmd.CommandText = CreateCustomer;
            sqlite_cmd.ExecuteNonQuery();

            sqlite_cmd.CommandText = CreateGuest;
            sqlite_cmd.ExecuteNonQuery();

            sqlite_cmd.CommandText = CreateBreakfast;
            sqlite_cmd.ExecuteNonQuery();

            sqlite_cmd.CommandText = CreateExtraCarHire;
            sqlite_cmd.ExecuteNonQuery();

            sqlite_cmd.CommandText = CreateEveningMeal;
            sqlite_cmd.ExecuteNonQuery();

            sqlite_cmd.CommandText = CreateBooking;
            sqlite_cmd.ExecuteNonQuery();

        }

        public bool DeleteTable()
        {
            SQLiteCommand sqlite_cmd;

            string DeleteCustomer = "DROP TABLE Customer";

            string DeleteGuest = "DROP TABLE Guest";

            string DeleteBreakfast = "DROP TABLE Breakfast";

            string DeleteExtraCarHire = "DROP TABLE ExtraCarHire";

            string DeleteEveningMeal = "DROP TABLE EveningMeal";

            string DeleteBooking = "DROP TABLE Booking";

            try
            {
                sqlite_cmd = sqlite_conn.CreateCommand();

                sqlite_cmd.CommandText = DeleteCustomer;
                sqlite_cmd.ExecuteNonQuery();

                sqlite_cmd.CommandText = DeleteGuest;
                sqlite_cmd.ExecuteNonQuery();

                sqlite_cmd.CommandText = DeleteBreakfast;
                sqlite_cmd.ExecuteNonQuery();

                sqlite_cmd.CommandText = DeleteExtraCarHire;
                sqlite_cmd.ExecuteNonQuery();

                sqlite_cmd.CommandText = DeleteEveningMeal;
                sqlite_cmd.ExecuteNonQuery();

                sqlite_cmd.CommandText = DeleteBooking;
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