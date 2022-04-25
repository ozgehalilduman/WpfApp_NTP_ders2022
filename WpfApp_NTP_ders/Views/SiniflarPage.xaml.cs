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

namespace WpfApp_NTP_ders.Views
{
    /// <summary>
    /// SiniflarPage.xaml etkileşim mantığı
    /// </summary>
    public partial class SiniflarPage : Page
    {
        vt2022Context veriler = new vt2022Context();
        Sinif secilen_snf = new Sinif();
        private void yenikayit_alan_temizle()
        {
            TextBox_yeni_sube.Text = "";
            TextBox_yeni_seviye.Text = "";
            if (Datagrid_Siniflar.SelectedItem != null)
                Datagrid_Siniflar.SelectedItem = null;
            Grid_yeni.Visibility = Visibility.Hidden;
            Button_yenikayit.IsEnabled = true;
            ComboBox_yeni_okul.SelectedIndex = -1;
        }
        public SiniflarPage()
        {
            InitializeComponent();
            Grid_Modal.Visibility = Visibility.Hidden;
            veriler.Database.EnsureCreated();
            veriler.Sinifs.Load();
            veriler.Okuls.Load();
            CheckBox_TumOkulGoster.IsChecked = true;
            ComboBox_FilterOkul.ItemsSource = veriler.Okuls.Local.ToObservableCollection();
            Datagrid_Siniflar.ItemsSource = veriler.Sinifs.Local.ToObservableCollection();
            //modal kısımdaki comboboxları dolduruyorum
            ComboBox_yeni_okul.ItemsSource = veriler.Okuls.Local.ToObservableCollection();
            ComboBox_guncelle_okul.ItemsSource = veriler.Okuls.Local.ToObservableCollection();
        }

        private void Button_yenikayit_Click(object sender, RoutedEventArgs e)
        {
            Grid_Modal.Visibility = Visibility.Visible;
            Grid_yeni.Visibility = Visibility.Visible;
            Grid_guncelle.Visibility = Visibility.Hidden;
            Button_yenikayit.IsEnabled = false;
        }

        private void ComboBox_FilterOkul_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (ComboBox_FilterOkul.SelectedIndex > -1)
            {
                CheckBox_TumOkulGoster.IsChecked = false;
                Int32 secilen = (Int32)ComboBox_FilterOkul.SelectedValue;
                Datagrid_Siniflar.ItemsSource = veriler.Sinifs.Local.ToObservableCollection().Where(x => x.Okul == secilen);
            }
        }

        private void Datagrid_Siniflar_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (Datagrid_Siniflar.SelectedItem != null)
            {
                Grid_Modal.Visibility = Visibility.Visible;
                Grid_yeni.Visibility = Visibility.Hidden;
                Grid_guncelle.Visibility = Visibility.Visible;
                secilen_snf = (Sinif)Datagrid_Siniflar.SelectedItem;
                TextBox_guncelle_seviye.Text = secilen_snf.Seviye.ToString();
                TextBox_guncelle_sube.Text = secilen_snf.Sube;                
                ComboBox_guncelle_okul.SelectedItem = secilen_snf.OkulNavigation;
                Button_yenikayit.IsEnabled = true;
            }
        }

        private void Button_kayit_sil_Click(object sender, RoutedEventArgs e)
        {
            if (Datagrid_Siniflar.SelectedItem != null)
            {
                Grid_Modal.Visibility = Visibility.Hidden;
                Grid_guncelle.Visibility = Visibility.Hidden;
                veriler.Sinifs.Local.Remove(secilen_snf);
                Grid_guncelle.Visibility = Visibility.Hidden;
                veriler.SaveChanges();
            }
        }

        private void Button_guncelle_iptal_Click(object sender, RoutedEventArgs e)
        {
            Grid_Modal.Visibility = Visibility.Hidden;
            Grid_guncelle.Visibility = Visibility.Hidden;
            Datagrid_Siniflar.SelectedItem = null;
        }

        private void Button_guncelle_kaydet_Click(object sender, RoutedEventArgs e)
        {
            Grid_Modal.Visibility = Visibility.Hidden;
            Grid_guncelle.Visibility = Visibility.Hidden;
            secilen_snf.Seviye = Int32.Parse(TextBox_guncelle_seviye.Text);
            secilen_snf.Sube = TextBox_guncelle_sube.Text;
            secilen_snf.Okul = (Int32)ComboBox_guncelle_okul.SelectedValue;
            veriler.SaveChanges();
            Grid_guncelle.Visibility = Visibility.Hidden;
            Datagrid_Siniflar.SelectedItem = null;
            Datagrid_Siniflar.Items.Refresh();
        }

        private void Button_yeni_iptal_Click(object sender, RoutedEventArgs e)
        {
            yenikayit_alan_temizle();
            Grid_Modal.Visibility = Visibility.Hidden;
        }

        private void Button_yeni_kaydet_Click(object sender, RoutedEventArgs e)
        {
            Sinif yeni_snf = new Sinif();
            yeni_snf.Seviye = Int32.Parse(TextBox_yeni_seviye.Text);
            yeni_snf.Sube = TextBox_yeni_sube.Text;            
            yeni_snf.Okul = (Int32)ComboBox_yeni_okul.SelectedValue;
            veriler.Sinifs.Local.Add(yeni_snf);
            veriler.SaveChanges();
            yenikayit_alan_temizle();
            Grid_Modal.Visibility = Visibility.Hidden;
        }

        private void CheckBox_TumOkulGoster_Checked(object sender, RoutedEventArgs e)
        {
            if (CheckBox_TumOkulGoster.IsChecked == true)
            {
                Datagrid_Siniflar.ItemsSource = veriler.Sinifs.Local.ToObservableCollection();
                ComboBox_FilterOkul.SelectedIndex = -1;
            }
        }
    }
}
