using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace pract8_RPO
{
    public class Product
    {
        public string Name { get; set; }
        public decimal Price { get; set; }
        public string Category { get; set; }
        public string Description { get; set; }
    }

    public class CartItem : INotifyPropertyChanged
    {
        private int _quantity;
        public Product Product { get; set; }

        public int Quantity
        {
            get => _quantity;
            set
            {
                _quantity = value;
                OnPropertyChanged();
                OnPropertyChanged(nameof(TotalSum));
            }
        }

        public decimal TotalSum => Product.Price * Quantity;

        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged([CallerMemberName] string name = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }
    }

    public static class DataStore
    {
        public static ObservableCollection<Product> Products { get; set; } = new ObservableCollection<Product>
        {
            new Product { Name = "Смартфон Galaxy S23", Price = 80000, Category = "Смартфоны", Description = "Флагманский смартфон с отличной камерой." },
            new Product { Name = "Ноутбук MacBook Air", Price = 120000, Category = "Ноутбуки", Description = "Легкий, тонкий и невероятно мощный." },
            new Product { Name = "Наушники Sony WH-1000XM5", Price = 30000, Category = "Аудио", Description = "Лучшее активное шумоподавление на рынке." },
            new Product { Name = "Телевизор LG OLED", Price = 150000, Category = "ТВ и Видео", Description = "Идеальный черный цвет и бесконечный контраст." }
        };

        public static ObservableCollection<CartItem> Cart { get; set; } = new ObservableCollection<CartItem>();
    }
}
