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
    /// OkullarPage.xaml etkileşim mantığı
    /// </summary>
    public partial class OkullarPage : Page
    {
        vt2022Context veriler = new vt2022Context();
        Okul secilen_okul = new Okul();
        private void yenikayit_alan_temizle()
        {
            TextBox_yeni_kisaad.Text = "";
            TextBox_yeni_tamad.Text = "";
            TextBox_yeni_mevcut.Text = "";
            if (Datagrid_Okullar.SelectedItem != null)
                Datagrid_Okullar.SelectedItem = null;
            Grid_yeni.Visibility = Visibility.Hidden;
            Button_yenikayit.IsEnabled = true;
        }
        public OkullarPage()
        {
            InitializeComponent();
            Grid_Modal.Visibility = Visibility.Hidden;
            veriler.Database.EnsureCreated();
            veriler.Okuls.Load();
            Datagrid_Okullar.ItemsSource = veriler.Okuls.Local.ToObservableCollection();
        }

        private void Button_yenikayit_Click(object sender, RoutedEventArgs e)
        {
            Grid_Modal.Visibility = Visibility.Visible;
            Grid_yeni.Visibility = Visibility.Visible;
            Grid_guncelle.Visibility = Visibility.Hidden;
            Button_yenikayit.IsEnabled = false;
        }

        private void Datagrid_Ogrenciler_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void ComboBox_FilterSinif_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void CheckBox_TumSinifGoster_Checked(object sender, RoutedEventArgs e)
        {

        }

        private void Button_guncelle_kaydet_Click(object sender, RoutedEventArgs e)
        {
            Grid_Modal.Visibility = Visibility.Hidden;
            Grid_guncelle.Visibility = Visibility.Hidden;
            secilen_okul.Mevcut = Int32.Parse(TextBox_guncelle_mevcut.Text);
            secilen_okul.KisaAd = TextBox_guncelle_kisaad.Text;
            secilen_okul.TamAd = TextBox_guncelle_tamad.Text;
            veriler.SaveChanges();
            Grid_guncelle.Visibility = Visibility.Hidden;
            Datagrid_Okullar.SelectedItem = null;
            Datagrid_Okullar.Items.Refresh();
        }

        private void Button_guncelle_iptal_Click(object sender, RoutedEventArgs e)
        {
            Grid_Modal.Visibility = Visibility.Hidden;
            Grid_guncelle.Visibility = Visibility.Hidden;
            Datagrid_Okullar.SelectedItem = null;
        }

        private void Button_kayit_sil_Click(object sender, RoutedEventArgs e)
        {
            if (Datagrid_Okullar.SelectedItem != null)
            {
                Grid_Modal.Visibility = Visibility.Hidden;
                Grid_guncelle.Visibility = Visibility.Hidden;
                veriler.Okuls.Local.Remove(secilen_okul);
                Grid_guncelle.Visibility = Visibility.Hidden;
                veriler.SaveChanges();
            }
        }

        private void Button_yeni_iptal_Click(object sender, RoutedEventArgs e)
        {
            yenikayit_alan_temizle();
            Grid_Modal.Visibility = Visibility.Hidden;
        }

        private void Button_yeni_kaydet_Click(object sender, RoutedEventArgs e)
        {
            Okul yeni_okul = new Okul();
            yeni_okul.Mevcut = Int32.Parse(TextBox_yeni_mevcut.Text);
            yeni_okul.KisaAd = TextBox_yeni_kisaad.Text;
            yeni_okul.TamAd = TextBox_yeni_tamad.Text;            
            veriler.Okuls.Local.Add(yeni_okul);
            veriler.SaveChanges();
            yenikayit_alan_temizle();
            Grid_Modal.Visibility = Visibility.Hidden;
        }

        private void Datagrid_Okullar_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (Datagrid_Okullar.SelectedItem != null)
            {
                Grid_Modal.Visibility = Visibility.Visible;
                Grid_yeni.Visibility = Visibility.Hidden;
                Grid_guncelle.Visibility = Visibility.Visible;
                secilen_okul = (Okul)Datagrid_Okullar.SelectedItem;
                TextBox_guncelle_kisaad.Text = secilen_okul.KisaAd;
                TextBox_guncelle_tamad.Text = secilen_okul.TamAd;
                TextBox_guncelle_mevcut.Text = secilen_okul.Mevcut.ToString();               
                Button_yenikayit.IsEnabled = true;
            }
        }
    }
}
