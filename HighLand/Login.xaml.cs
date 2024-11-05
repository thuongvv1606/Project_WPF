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
    /// Interaction logic for Login.xaml
    /// </summary>
    public partial class Login : Window
    {
        private IGenericRepository<User> userRepository = new GenericRepository<User>();
        public Login()
        {
            InitializeComponent();
        }

        private void btnLogin_Click(object sender, RoutedEventArgs e)
        {
            string email = txtEmail.Text;
            string password = txtPassword.Password;
            User user = userRepository.Get(x => x.Email == email);
            if (user != null)
            {
                if (user.Password == password)
                {
                    FirstOrderWindow wpf = new FirstOrderWindow();
                    wpf.Show();
                }
                else
                {
                    MessageBox.Show("Invalid account. Please check your email and password", "Error", MessageBoxButton.OK);
                    return;
                }

            }
            else
            {
                MessageBox.Show("Invalid account. Please check your email and password", "Error", MessageBoxButton.OK);
                return;
            }
            this.Hide();
        }

        private void btnCancel_Click(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }
    }
}
