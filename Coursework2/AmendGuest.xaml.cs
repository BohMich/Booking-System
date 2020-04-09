using System;
using System.Collections.Generic;
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
using System.Windows.Shapes;

namespace Coursework2
{
    public partial class AmendGuest : Window
    {
        /// <summary>
        /// AmendGuest only acts as a switch. Passes data betwen windows.
        /// </summary>
        public AmendGuest()
        {
            InitializeComponent();
            var amendGuest = DataContext;
        }

        public static bool isAmend = false;

        private void button_amend_Click(object sender, RoutedEventArgs e)
        {
            //Data entered correctly. Leave the pop-up window and execute the code.
            isAmend = true;
            Close();
            
        }

        private void button_cancel_Click(object sender, RoutedEventArgs e)
        {
            //close the pop-up window. No amendment is made
            Close();
        }

    }
}
