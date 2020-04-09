using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Coursework2
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {

        // ReservationSystem data = new ReservationSystem();
        ReservationSystem data;
        DataBase db;

        public MainWindow()
        {
            InitializeComponent();
            data = new ReservationSystem();
            Griddata.ItemsSource = data.ListCustomer();
            //data.ShowPrice();

           
           // db = new DataBase();
        }

        //Customer Functions
        private void button_addCust_Click(object sender, RoutedEventArgs e)
        {
            
            data.AddCustomer(textBox_custfNameset.Text, textBox_1custaddress.Text);
            Griddata.Items.Refresh();
        }
        private void button_delCust_Click(object sender, RoutedEventArgs e)
        {
            data.DeleteCustomer(textBox_custfNameset.Text);
            Griddata.Items.Refresh();
        }

        //BOOKING BUTTONS
        private void button_bookingAdd_Click(object sender, RoutedEventArgs e)
        {
            data.AddBooking(textBox_booking_CustName.Text, textBox_booking_CustAddress1.Text, textBox_Booking_Arrival.Text, textBox_Booking_Departure.Text);
            Griddata.Items.Refresh();
            dataGrid_Booking.Items.Refresh();
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

            data.DeleteBooking(textBox_booking_Reference.Text);

            Griddata.Items.Refresh();
            dataGrid_Booking.Items.Refresh();
            dataGrid_Guests.Items.Refresh();

        }

        private void button_BookingLoad_Click(object sender, RoutedEventArgs e)
        {
            if (textBox_booking_Reference.Text != "")
            {

                Booking bookTemp = data.GetBooking(textBox_booking_Reference.Text);
                if (bookTemp != null)
                {
                    textBox_Booking_Arrival.Text = bookTemp.ArrivalDate.ToString();
                    textBox_Booking_Departure.Text = bookTemp.DapartureDate.ToString();
                }
                //else MessageBox.Show("Booking doesn't exist");
                Customer custTemp;
                foreach (Customer cust in data.ListCustomer())
                {
                    if (cust.ExistsBooking(bookTemp))
                    {
                        custTemp = cust;
                        textBox_booking_CustName.Text = custTemp.Name;
                        textBox_booking_CustAddress1.Text = custTemp.Address;
                        return;
                    }

                }
                MessageBox.Show("Invalid booking reference number");
            }
            else MessageBox.Show("Please provide Booking Reference Number");
        }

        private void Griddata_SelectedCellsChanged(object sender, SelectedCellsChangedEventArgs e)
        {

            foreach (var item in e.AddedCells)
            {
                var col = item.Column as DataGridColumn;
                var fc = col.GetCellContent(item.Item);

                if (fc is TextBlock)
                {
                    dataGrid_Booking.ItemsSource = data.ListBookings((fc as TextBlock).Text);
                    dataGrid_Booking.Items.Refresh();

                    break;
                }

            }
            dataGrid_Booking.Items.Refresh();


        }

        private void button_bookingAmend_Click(object sender, RoutedEventArgs e)
        {
            data.AmendBooking(textBox_booking_Reference.Text, textBox_Booking_Arrival.Text, textBox_Booking_Departure.Text);
            dataGrid_Booking.Items.Refresh();
            Griddata.Items.Refresh();
            dataGrid_Guests.Items.Refresh();

        }

        private void button_amendCust_Click(object sender, RoutedEventArgs e)
        {
            data.AmendCustomer(textBox_custRef.Text, textBox_custfNameset.Text, textBox_1custaddress.Text);
            dataGrid_Booking.Items.Refresh();
            Griddata.Items.Refresh();
        }

        private void button_addGuest_Click(object sender, RoutedEventArgs e)
        {
            data.AddGuest(textBox_guestName.Text, textBox_guestPassport.Text,
                          textBox_guestAge.Text, textBox_guestRef.Text);

            dataGrid_Guests.Items.Refresh();

        }

        private void dataGrid_Booking_SelectedCellsChanged(object sender, SelectedCellsChangedEventArgs e)
        {
            Booking temp;
            foreach (var item in e.AddedCells)
            {
                var col = item.Column as DataGridColumn;
                var fc = col.GetCellContent(item.Item);

                temp = data.GetBooking((fc as TextBlock).Text);
                dataGrid_Guests.ItemsSource = temp.ListGuests();
                dataGrid_Guests.Items.Refresh();

                break;
            }
        }

        private void button_delGuest_Click(object sender, RoutedEventArgs e)
        {
            data.DeleteGuest(textBox_guestPassport.Text, textBox_guestRef.Text);
            dataGrid_Guests.Items.Refresh();

        }

        private void button_extras_Click(object sender, RoutedEventArgs e)
        {
            //check if booking reference exists
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
            else MessageBox.Show("Please provide a booking reference number");
        }

        private void button_amendGuest_Click(object sender, RoutedEventArgs e)
        {
            //reference check
            AmendGuest window = new AmendGuest();
            window.ShowDialog();

            data.AmendGuest(textBox_guestRef.Text, window.textbox_oldPassport.Text, window.textbox_newName.Text, window.textbox_newPassportNo.Text, window.textbox_newAge.Text);
        }

        private void button_bookingInvoice_Click(object sender, RoutedEventArgs e)
        {
            if (data.BookingExists(textBox_booking_Reference.Text))
            {
                invoice window = new invoice(data.ShowPrice(textBox_booking_Reference.Text));

                window.ShowDialog();
            }
            else MessageBox.Show("Invalid booking number");

        }

        
    }
}

