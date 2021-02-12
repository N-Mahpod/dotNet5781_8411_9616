﻿using System;
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
using BLL.BLL_Api;
using BLL.BLL_Object;


namespace PL_Gui
{
    /// <summary>
    /// Interaction logic for AddLine_Window.xaml
    /// </summary>
    public partial class AddLine_Window : Window
    {
        IBLL bl;
        public AddLine_Window(IBLL _bl)
        {
            InitializeComponent();
            bl = _bl;
            cbArea.ItemsSource = Enum.GetValues(typeof(BLL.BLL_Object.Area));
            cbArea.SelectedIndex = 0;
        }

        private void CreateButton_Click(object sender, RoutedEventArgs e)
        {
            int nk;
            bool f = int.TryParse(tbKey.Text, out nk);
            if (!f)
            {
                MessageBox.Show("invalid bus line key");
                return;
            }
            Area na = (Area)Enum.Parse(typeof(BLL.BLL_Object.Area), cbArea.SelectedItem.ToString());
            if(na == Area.Error)
            {
                MessageBox.Show("invalid bus line area");
                return;
            }
            try
            {
                bl.CreatBusLine(nk, na);
            }
            catch (AlreadyExistExeption er)
            {
                MessageBox.Show(er.Message);
                return;
            }

            this.Close();
        }
    }
}
