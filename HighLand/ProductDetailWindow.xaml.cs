using HighLand.Models;
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
    /// Interaction logic for ProductDetailWindow.xaml
    /// </summary>
    public partial class ProductDetailWindow : Window
    {
        private readonly Product _product;

        public ProductDetailWindow(Product product)
        {
            InitializeComponent();
        
            InitializeComponent();
            _product = product;

            ProductCodeTextBox.Text = _product.ProductCode;
            ProductNameTextBox.Text = _product.ProductName;
            CategoryIdTextBox.Text = _product.Category.CategoryName;
            UnitsInStockTextBox.Text = _product.UnitsInStock.ToString();
            PriceTextBox.Text = _product.Price.ToString("C");
            DescriptionTextBox.Text = _product.Description;
            StatusCheckBox.IsChecked = _product.Status;

            if (!string.IsNullOrEmpty(_product.Image))
            {
                ProductImage.Source = new BitmapImage(new Uri(_product.ImagePath));
            }
        }

        private void CloseButton_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
