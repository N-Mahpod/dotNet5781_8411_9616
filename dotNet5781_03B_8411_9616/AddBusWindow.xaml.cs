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

namespace dotNet5781_03B_8411_9616
{
    /// <summary>
    /// Interaction logic for AddBusWindow.xaml
    /// </summary>
    public partial class AddBusWindow : Window
    {
        public AddBusWindow()
        {
            InitializeComponent();
        }

        private void SubmitBusButton_Click(object sender, RoutedEventArgs e)
        {
            //SubmitBusButton.Content = "Done";

            int d, m, y;
            bool success = true;

            success &= Int32.TryParse(tbDay.Text, out d);
            success &= Int32.TryParse(tbMonth.Text, out m);
            success &= Int32.TryParse(tbYear.Text, out y);

            if(!success)
            {
                tbError.Text = "Error: Invalid date input!. try again.";
                return;
            }

            DateTime start = new DateTime(d, m, y);

            ///
            //Needs test against the simulation clock!
            ///


        }
    }
}
