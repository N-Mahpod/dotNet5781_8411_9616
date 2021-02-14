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
using System.Windows.Navigation;
using System.Windows.Shapes;
using BLL.BLL_Api;
using BLL.BLL_Object;

namespace PL_Gui
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        IBLL bl = BLL_Factory.GetBL();
        bool admin;

        public MainWindow()
        {
            InitializeComponent();
            bl.CreateWorld();//for xml
/*#if DEBUG
            admin = bl.IsAdmin("bob", "123");
            AdminWindow adwin = new AdminWindow(bl);
            adwin.Show();
            this.Close();
#endif*/
        }

        private void btnLogin_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                admin = bl.IsAdmin(tbUserName.Text, tbPassword.Password);
            }
            catch (IncorrectSomethingExeption)
            {
                MessageBox.Show("user name = bob, password = 123");
                return;
            }
            if (admin)
            {
                AdminWindow adwin = new AdminWindow(bl);
                adwin.Show();
                this.Close();
            }
        }

        private void btnSignup_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("The function doesn't exist yet:(");
        }
    }
}
