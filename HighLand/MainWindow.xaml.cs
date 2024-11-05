using HighLand.Models;
using HighLand.repositories;
using System.Collections.ObjectModel;
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
using static System.Windows.Forms.VisualStyles.VisualStyleElement.TextBox;

namespace HighLand
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private IGenericRepository<Product> productRepository = new GenericRepository<Product>();
        private IGenericRepository<User> userRepository = new GenericRepository<User>();
        private IGenericRepository<Role> roleRepository = new GenericRepository<Role>();
        private IGenericRepository<Order> orderRepository = new GenericRepository<Order>();
        private IGenericRepository<ProductCategory> categoryRepository = new GenericRepository<ProductCategory>();
        private int currentOffset = 0;
        private List<CartItem> cartItems = new List<CartItem>();
        public ObservableCollection<ProductCategory> Categories { get; set; }

        public decimal TotalAmount => cartItems.Sum(item => item.Total);
        public decimal TaxAmount => TotalAmount * 0.20m;
        public decimal FinalAmount => TotalAmount + TaxAmount;
        public Models.Table Table { get; private set; }
        User user = new User();


        public MainWindow(Models.Table table)
        {
            InitializeComponent();
            LoadCategory();
            LoadProducts();
            LoadCarts();
            LoadUser();
            Table = table;

        }

        private void LoadCategory()
        {
            Categories = new ObservableCollection<ProductCategory>
    {
        new ProductCategory {CategoryId = -2, CategoryName = "Tất cả sản phẩm" },
        new ProductCategory { CategoryId = -1, CategoryName = "Bán chạy" },
    };

            var additionalCategories = categoryRepository.GetAll();

            foreach (var category in additionalCategories)
            {
                Categories.Add(category);
            }

            CategoryListBox.ItemsSource = Categories;
        }

        private void LoadUser()
        {
            user = userRepository.Get(x => x.UserId == 1);
            string role = roleRepository.Get(x => x.RoleId == user.RoleId).RoleName;
            txtName.Text = $"{role}: {user.FullName}";
        }


        private void LoadCarts()
        {
            CartListBox.ItemsSource = null;
            CartListBox.ItemsSource = cartItems;
        }
        private void LoadProducts()
        {
            txtDate.Text = DateTime.UtcNow.ToString("MMMM dd, dddd hh:mm:ss tt");

            var products = productRepository.GetAll().Skip(currentOffset).Take(30).ToList();
            ProductListBox.ItemsSource = products;
        }

        private void Border_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (sender is FrameworkElement element && element.DataContext is Product product)
            {
                if (product.Status == true && product.Quantity > 0)
                {
                    CartItem cartItem = new CartItem
                    {
                        ProductId = product.ProductId,
                        ProductName = product.ProductName,
                        Price = product.Price,
                        Quantity = 1,
                    };

                    AddItem(cartItem);
                    LoadCarts();
                    UpdateTotalAmount();
                }
                else
                {
                    MessageBox.Show("Sản phẩm không khả dụng.");
                }
            }
        }


        private void Button_Click(object sender, RoutedEventArgs e)
        {
            if (currentOffset <= productRepository.GetAll().Count() - 30)
            {
                currentOffset += 30;
                LoadProducts();
            }
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            if (currentOffset >= 30)
            {
                currentOffset -= 30;
                LoadProducts();
            }

            Application.Current.Properties["UserName"] = "John Doe";
        }

        public void AddItem(CartItem item)
        {
            var existingItem = cartItems.FirstOrDefault(i => i.ProductName == item.ProductName);
            if (existingItem != null)
            {
                existingItem.Quantity++;
            }
            else
            {
                cartItems.Add(item);
            }
        }

        private void IncreaseQuantity_Click(object sender, RoutedEventArgs e)
        {
            if (sender is Button button && button.DataContext is CartItem cartItem)
            {
                cartItem.Quantity++;
                CartListBox.Items.Refresh();
            }
            UpdateTotalAmount();
        }

        private void DecreaseQuantity_Click(object sender, RoutedEventArgs e)
        {
            if (sender is Button button && button.DataContext is CartItem cartItem)
            {
                if (cartItem.Quantity > 1)
                {
                    cartItem.Quantity--;
                    CartListBox.Items.Refresh();
                }
                else
                {
                    cartItems.Remove(cartItem);
                    LoadCarts();
                    CartListBox.Items.Refresh();
                }
            }
            UpdateTotalAmount();
        }

        private void UpdateTotalAmount()
        {
            txtTotalAmount.Text = TotalAmount.ToString("C");
            txtTaxAmount.Text = TaxAmount.ToString("C");
            txtFinalAmount.Text = FinalAmount.ToString("C");
            txtOrderedItems.Text = $"{cartItems.Sum(item => item.Quantity)}/{cartItems.Count()}";

        }

        private void btnReOrder(object sender, RoutedEventArgs e)
        {
            cartItems = new List<CartItem>();
            UpdateTotalAmount();
            LoadCarts();
        }

        private void btnEnter(object sender, RoutedEventArgs e)
        {
            MessageBoxResult result = MessageBox.Show(
                "Bạn có chắc chắn muốn xác nhận đơn hàng này không?",
                "Xác Nhận Đơn Hàng",
                MessageBoxButton.YesNo,
                MessageBoxImage.Question
            );

            if (result == MessageBoxResult.Yes)
            {
                ConfirmOrder();
            }
        }


        private void ConfirmOrder()
        {
            if (cartItems.Count == 0)
            {
                MessageBox.Show("Giỏ hàng rỗng. Không thể xác nhận đơn hàng.");
                return;
            }
            Order newOrder = new Order();
            newOrder.OrderDate = DateTime.Now;
            newOrder.UserId = user.UserId;
            // CustomerId = selectedCustomer?.CustomerId, 
            newOrder.TotalAmount = TotalAmount;
            newOrder.Tax = TaxAmount;
            // Discount = CalculateDiscount(cartItems),
            newOrder.Status = true;
            // PaymentMethod = selectedPaymentMethod, 
            newOrder.OrderType = true;
            if(Table != null)
            {
                newOrder.TableId = Table.TableId;
            }

            foreach (var item in cartItems)
            {
                OrderDetail orderDetail = new OrderDetail
                {
                    ProductId = item.ProductId,
                    Quantity = item.Quantity,
                    Price = item.Price,
                    TotalPrice = item.Price * item.Quantity
                };
                newOrder.OrderDetails.Add(orderDetail);

                UpdateInventory(item.ProductId, item.Quantity);
            }

            orderRepository.Add(newOrder);

            MessageBox.Show("Đơn hàng đã được xác nhận thành công!");
            cartItems.Clear();
            LoadCarts();
            UpdateTotalAmount();
        }


        private void UpdateInventory(int productId, int quantitySold)
        {
            var product = productRepository.Get(p => p.ProductId == productId);
            if (product != null)
            {
                product.Quantity -= quantitySold;
                productRepository.Update(product);
            }
        }

        private void btnExit(object sender, RoutedEventArgs e)
        {
            MessageBoxResult result = MessageBox.Show(
        "Bạn có chắc chắn muốn thoát ứng dụng không?",
        "Xác Nhận Thoát",
        MessageBoxButton.YesNo,
        MessageBoxImage.Question
    );

            // Nếu người dùng chọn Yes, thoát ứng dụng
            if (result == MessageBoxResult.Yes)
            {
                Application.Current.Shutdown();
            }
        }

        private void CategoryListBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var products = new List<Product>();
            var selectedRow = CategoryListBox.SelectedItem as ProductCategory;
            if (selectedRow != null)
            {
                if (selectedRow.CategoryId == -2)
                {
                    products = productRepository.GetAll().Skip(currentOffset).Take(30).ToList();
                    ProductListBox.ItemsSource = products;

                }
                else if (selectedRow.CategoryId == -1)
                {
                    products = productRepository.GetAllIncluding(x => x.OrderDetails)
    .Select(p => new
    {
        Product = p,
        OrderCount = p.OrderDetails.Sum(od => (int?)od.Quantity) ?? 0 // Đảm bảo không có giá trị null
    })
    .OrderByDescending(x => x.OrderCount) // Sắp xếp theo số lượng đơn hàng
    .Skip(currentOffset) // Bỏ qua các sản phẩm trước đó
    .Take(30) // Lấy 30 sản phẩm tiếp theo
    .Select(x => x.Product) // Chọn sản phẩm từ đối tượng
    .ToList();
                    ProductListBox.ItemsSource = products;
                }
                else
                {
                    products = productRepository.GetAll().Where(x => x.CategoryId == selectedRow.CategoryId).Skip(currentOffset).Take(30).ToList();
                    ProductListBox.ItemsSource = products;

                }
            }
        }

        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            FirstOrderWindow firstOrderWindow = new FirstOrderWindow();
            firstOrderWindow.Show();
            this.Close();
        }
    }
}