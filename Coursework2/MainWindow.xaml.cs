using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Autofac;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using Coursework2.Architecture.Interfaces;
using Coursework2.Architecture;
namespace Coursework2
{
    public partial class MainWindow : Window
    {
        // ReservationSystem data = new ReservationSystem
        IDataBase _db;
        IReservationSystem _system;

        public MainWindow()
        {
            //Autofac IoC container initialization
            var container = ContainerConfig.Configure();

            using (var scope = container.BeginLifetimeScope())
            {
                _db = scope.Resolve<IDataBase>();
                _system = scope.Resolve<IReservationSystem>();
            }
            InitializeComponent();
        }

        //Customer Functions
        private void button_AddCustomer_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                _system.AddCustomer(textBox_Customer_Name.Text, textBox_Customer_Address.Text);
            }
            catch(ArgumentException error)
            {
                MessageBox.Show(error.Message.ToString());
            }
            UpdateGridData();
        }
        private void button_DeleteCustomer_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                _system.DeleteCustomer(textBox_Customer_Name.Text, textBox_Customer_Address.Text);
            }
            catch(ArgumentException error)
            {
                MessageBox.Show(error.Message.ToString());
            }
            UpdateGridData();
        }

        //BOOKING BUTTONS
        private void button_AddBooking_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                _system.AddBooking(textBox_booking_CustName.Text, textBox_booking_CustAddress1.Text, textBox_Booking_Arrival.Text, textBox_Booking_Departure.Text);
                UpdateGridData();
            }
            catch (ArgumentException error)
            {
                MessageBox.Show(error.Message.ToString());
            }
            UpdateGridData();
        }
        private void button_ClearBooking_Click(object sender, RoutedEventArgs e)
        {
            textBox_booking_CustName.Clear();
            textBox_booking_Reference.Clear();
            textBox_booking_CustAddress1.Clear();
            textBox_Booking_Arrival.Clear();
            textBox_Booking_Departure.Clear();
        }
        private void button_DeleteBooking_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                _system.DeleteBooking(textBox_booking_Reference.Text);
                UpdateGridData();
            }
            catch (ArgumentException error)
            {
                MessageBox.Show(error.Message.ToString());
            }
            UpdateGridData();
        }

        private void button_LoadBooking_Click(object sender, RoutedEventArgs e)
        {
            //get booking from the handler

            Booking tempBooking = null; 
            try
            {
                tempBooking = _system.GetBooking(textBox_booking_Reference.Text);
            }
            catch (ArgumentException error)
            {
                MessageBox.Show(error.Message);
            }

            if (tempBooking != null)   //booking exists
            { 
                //get booking
                textBox_Booking_Arrival.Text = tempBooking.ArrivalDate.ToString();
                textBox_Booking_Departure.Text = tempBooking.DapartureDate.ToString();

                //get customer who connects to the booking
                textBox_booking_CustName.Text = tempBooking.GetCustomer.Name;
                textBox_booking_CustAddress1.Text = tempBooking.GetCustomer.Address;
            }
            UpdateGridData();
        }

        private void Griddata_Customers_SelectedCellsChanged(object sender, SelectedCellsChangedEventArgs e)
        { 
            foreach (var item in e.AddedCells)
            {
                var col = item.Column as DataGridColumn;
                var fc = col.GetCellContent(item.Item);

                if(fc is TextBlock)
                {
                    //populate customer textboxes based on selection
                    var custList= _system.ListCustomer();
                    var custInfo = custList.Find((x => x.Name == (fc as TextBlock).Text));

                    textBox_Customer_Name.Text = custInfo.Name;
                    textBox_Customer_Address.Text = custInfo.Address;
                }

                if (fc is TextBlock)
                {
                    //populate the booking datagrid based on selection
                    dataGrid_Booking.ItemsSource = _system.ListBookings((fc as TextBlock).Text);
                    break;
                }
            }
            UpdateGridData();
        }

        private void button_AmendBooking_Click(object sender, RoutedEventArgs e)
        { 
            
            try
            {
                _system.AmendBooking(textBox_booking_Reference.Text, textBox_Booking_Arrival.Text, textBox_Booking_Departure.Text);
            }
            catch (ArgumentException error)
            {
                MessageBox.Show(error.Message.ToString());
            }
            UpdateGridData();
        }

        private void button_AddGuest_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                _system.AddGuest(textBox_guestName.Text, textBox_guestPassport.Text,
                            textBox_guestAge.Text, textBox_guestRef.Text);
                UpdateGridData();
            }
            catch(ArgumentException error)
            {
                MessageBox.Show(error.Message);
            }
            UpdateGridData();
        }

        private void DataGrid_Booking_SelectedCellsChanged(object sender, SelectedCellsChangedEventArgs e)
        {
            Booking temp;
            foreach (var item in e.AddedCells)
            {
                var col = item.Column as DataGridColumn;
                var fc = col.GetCellContent(item.Item);

                temp = _system.GetBooking((fc as TextBlock).Text);  //get booking based on row.

                //populate booking textboxes with the selected data from the grid
                textBox_booking_CustName.Text = temp.GetCustomer.Name;
                textBox_booking_CustAddress1.Text = temp.GetCustomer.Address;
                textBox_booking_Reference.Text = temp.ReferenceNumber.ToString();
                textBox_Booking_Arrival.Text = temp.ArrivalDate.ToString();
                textBox_Booking_Departure.Text = temp.DapartureDate.ToString();
                
                var guests = _system.ListGuests(temp.ReferenceNumber.ToString());  //get guest list based on reference number

                dataGrid_Guests.ItemsSource = guests; //populate the Guests table with the infomation.
                dataGrid_Guests.Items.Refresh();

                break;
            }
        }

        private void button_DeleteGuest_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                _system.DeleteGuest(textBox_guestPassport.Text);
            }
            catch(ArgumentException error)
            {
                MessageBox.Show(error.Message);
            }
            UpdateGridData();
        }

        private void button_Extras_Click(object sender, RoutedEventArgs e)
        {
            Booking booking = null;
            try
            {
                booking = _system.GetBooking(textBox_booking_Reference.Text);
            }
            catch(ArgumentException error)
            {
                MessageBox.Show(error.Message);
            }

            //check if booking exists
            if (booking != null)
            {
                //create and run the extras window
                extras extras = new extras();
                extras.ShowDialog();

                //Work with data provided by the new window.
                if (extras.comboBox_extras.SelectedIndex == 0)
                {
                    _system.ExtraBreakfast(textBox_booking_Reference.Text, extras.textBox_dietReq.Text);
                }
                else if (extras.comboBox_extras.SelectedIndex == 1)
                {
                    _system.ExtraEveningMeal(textBox_booking_Reference.Text, extras.textBox_dietReq.Text);
                }
                else if (extras.comboBox_extras.SelectedIndex == 2)
                {
                    try
                    {
                        _system.ExtraCarHire(textBox_booking_Reference.Text, extras.textBox_startDate.Text, extras.textBox_endDate.Text, extras.textBox_driverName.Text);
                    }
                    catch (ArgumentException error)
                    {
                        MessageBox.Show(error.Message);
                    }
                }
                else MessageBox.Show("No extra option selected.");
            }
        }

        private void button_BookingInvoice_Click(object sender, RoutedEventArgs e)
        {
            Booking booking = null;
            try
            {
                booking = _system.GetBooking(textBox_booking_Reference.Text);
            }
            catch (ArgumentException error)
            {
                MessageBox.Show(error.Message);
            }
            //check if booking exists
            if (booking != null)
            {
                invoice window = new invoice(_system.ShowPrice(textBox_booking_Reference.Text));
                window.ShowDialog();
            }
            else MessageBox.Show("Invalid booking number");
        }

        private void New_Database_Click(object sender, RoutedEventArgs e)
        {
            //Creates the new database.
            bool x = _db.SetUp();
            if(!x)
            {
                MessageBox.Show("System Already connected to a database");
            }
            else
            {
                MessageBox.Show("Booking Database successfully created");
            }
        }

        private void Delete_Database_Click(object sender, RoutedEventArgs e)
        { 
            bool x = _db.DeleteTable();
            if (!x)
            {
                MessageBox.Show("Error Cant delete database ");
            }
            else
            {
                MessageBox.Show("Booking Database successfully deleted");
            }
            UpdateGridData();
        }

        private void Add_Local_Database_Click(object sender, RoutedEventArgs e)
        {

            //To instert local data into the database, simply pass through the list of customers.
            _db.InsertLocalData(_system.ListCustomer());
            MessageBox.Show("Information added,  duplicates ommited");
        }

        private void Load_Database_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                Customer temp = _db.LoadData();
                _system.AddCustomer(temp.Name, temp.Address);
                MessageBox.Show("Added the data to the database");
                UpdateGridAll();
            }
            catch
            {
                MessageBox.Show("Error: Can't Load Customer from external database");
                UpdateGridAll();
            }
        }

        private void UpdateGridData()
        {
            dataGrid_Customer.Items.Refresh();

            dataGrid_Booking.Items.Refresh();

            dataGrid_Guests.Items.Refresh();
        }

        private void UpdateGridAll()
        {
            dataGrid_Customer.ItemsSource = _system.ListCustomer();
            dataGrid_Booking.ItemsSource = _system.GetBooking();
            dataGrid_Guests.ItemsSource = _system.ListGuests();
        }
        private void AddMockData()
        {
            try
            {
                _system.AddBooking("Josh Pinklet", "11 Watergate Place", "22/01/2020", "25/01/2020");
                _system.AddBooking("Alfred Hitchhike", "Bane's Drive", "13/12/2021", "20/12/2021");
                _system.AddBooking("Hugh Hueston", "Bulding 2 Moonplace", "12/11/2020", "25/11/2020");

                _system.AddCustomer("Kepler Mopler", "13 Funkytown Groove, Iceland");
                _system.AddGuest("Mario", "ISAME55", "32", "1");
                _system.AddGuest("Luigi", "AMA499", "33", "3");
                _system.AddGuest("Peach, Princess", "PIKA23", "46", "3");
                _system.AddGuest("Testoviron", "KHWA400", "92", "2");
            }
            catch(ArgumentException error)
            {
                MessageBox.Show(error.Message);
            }
        }
        private void button_InjectData_Click(object sender, RoutedEventArgs e)
        {
            AddMockData();
        }
        private void button_showAllData_Click(object sender, RoutedEventArgs e)
        {
            UpdateGridAll();
        }

        private void dataGrid_Guests_SelectedCellsChanged_1(object sender, SelectedCellsChangedEventArgs e)
        { 
            foreach (var item in e.AddedCells)
            {
                var col = item.Column as DataGridColumn;
                var fc = col.GetCellContent(item.Item);

                if(fc is TextBlock)
                {
                    var temp = _system.ListGuests().Find((x => x.Name == (fc as TextBlock).Text));

                    //populate guest textboxes with the selected data from the grid
                    textBox_guestName.Text = temp.Name;
                    textBox_guestPassport.Text = temp.PassportNo;
                    textBox_guestAge.Text = temp.Age.ToString();
                    textBox_guestRef.Text = temp.BookingReferenceNumber.ToString();

                }

                break;
            }
        }
    }
}

