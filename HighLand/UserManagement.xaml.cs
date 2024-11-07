using HighLand.Models;
using HighLand.repositories;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Net;
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
using System.Xml.Linq;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace HighLand
{
    /// <summary>
    /// Interaction logic for UserManagement.xaml
    /// </summary>
    public partial class UserManagement : Window
    {
        HighLandContext context = new HighLandContext();
        public UserManagement()
        {
            InitializeComponent();
            Male.IsChecked = true;
            loadLv();

        }
        public void loadLv()
        {
            var list = context.Users.Select(u => new
            {
                UserId = u.UserId,
                FullName = u.FullName,
                Email = u.Email,
                Gender = u.Gender ? "Male" : "Female",
                Role = u.Role.RoleName,
                PhoneNumber = u.PhoneNumber,
                Address = u.Address
            }).ToList();
            lv.ItemsSource = list;

        }
        private void ListView_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var selectedRow = lv.SelectedItem as dynamic;

            if (selectedRow != null)
            {
                txtId.Text = selectedRow.UserId.ToString();
                txtName1.Text = selectedRow.FullName;
                if (selectedRow.Gender == "Male")
                {
                    Male.IsChecked = true;
                }
                if (selectedRow.Gender == "Female")
                {
                    Female.IsChecked = true;
                }
                txtEmail1.Text = selectedRow.Email;
                txtAddress.Text = selectedRow.Address;
                txtPhone.Text = selectedRow.PhoneNumber;
                txtRole.Text = selectedRow.Role;

            }
        }

        private void btnQuit_Click(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }

        private void btnSearch_Click(object sender, RoutedEventArgs e)
        {
            string name = txtName.Text.Trim();
            string email = txtEmail.Text.Trim();
            var query = context.Users.AsQueryable();
            if (!string.IsNullOrEmpty(name))
            {
                query = query.Where(s => s.FullName.StartsWith(name));
            }
            if (!string.IsNullOrEmpty(email))
            {
                query = query.Where(s => s.Email.StartsWith(email));
            }
            var listlv = query.Select(u => new
            {
                UserId = u.UserId,
                FullName = u.FullName,
                Email = u.Email,
                Gender = u.Gender ? "Male" : "Female",
                Role = u.Role.RoleName,
                PhoneNumber = u.PhoneNumber,
                Address = u.Address
            }).ToList();
            lv.ItemsSource = listlv;
        }

        private void btnEdit_Click(object sender, RoutedEventArgs e)
        {
            var editId = int.Parse(txtId.Text);
            var editObject = context.Users.FirstOrDefault(x => x.UserId == editId);
            if (editObject != null)
            {
                editObject.FullName = txtName1.Text;
                editObject.Email = txtEmail1.Text;
                editObject.Gender = Male.IsChecked == true;
                editObject.Role = context.Roles.FirstOrDefault(r => r.RoleName == txtRole.Text);
                editObject.PhoneNumber = txtPhone.Text;
                editObject.Address = txtAddress.Text;
                context.SaveChanges();
                loadLv();
            }
        }

    

        private void btnRefresh_Click(object sender, RoutedEventArgs e)
        {
            loadLv();
            txtId.Text = "";
            txtName1.Text = "";
            Male.IsChecked = true;
            txtEmail1.Text = "";
            txtRole.Text = "";
            txtPhone.Text = "";
            txtAddress.Text = "";
        }

        private void btnDelete_Click(object sender, RoutedEventArgs e)
        {
            var SelectId = int.Parse(txtId.Text);
            var SelectObject = context.Users.Include(s => s.Role).Include(s => s.Orders).FirstOrDefault(s => s.UserId == SelectId);
            if (SelectObject != null)
            {
                context.Remove(SelectObject);
                context.SaveChanges();
            }
            loadLv();
        }
    }
}
