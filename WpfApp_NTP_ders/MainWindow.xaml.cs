using Microsoft.EntityFrameworkCore;
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
using WpfApp_NTP_ders.Views;

namespace WpfApp_NTP_ders
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
       
        public MainWindow()
        {
            InitializeComponent();
           
        }

        private void Button_ogrenciler_Click(object sender, RoutedEventArgs e)
        {
            OgrencilerPage ogrPage=new OgrencilerPage();
            frame_islemler.Navigate(ogrPage);
        }

        private void Button_okullar_Click(object sender, RoutedEventArgs e)
        {
            OkullarPage okulPage = new OkullarPage();
            frame_islemler.Navigate(okulPage);
        }

        private void Button_siniflar_Click(object sender, RoutedEventArgs e)
        {
            SiniflarPage sinifPage = new SiniflarPage();
            frame_islemler.Navigate(sinifPage);
        }
    }
}
