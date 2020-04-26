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
        private void button_addCust_Click(object sender, RoutedEventArgs e)
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
        private void button_delCust_Click(object sender, RoutedEventArgs e)
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
        private void button_bookingAdd_Click(object sender, RoutedEventArgs e)
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
        private void button_bookingClear_Click(object sender, RoutedEventArgs e)
        {
            textBox_booking_CustName.Clear();
            textBox_booking_Reference.Clear();
            textBox_booking_CustAddress1.Clear();
            textBox_Booking_Arrival.Clear();
            textBox_Booking_Departure.Clear();
        }
        private void button_bookingDelete_Click(object sender, RoutedEventArgs e)
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

        private void button_BookingLoad_Click(object sender, RoutedEventArgs e)
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

        private void Griddata_SelectedCellsChanged(object sender, SelectedCellsChangedEventArgs e)
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

        private void button_bookingAmend_Click(object sender, RoutedEventArgs e)
        { 
            _system.AmendBooking(textBox_booking_Reference.Text, textBox_Booking_Arrival.Text, textBox_Booking_Departure.Text);
            UpdateGridData();

        }

        private void button_amendCust_Click(object sender, RoutedEventArgs e)
        {
        
        }

        private void button_addGuest_Click(object sender, RoutedEventArgs e)
        {
            _system.AddGuest(textBox_guestName.Text, textBox_guestPassport.Text, 
                            textBox_guestAge.Text, textBox_guestRef.Text);

            dataGrid_Guests.Items.Refresh();

        }

        private void dataGrid_Booking_SelectedCellsChanged(object sender, SelectedCellsChangedEventArgs e)
        {
            /*Booking temp;
            foreach (var item in e.AddedCells)
            {
                var col = item.Column as DataGridColumn;
                var fc = col.GetCellContent(item.Item);

                temp = data.GetBooking((fc as TextBlock).Text);
                dataGrid_Guests.ItemsSource = temp.ListGuests();
                dataGrid_Guests.Items.Refresh();

                break;
            }*/
        }

        private void button_delGuest_Click(object sender, RoutedEventArgs e)
        {
           /* data.DeleteGuest(textBox_guestPassport.Text, textBox_guestRef.Text);
            dataGrid_Guests.Items.Refresh();
*/
        }

        private void button_extras_Click(object sender, RoutedEventArgs e)
        {
            /*//check if booking reference exists
            if (data.BookingExists(textBox_booking_Reference.Text))
            {
                //create and run the extras window
                extras test = new extras();
                test.ShowDialog();

                //Work with data provided by it.
                //if breakfast was selected
                if (test.comboBox_extras.SelectedIndex == 0)
                {

                    data.ExtraBreakfast(textBox_booking_Reference.Text, test.textBox_dietReq.Text);
                }
                else if (test.comboBox_extras.SelectedIndex == 1)
                {
                    data.ExtraEveningMeal(textBox_booking_Reference.Text, test.textBox_dietReq.Text);
                }
                else if (test.comboBox_extras.SelectedIndex == 2)
                {
                    try
                    {
                        data.ExtraCarHire(textBox_booking_Reference.Text, test.textBox_startDate.Text, test.textBox_endDate.Text, test.textBox_driverName.Text);
                    }
                    catch
                    {
                        MessageBox.Show("Error on mainwindow level.");
                       
                    }
                }

                else MessageBox.Show("No extra option selected.");
                
            }
            else MessageBox.Show("Please provide a booking reference number");*/
        }

        private void button_amendGuest_Click(object sender, RoutedEventArgs e)
        {
           /* //reference check
            AmendGuest window = new AmendGuest();
            window.ShowDialog();

            data.AmendGuest(textBox_guestRef.Text, window.textbox_oldPassport.Text, window.textbox_newName.Text, window.textbox_newPassportNo.Text, window.textbox_newAge.Text);*/
        }

        private void button_bookingInvoice_Click(object sender, RoutedEventArgs e)
        {
            /*if (_system.BookingExists(textBox_booking_Reference.Text))
            {
                invoice window = new invoice(data.ShowPrice(textBox_booking_Reference.Text));

                window.ShowDialog();
            }
            else MessageBox.Show("Invalid booking number");*/

        }

        /// <summary>
        /// Databse functionality. 
        /// </summary>
   
        private void New_db_Click(object sender, RoutedEventArgs e)
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

        private void Delete_db_Click(object sender, RoutedEventArgs e)
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

        private void Add_Local_db_Click(object sender, RoutedEventArgs e)
        {

            //To instert local data into the database, simply pass through the list of customers.
            _db.InsertLocalData(_system.ListCustomer());
            MessageBox.Show("Information added,  duplicates ommited");
        }

        private void Load_db_Click(object sender, RoutedEventArgs e)
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

