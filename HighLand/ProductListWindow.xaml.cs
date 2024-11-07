using HighLand.Models;
using HighLand.repositories;
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
using System.Windows.Shapes;

namespace HighLand
{
    /// <summary>
    /// Interaction logic for ProductListWindow.xaml
    /// </summary>
    public partial class ProductListWindow : Window
    {
        private IGenericRepository<Product> productRepository = new GenericRepository<Product>();
        public ObservableCollection<Product> Products { get; set; } = new ObservableCollection<Product>();
        public ProductListWindow()
        {
            InitializeComponent();
            DataContext = this;
            LoadProducts();
        }

        private void BackButton_Click(object sender, RoutedEventArgs e)
        {
            FirstOrderWindow firstOrderWindow = new FirstOrderWindow();
            firstOrderWindow.Show();
            this.Close();

        }


        private void LoadProducts()
        {
           var list = productRepository.GetAllIncluding(x => x.Category);
            foreach (var product in list)
            {
                Products.Add(product);
            }
        }

        private void ViewProduct_Click(object sender, RoutedEventArgs e)
        {
            var product = (sender as Button)?.Tag as Product;
            if (product != null)
            {
                ProductDetailWindow detailView = new ProductDetailWindow(product);
                detailView.ShowDialog();
            }
        }

        private void EditProduct_Click(object sender, RoutedEventArgs e)
        {
            var product = (sender as Button)?.Tag as Product;
            if (product != null)
            {
                ProductEditWindow editView = new ProductEditWindow(product);
                editView.Show();
                this.Close();
            }
        }

        private void DeleteProduct_Click(object sender, RoutedEventArgs e)
        {
            var product = (sender as Button)?.Tag as Product;
            if (product != null && MessageBox.Show("Are you sure you want to delete this product?",
                                                    "Confirm Delete",
                                                    MessageBoxButton.YesNo) == MessageBoxResult.Yes)
            {
                var result = productRepository.Get(x => x.ProductId == product.ProductId);
                if (result != null)
                {
                    productRepository.Remove(result);

                    MessageBox.Show($"Deleted Product: {product.ProductName}");
                }
               
            }
        }

        private void LockUnlockProduct_Click(object sender, RoutedEventArgs e)
        {
            var product = (sender as Button)?.Tag as Product;
            if (product != null)
            {
                var result = productRepository.Get(x => x.ProductId == product.ProductId);
                if (result != null)
                {
                    result.Status = !result.Status; 
                    productRepository.Update(result);  

                    MessageBox.Show(result.Status == true ?
                                    $"Locked Product: {result.ProductName}" :
                                    $"Unlocked Product: {result.ProductName}");
                }
            }
        }

    }
}
