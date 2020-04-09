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
    /// Interaction logic for extras.xaml
    /// </summary>
    public partial class extras : Window
    {
        
        public extras()
        {
                      
            InitializeComponent();
            var extras = this.DataContext;

        }

        
        private void comboBox_extras_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (comboBox_extras.SelectedIndex == 0 || comboBox_extras.SelectedIndex == 1)
            {
                //breakfast/dinner options selected make fields visible
                textBlock_dietReq.Visibility = Visibility.Visible;
                textBox_dietReq.Visibility = Visibility.Visible;

                //hide hire options if they were selected before and delete their values;
                if (textBlock_startDate.Visibility == Visibility.Visible)
                {
                    textBlock_startDate.Visibility = Visibility.Hidden;
                    textBox_startDate.Visibility = Visibility.Hidden;
                    textBox_startDate.Text = string.Empty;

                    textBlock_endDate.Visibility = Visibility.Hidden;
                    textBox_endDate.Visibility = Visibility.Hidden;
                    textBox_endDate.Text = string.Empty;

                    textBlock_driverName.Visibility = Visibility.Hidden;
                    textBox_driverName.Visibility = Visibility.Hidden;
                    textBox_driverName.Text = string.Empty;
                }
            }
            else if (comboBox_extras.SelectedIndex == 2)
            {
                //car hire selected, make car hire options visible
                textBlock_startDate.Visibility = Visibility.Visible;
                textBox_startDate.Visibility = Visibility.Visible;

                textBlock_endDate.Visibility = Visibility.Visible;
                textBox_endDate.Visibility = Visibility.Visible;

                textBlock_driverName.Visibility = Visibility.Visible;
                textBox_driverName.Visibility = Visibility.Visible;

                //make breakfast/dinner fields hidden and delete their values
                if (textBlock_dietReq.Visibility == Visibility.Visible)
                {
                    textBlock_dietReq.Visibility = Visibility.Hidden;
                    textBox_dietReq.Visibility = Visibility.Hidden;
                    textBox_dietReq.Text = string.Empty;
                }
            }
                
        }

        private void button_addExtra_Click(object sender, RoutedEventArgs e)
        {

            Close();

        }
    }
}
