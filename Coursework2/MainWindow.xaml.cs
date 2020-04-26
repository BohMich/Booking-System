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
            Griddata.ItemsSource = _system.ListCustomer(); 
        }

        //Customer Functions
        private void button_AddCustomer_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                _system.AddCustomer(textBox_custfNameset.Text, textBox_1custaddress.Text);
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
                _system.DeleteCustomer(textBox_custfNameset.Text, textBox_1custaddress.Text);
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
            Booking tempBooking = _system.GetBooking(textBox_booking_Reference.Text);

            if (tempBooking != null)   //booking exists
            { 
                //get booking
                textBox_Booking_Arrival.Text = tempBooking.ArrivalDate.ToString();
                textBox_Booking_Departure.Text = tempBooking.DapartureDate.ToString();

                //get customer who connects to the booking
                textBox_booking_CustName.Text = tempBooking.GetCustomer.Name;
                textBox_booking_CustAddress1.Text = tempBooking.GetCustomer.Address;
            }
            else
            {
                MessageBox.Show("Booking doesn't exist.");
            }
            UpdateGridData();
        }

        private void Griddata_Customers_SelectedCellsChanged(object sender, SelectedCellsChangedEventArgs e)
        { 
            foreach (var item in e.AddedCells)
            {
                var col = item.Column as DataGridColumn;
                var fc = col.GetCellContent(item.Item);

                if (fc is TextBlock)
                {
                    dataGrid_Booking.ItemsSource = _system.ListBookings((fc as TextBlock).Text);
                    UpdateGridData();

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
            }
            catch(ArgumentException error)
            {
                MessageBox.Show(error.Message.ToString());
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

                var guests = _system.ListGuests(temp.ReferenceNumber);  //get guest list based on reference number

                dataGrid_Guests.ItemsSource = guests; //populate the Guests table with the infomation.
                dataGrid_Guests.Items.Refresh();

                break;
            }
        }

        private void button_DeleteGuest_Click(object sender, RoutedEventArgs e)
        {
            _system.DeleteGuest(textBox_guestPassport.Text);
            UpdateGridData();
        }

        private void button_Extras_Click(object sender, RoutedEventArgs e)
        {
            var booking = _system.GetBooking(textBox_booking_Reference.Text);

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
                        MessageBox.Show(error.Message.ToString());
                    }
                }
                else MessageBox.Show("No extra option selected.");
            }
            else MessageBox.Show("Please provide a valid booking reference number.");
        }

        private void button_BookingInvoice_Click(object sender, RoutedEventArgs e)
        {
            var booking = _system.GetBooking(textBox_booking_Reference.Text);
            //check if booking exists
            if (booking != null)
            {
                invoice window = new invoice(_system.ShowPrice(textBox_booking_Reference.Text));
                window.ShowDialog();
            }
            else MessageBox.Show("Invalid booking number");
        }

        /// <summary>
        /// Databse functionality. 
        /// </summary>
   
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
                UpdateGridData();
            }
            catch
            {
                MessageBox.Show("Error: Can't Load Customer from external database");
                UpdateGridData();
            }
        }

        private void UpdateGridData()
        {
            dataGrid_Booking.Items.Refresh();
            Griddata.Items.Refresh();
            dataGrid_Guests.Items.Refresh();
        }
    }
}

