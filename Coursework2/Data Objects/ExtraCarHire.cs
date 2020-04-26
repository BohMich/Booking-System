using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Coursework2
{
    class ExtraCarHire
    {
        //Class used by the booking class
        //stores information about car hires.

        //name of the driver and start end dates.

        string driverName;
        DateTime startDate;
        DateTime endDate;

        public string DriverName
        {
            get { return driverName; }
            set { driverName = value; }
        }

        //ACCESSORS
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
                    MessageBox.Show("Invalid Date Format");
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
                    MessageBox.Show("Invalid Date Format");
                }
            }
        }
        public ExtraCarHire(string name, DateTime start, DateTime end )
        {
            //validity of the name and dates are checked by the booking class.
            driverName = name;
            startDate = start;
            endDate = end;
        }

    }
}
