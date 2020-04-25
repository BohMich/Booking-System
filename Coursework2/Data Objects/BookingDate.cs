
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Coursework2
{
    class BookingDate
    {
        DateTime startDate;
        DateTime endDate;
        string name;
        public BookingDate(DateTime startDate, DateTime endDate, string name)
        {
            this.startDate = startDate;
            this.endDate = endDate;
            this.name = name;
        }

        public DateTime StartDate
        {
            get { return startDate; }
        }

        public DateTime EndDate
        {
            get { return endDate; }
        }

        public string Name
        {
            get { return name; }
        }
    }
}