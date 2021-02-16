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
using System.Threading;

namespace PL_Gui
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        IBLL bl = BLL_Factory.GetBL();
        bool admin;
        bool login = true;

        public MainWindow()
        {
            InitializeComponent();
            bl.CreateWorld();//for xml
            gSecretPass.Visibility = Visibility.Hidden;
            this.Title = "Pumbuses - Log in";
        }

        private void btnLogin_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                admin = bl.IsAdmin(tbUserName.Text, tbPassword.Password);
            }
            catch (IncorrectSomethingExeption)
            {
                MessageBox.Show("user name = bob, password = 123. or sign up.");
                return;
            }
            if (admin)
            {
                Thread.Sleep(TimeSpan.FromSeconds(0.5));
                AdminWindow adwin = new AdminWindow(bl);
                adwin.Show();
                this.Close();
            }
            else
            {
                MessageBox.Show("you are not an admin!");
            }
        }

        private void btnSignup_Click(object sender, RoutedEventArgs e)
        {
            if (login)
            {
                login = false;
                this.Title = "Pumbuses - Sign up";

                gSecretPass.Visibility = Visibility.Visible;
                btnLogin.Visibility = Visibility.Hidden;
                tbPassword.Clear();
                tbUserName.Clear();
            }
            else
            {
                login = true;
                this.Title = "Pumbuses - Log in";

                gSecretPass.Visibility = Visibility.Hidden;
                btnLogin.Visibility = Visibility.Visible;
                
                try
                {
                    bl.SignUp(tbUserName.Text, tbPassword.Password, pbSecretPass.Password);
                } 
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                    MessageBox.Show("the secret password is 'ADMIN(-;'");
                    tbPassword.Clear();
                    tbUserName.Clear();
                }
            }
        }
    }
}
