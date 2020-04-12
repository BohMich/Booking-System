# Booking-System

![UML Diagram](https://raw.githubusercontent.com/BohMich/Booking-System/master/Project%20Model.png)

Booking system written in C# using WPF forms in .NET framework. 

The project started as a university coursework, and was further developed in my free time. 

ReservationSystem uses the facade design pattern to isolate the system from the WPF designer/programmer. 
It uses the singleton design pattern to prevent the client from saving booking data to more than one booking system.
It contains all neccessary methods to create/edit/delete all aspects of the booking system structure. 
It is doing so without becoming a god object. All interaction with the data is done through accessing the information using 
the customer instances that handle customer details internally. No global access is given to either customer/booking or guest objects. 

# SQLite functionality

```sql
CREATE TABLE Customer(
Name TEXT PRIMARY KEY
Address TEXT NOT NULL)

CREATE TABLE Guest( 
Age INT,
Name TEXT,
BookingNo INTEGER references Booking(BookingNo),
PassportNo INTEGER PRIMARY KEY)

CREATE TABLE Booking(
BookingNo INTEGER PRIMARY KEY AUTOINCREMENT,
CustomerRefNo TEXT references Customer(Name), 
ArrivalDate TEXT NOT NULL, 
DepartureDate TEXT NOT NULL)
```

The system was recently updated with a SQLite functionality. 
The booking system can connect to an external .db file. 
It uses the ADO.NET library to implement the SQLite functionality. 
The database structures mimics that of the UML design with the exception of the guest having a foreign key to the booking, 
and the extras not being implemented yet. 

# Possible Improvements. 

-Implement more dynamic and responsive UI. 
  1. Fix pop-up messages that appear when data is loaded from SQL 
  2. Dynamically fill in the customer details when interacting with table of customers.
  
-Further develop the SQL connection. Allow for connecting to external databases. 
-Change the structure of the “booking options”. As of right now they are three separate singleton objects that can be added to the booking. Instead, there should be one interface class.



