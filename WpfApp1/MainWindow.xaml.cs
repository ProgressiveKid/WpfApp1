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
using System.Windows.Navigation;
using System.Windows.Shapes;
using Newtonsoft.Json;



namespace WpfApp1
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            ReaderFromJson readerFromJson = new ReaderFromJson();
           List <User> listPeople = readerFromJson.ReadFromJsonFile(); // Синициализировали JSON файл в List <User>
            TextBox1.Text = "Your effort will pay off";          
            foreach (var lineTable in listPeople)
            {
                string a = "asdasd";

                //DataGridView



            }
            MessageBox.Show(Convert.ToString(readerFromJson.aaa));
        }
        
        private void DataGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            //DataGridView.Columns.Add("newColumnName", "Column Name in Text");
        }

        private void DataGridView_Loaded(object sender, RoutedEventArgs e)
        {
            ReaderFromJson readerFromJson = new ReaderFromJson();
            List<User> listPeople = readerFromJson.ReadFromJsonFile();

            DataGridView.ItemsSource = listPeople;
        }
    }
}
