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

namespace pract8_RPO
{
    /// <summary>
    /// Логика взаимодействия для CartPage.xaml
    /// </summary>
    public partial class CartPage : Page
    {
        public CartPage()
        {
            InitializeComponent();
            UpdateTotalSum();
            DataStore.Cart.CollectionChanged += (s, e) => UpdateTotalSum();
        }

        private void TxtPromo_TextChanged(object sender, TextChangedEventArgs e)
        {
            UpdateTotalSum();
        }

        private void UpdateTotalSum()
        {
            if (TotalAmountText == null) return;

            decimal total = DataStore.Cart.Sum(item => item.TotalSum);

            if (TxtPromo != null && TxtPromo.Text.Trim().ToUpper() == "SALE")
            {
                total = total * 0.9m;
            }

            TotalAmountText.Text = total.ToString("C");
        }

        private void Checkout_Click(object sender, RoutedEventArgs e)
        {
            if (DataStore.Cart.Count == 0) return;
            MessageBox.Show("Заказ успешно оформлен");
            DataStore.Cart.Clear();
            UpdateTotalSum();
        }

        private void Continue_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new CatalogPage());
        }
    }
}
