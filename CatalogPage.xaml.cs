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

namespace pract8_RPO
{
    /// <summary>
    /// Логика взаимодействия для CatalogPage.xaml
    /// </summary>
    public partial class CatalogPage : Page
    {
        private ObservableCollection<Product> _displayedProducts = new ObservableCollection<Product>();

        public CatalogPage(string searchQuery = "")
        {
            InitializeComponent();
            ProductsList.ItemsSource = _displayedProducts;

            var categories = new List<string> { "Все" };
            categories.AddRange(DataStore.Products.Select(p => p.Category).Distinct());
            CmbCategory.ItemsSource = categories;
            CmbCategory.SelectedIndex = 0;

            if (!string.IsNullOrEmpty(searchQuery))
            {
                TxtSearchCat.Text = searchQuery;
            }
        }

        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            ApplyFilters();
        }

        private void ApplyFilters()
        {
            if (ProductsList == null) return;

            decimal min = 0, max = decimal.MaxValue;
            decimal.TryParse(TxtMinPrice?.Text, out min);
            decimal.TryParse(TxtMaxPrice?.Text, out max);
            if (min == 0) min = 0;
            if (max == 0) max = decimal.MaxValue;

            string category = CmbCategory?.SelectedItem as string;
            string search = TxtSearchCat?.Text.ToLower() ?? "";

            var filtered = DataStore.Products.Where(p =>
                (category == "Все" || p.Category == category) &&
                (p.Price >= min && p.Price <= max) &&
                (string.IsNullOrWhiteSpace(search) || p.Name.ToLower().Contains(search))
            ).ToList();

            _displayedProducts.Clear();
            foreach (var item in filtered)
            {
                _displayedProducts.Add(item);
            }
        }

        private void Filter_Changed(object sender, RoutedEventArgs e)
        {
            ApplyFilters();
        }

        private void AddToCart_Click(object sender, RoutedEventArgs e)
        {
            var btn = sender as Button;
            var product = btn.DataContext as Product;

            if (product != null)
            {
                var existingItem = DataStore.Cart.FirstOrDefault(x => x.Product.Name == product.Name);
                if (existingItem != null) existingItem.Quantity++;
                else DataStore.Cart.Add(new CartItem { Product = product, Quantity = 1 });

                MessageBox.Show("Товар успешно добавлен в корзину!");
            }
        }

        private void Details_Click(object sender, RoutedEventArgs e)
        {
            var btn = sender as Button;
            var product = btn.DataContext as Product;
            NavigationService.Navigate(new ProductDetailPage(product));
        }

        private void ScrollViewer_PreviewMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            ProductsList.SelectedItem = null;
        }
    }
}
