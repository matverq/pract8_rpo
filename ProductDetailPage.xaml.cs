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
    /// Логика взаимодействия для ProductDetailPage.xaml
    /// </summary>
    public partial class ProductDetailPage : Page
    {
        private Product _currentProduct;

        public ProductDetailPage(Product product)
        {
            InitializeComponent();
            _currentProduct = product;

            this.DataContext = _currentProduct;
            TxtName.SetBinding(TextBlock.TextProperty, "Name");
            TxtPrice.SetBinding(TextBlock.TextProperty, "Price");
            TxtDesc.SetBinding(TextBlock.TextProperty, "Description");
            TxtCategory.SetBinding(TextBlock.TextProperty, "Category");
        }

        private void AddToCart_Click(object sender, RoutedEventArgs e)
        {
            var existingItem = DataStore.Cart.FirstOrDefault(x => x.Product.Name == _currentProduct.Name);
            if (existingItem != null) existingItem.Quantity++;
            else DataStore.Cart.Add(new CartItem { Product = _currentProduct, Quantity = 1 });

            MessageBox.Show("Добавлено!");
            NavigationService.GoBack();
        }
    }
}
