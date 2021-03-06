using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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

namespace WpfApp_NTP_ders
{
    /// <summary>
    /// OgrencilerPage.xaml etkileşim mantığı
    /// </summary>
    public partial class OgrencilerPage : Page
    {
       
        vt2022Context veriler = new vt2022Context();
        Ogrenci secilen_ogr = new Ogrenci();
        private void yenikayit_alan_temizle()
        {
            TextBox_yeni_okulno.Text = "";
            TextBox_yeni_ad.Text = "";
            TextBox_yeni_soyad.Text = "";
            if (Datagrid_Ogrenciler.SelectedItem!=null)
                Datagrid_Ogrenciler.SelectedItem = null;
            Grid_yeni.Visibility = Visibility.Hidden;
            Button_yenikayit.IsEnabled = true;
            ComboBox_yeni_sinif.SelectedIndex = -1;  
        }
        public OgrencilerPage()
        {            
            InitializeComponent();
            Grid_Modal.Visibility = Visibility.Hidden;
            veriler.Database.EnsureCreated();
            veriler.Ogrencis.Load();
            veriler.Sinifs.Load();
            veriler.Okuls.Load();
            CheckBox_TumSinifGoster.IsChecked = true;   
            ComboBox_FilterSinif.ItemsSource = veriler.Sinifs.Local.ToObservableCollection(); 
            Datagrid_Ogrenciler.ItemsSource = veriler.Ogrencis.Local.ToObservableCollection();
            //modal kısımdaki comboboxları dolduruyorum
            ComboBox_yeni_sinif.ItemsSource = veriler.Sinifs.Local.ToObservableCollection();
            ComboBox_guncelle_sinif.ItemsSource = veriler.Sinifs.Local.ToObservableCollection();
        }

        private void Button_yenikayit_Click(object sender, RoutedEventArgs e)
        {
            Grid_Modal.Visibility = Visibility.Visible;
            Grid_yeni.Visibility = Visibility.Visible;
            Grid_guncelle.Visibility = Visibility.Hidden;
            Button_yenikayit.IsEnabled = false;            
        }

        private void Button_yeni_iptal_Click(object sender, RoutedEventArgs e)
        {            
            yenikayit_alan_temizle();
            Grid_Modal.Visibility = Visibility.Hidden;
        }

        private void Button_yeni_kaydet_Click(object sender, RoutedEventArgs e)
        {
            
            Ogrenci yeni_ogr = new Ogrenci();
            yeni_ogr.Okulno = Int32.Parse(TextBox_yeni_okulno.Text);
            yeni_ogr.Ad = TextBox_yeni_ad.Text;
            yeni_ogr.Soyad = TextBox_yeni_soyad.Text;
            yeni_ogr.Sinif = (Int32)ComboBox_yeni_sinif.SelectedValue;
            veriler.Ogrencis.Local.Add(yeni_ogr);
            veriler.SaveChanges();
            yenikayit_alan_temizle();
            Grid_Modal.Visibility = Visibility.Hidden;
        }

        private void Datagrid_Ogrenciler_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            
            if (Datagrid_Ogrenciler.SelectedItem != null)
            {
                Grid_Modal.Visibility = Visibility.Visible;
                Grid_yeni.Visibility = Visibility.Hidden;
                Grid_guncelle.Visibility = Visibility.Visible;
                secilen_ogr = (Ogrenci)Datagrid_Ogrenciler.SelectedItem;
                TextBox_guncelle_okulno.Text = secilen_ogr.Okulno.ToString();
                TextBox_guncelle_ad.Text = secilen_ogr.Ad;
                TextBox_guncelle_soyad.Text = secilen_ogr.Soyad;
                ComboBox_guncelle_sinif.SelectedItem = secilen_ogr.SinifNavigation;
                Button_yenikayit.IsEnabled = true;              
            }
        }

        private void Button_guncelle_iptal_Click(object sender, RoutedEventArgs e)
        {
            Grid_Modal.Visibility = Visibility.Hidden;
            Grid_guncelle.Visibility = Visibility.Hidden;
            Datagrid_Ogrenciler.SelectedItem = null;
        }

        private void Button_guncelle_kaydet_Click(object sender, RoutedEventArgs e)
        {
            Grid_Modal.Visibility = Visibility.Hidden;
            Grid_guncelle.Visibility = Visibility.Hidden;
            secilen_ogr.Okulno = Int32.Parse(TextBox_guncelle_okulno.Text);
            secilen_ogr.Ad = TextBox_guncelle_ad.Text;
            secilen_ogr.Soyad = TextBox_guncelle_soyad.Text;
            secilen_ogr.Sinif = (Int32)ComboBox_guncelle_sinif.SelectedValue;
            veriler.SaveChanges();
            Grid_guncelle.Visibility = Visibility.Hidden;
            Datagrid_Ogrenciler.SelectedItem = null;
            Datagrid_Ogrenciler.Items.Refresh();
        }

        private void Button_kayit_sil_Click(object sender, RoutedEventArgs e)
        {
            if (Datagrid_Ogrenciler.SelectedItem != null)
            {
                Grid_Modal.Visibility = Visibility.Hidden;
                Grid_guncelle.Visibility = Visibility.Hidden;
                veriler.Ogrencis.Local.Remove(secilen_ogr);
                Grid_guncelle.Visibility = Visibility.Hidden;
                veriler.SaveChanges();
            }
        }

        private void ComboBox_FilterSinif_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            
            if(ComboBox_FilterSinif.SelectedIndex>-1)
            {   CheckBox_TumSinifGoster.IsChecked =false;
                Int32 secilen = (Int32) ComboBox_FilterSinif.SelectedValue; 
                Datagrid_Ogrenciler.ItemsSource= veriler.Ogrencis.Local.ToObservableCollection().Where(x=>x.Sinif==secilen);
            }            
        }

        private void CheckBox_TumSinifGoster_Checked(object sender, RoutedEventArgs e)
        {
            if (CheckBox_TumSinifGoster.IsChecked==true)
            {
                Datagrid_Ogrenciler.ItemsSource = veriler.Ogrencis.Local.ToObservableCollection();
                ComboBox_FilterSinif.SelectedIndex = -1;
            }
        }
    }
}
