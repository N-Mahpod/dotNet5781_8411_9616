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
using System.Collections.ObjectModel;
using BLL.BLL_Api;
using BLL.BLL_Object;

namespace PL_Gui
{
    /// <summary>
    /// Interaction logic for AdminWindow.xaml
    /// </summary>
    public partial class AdminWindow : Window
    {
        IBLL bl;
        ObservableCollection<BLL.BLL_Object.Bus> ObserListOfBuses = new ObservableCollection<BLL.BLL_Object.Bus>();

        public AdminWindow(IBLL _bl)
        {
            InitializeComponent();
            
            bl = _bl;

            foreach (var item in bl.GetAllBuses())
            {
                ObserListOfBuses.Add(item);
            }

            lvBuses.ItemsSource = ObserListOfBuses;
        }

        private void DriveButton_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("The function doesn't exist yet:(");
        }

        private void RefuelButton_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("The function doesn't exist yet:(");
        }
    }
}
