using HighLand.Models;
using HighLand.repositories;
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
using System.Windows.Shapes;

namespace HighLand
{
    /// <summary>
    /// Interaction logic for ProductEditWindow.xaml
    /// </summary>
    public partial class ProductEditWindow : Window
    {
        private IGenericRepository<Product> productRepository = new GenericRepository<Product>();
        private IGenericRepository<ProductCategory> categoryRepository = new GenericRepository<ProductCategory>();
        private readonly Product _product;
        public ProductEditWindow(Product product)
        {
            InitializeComponent();
            _product = product;
            var categories = GetCategories();

            CategoryComboBox.ItemsSource = categories;
            CategoryComboBox.SelectedValuePath = "CategoryId";
            CategoryComboBox.DisplayMemberPath = "CategoryName"; 

            ProductCodeTextBox.Text = _product.ProductCode;
            ProductNameTextBox.Text = _product.ProductName;
            UnitsInStockTextBox.Text = _product.UnitsInStock.ToString();
            PriceTextBox.Text = _product.Price.ToString("C");
            DescriptionTextBox.Text = _product.Description;
            StatusCheckBox.IsChecked = _product.Status;

            CategoryComboBox.SelectedValue = _product.CategoryId;

            if (!string.IsNullOrEmpty(_product.Image))
            {
                ProductImage.Source = new BitmapImage(new Uri(_product.ImagePath));
            }
        }

        private List<ProductCategory> GetCategories()
        {
            return categoryRepository.GetAll().ToList();
        }

        private void SaveButton_Click(object sender, RoutedEventArgs e)
        {
            _product.ProductId = _product.ProductId;
            _product.ProductCode = ProductCodeTextBox.Text;
            _product.ProductName = ProductNameTextBox.Text;
            _product.CategoryId = (int?)CategoryComboBox.SelectedValue; 
            _product.UnitsInStock = int.TryParse(UnitsInStockTextBox.Text, out int unitsInStock) ? unitsInStock : 0;
            _product.Price = decimal.TryParse(PriceTextBox.Text, out decimal price) ? price : 0;
            _product.Description = DescriptionTextBox.Text;
            _product.Status = StatusCheckBox.IsChecked;

            productRepository.Update(_product);
            MessageBox.Show("Product updated successfully!");
            ProductListWindow productListWindow = new ProductListWindow();
            productListWindow.Show();
            this.Close();
        }

        private void CancelButton_Click(object sender, RoutedEventArgs e)
        {
            ProductListWindow productListWindow = new ProductListWindow();
            productListWindow.Show();
            this.Close();
        }
    }
}


