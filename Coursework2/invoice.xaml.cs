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
    /// <summary>
    /// Interaction logic for invoice.xaml
    /// </summary>
    public partial class invoice : Window
    {
        public invoice()
        {
            InitializeComponent();
        }
        public invoice(List<int> totalprices) :this()
        {
            //totalprices = { guestsPerNight, extraBF, extraM, extraCH, noOfNights };
            try
            {

                textBox_guestPerNight.Text = "£ " + totalprices[0];
                textBox_guestTotal.Text = "£ " + totalprices[0] * totalprices[4];
                textBox_breakfast.Text = "£ " + totalprices[1];
                textBox_Meal.Text = "£ " + totalprices[2];
                textBox_carhire.Text = "£ " + totalprices[3];

                textbox_total.Text = "£ " + (   totalprices[0] * totalprices[4]
                                                + totalprices[1]
                                                + totalprices[2]
                                                + totalprices[3]    );
            }
            
            catch
            {

            }
        }
    }
}
