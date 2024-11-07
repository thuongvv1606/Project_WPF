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

namespace HighLand
{
    /// <summary>
    /// Interaction logic for RightPage.xaml
    /// </summary>
    public partial class RightPage : Page
    {
        public RightPage()
        {
            InitializeComponent();
        }

        private void btnProduct_Click(object sender, RoutedEventArgs e)
        {
            ProductListWindow productListWindow = new ProductListWindow();
            productListWindow.Show();
        }
    }
}
