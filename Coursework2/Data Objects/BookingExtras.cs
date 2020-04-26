using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Coursework2.Data_Objects
{
    public class BookingExtras
    {
        protected string customerComment;
        
        public string CustomerComment
        {
            get { return customerComment; }
            set { customerComment = value; }
        }
    }

    class ExtraEveningMeal : BookingExtras
    {

        public ExtraEveningMeal(string comment)
        {
            customerComment = comment;
        }
    }

    class ExtraBreakfast : BookingExtras
    {

        public ExtraBreakfast(string comment)
        {
            customerComment = comment;
        }
    }

    class ExtraCarHire : BookingExtras
    {
        string driverName;
        DateTime startDate;
        DateTime endDate;

        public ExtraCarHire(string name, DateTime start, DateTime end)
        {
            //validity of the name and dates are checked by the booking class
            driverName = name;
            //pass through public accessor to catch the error
            StartDate = start;
            EndDate = end;
        }

        public string DriverName
        {
            get { return driverName; }
            set { driverName = value; }
        }

        public DateTime StartDate
        {
            get { return startDate; }
            set
            {
                try
                {
                    startDate = value;
                }
                catch
                {
                    throw new ArgumentException("Invalid date format");
                }
            }
        }

        public DateTime EndDate
        {
            get { return endDate; }
            set
            {
                try
                {
                    endDate = value;
                }
                catch
                {
                    throw new ArgumentException("Invalid date format");
                }
            }
        }
    }
}

