using HighLand.Models;
using HighLand.repositories;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace HighLand
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        IGenericRepository<Product> productRepository = new GenericRepository<Product>();
        private int currentOffset = 0;
        public MainWindow()
        {
            InitializeComponent();
            LoadProducts();
        }

        private void LoadProducts()
        {
            var products = productRepository.GetAll().Skip(currentOffset).Take(30).ToList();
            ProductListBox.ItemsSource = products;
        }
        private void Border_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (sender is FrameworkElement element && element.DataContext is Product product)
            {
                int productId = product.ProductId; 
                MessageBox.Show(""+productId);
            }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            if (currentOffset <= productRepository.GetAll().Count() - 30)
            {
                currentOffset += 30;
            }
            LoadProducts();
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            if (currentOffset >= 30)
            {
                currentOffset -= 30;
            }
            LoadProducts();
            Application.Current.Properties["UserName"] = "John Doe";


        }
    }
}