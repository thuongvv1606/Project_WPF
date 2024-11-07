using HighLand.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
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
    /// Interaction logic for SignUp.xaml
    /// </summary>
    public partial class SignUp : Window
    {
        HighLandContext context = new HighLandContext();
        public SignUp()
        {
            InitializeComponent();
            Male.IsChecked = true;
        }

        private void btnSignUp_Click(object sender, RoutedEventArgs e)
        {
            User u = new User();
            if (txtName.Text.IsNullOrEmpty() || txtEmail.Text.IsNullOrEmpty() || txtPass.Password.IsNullOrEmpty() ||
                txtPass1.Password.IsNullOrEmpty() || txtPhone.Text.IsNullOrEmpty() || txtAddress.Text.IsNullOrEmpty())
            {
                MessageBox.Show("Please fill your information");
            }
            else
            {
                u.FullName = txtName.Text;
                u.Email = txtEmail.Text;
                u.Gender = (Male.IsChecked == true) ? true : false;
                u.PhoneNumber = txtPhone.Text;
                u.Address = txtAddress.Text;
                u.Role = context.Roles.FirstOrDefault(r => r.RoleId == 3);
                u.Status = true;
                u.CreatedDate = DateTime.Now;
                if (txtPass.Password == txtPass1.Password)
                {
                    u.Password = txtPass.Password;
                    if (context.Users.FirstOrDefault(a => a.Email == u.Email) == null)
                    {
                        context.Users.Add(u);
                        context.SaveChanges();
                        MessageBox.Show("Sign up successfully");
                    }
                    else { MessageBox.Show("Email was signed up before"); }
                }
                else
                {
                    MessageBox.Show("Check your password again");
                }
            }
        }

        private void btnCancel_Click(object sender, RoutedEventArgs e)
        {
            this.Hide();
        }
    }
}
