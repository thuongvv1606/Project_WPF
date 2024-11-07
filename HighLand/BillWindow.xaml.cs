using HighLand.Models;
using HighLand.repositories;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Printing;
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
    /// Interaction logic for BillWindow.xaml
    /// </summary>
    public partial class BillWindow : Window
    {
        private readonly OrderDTO orderDTO;

        public BillWindow(OrderDTO _orderDTO)
        {
            InitializeComponent();
            orderDTO = _orderDTO;
            DataContext = new
            {
                orderDTO.OrderId,
                orderDTO.TableName,
                orderDTO.UserName,
                orderDTO.OrderDate,
                orderDTO.OrderDetails,
                orderDTO.TotalAmount,
                orderDTO.Tax,
                orderDTO.TotalPayment
            };
        }

        private void PrintButton_Click(object sender, RoutedEventArgs e)
        {
            // Lưu trạng thái hiện tại của cửa sổ để phục hồi sau khi in
            bool wasMaximized = this.WindowState == WindowState.Maximized;
            this.WindowState = WindowState.Normal;

            // Lưu lại kích thước ban đầu
            double initialHeight = this.ActualHeight;
            double initialWidth = this.ActualWidth;

            // Đặt lại kích thước để in
            this.Width = 955;
            //this.Height = initialHeight + (ScrollerContent.ActualHeight - Scroller.ActualHeight) + 20;

            Print(this); // Gọi hàm in đã được định nghĩa

            // Khôi phục kích thước ban đầu
            this.Height = initialHeight;
            this.Width = initialWidth;

            if (wasMaximized)
            {
                this.WindowState = WindowState.Maximized;
            }
        }

        private void Print(Visual v)
        {
            FrameworkElement e = v as FrameworkElement;
            if (e == null)
                return;

            PrintDialog pd = new PrintDialog();
            if (pd.ShowDialog() == true)
            {
                pd.PrintTicket.PageMediaSize = new PageMediaSize(PageMediaSizeName.ISOA4);

                // Lưu lại tỷ lệ ban đầu
                Transform originalScale = e.LayoutTransform;

                // Lấy khả năng in của máy in và tính toán tỷ lệ
                var capabilities = pd.PrintQueue.GetPrintCapabilities(pd.PrintTicket);
                double scale = Math.Min(capabilities.PageImageableArea.ExtentWidth / e.ActualWidth,
                                        capabilities.PageImageableArea.ExtentHeight / e.ActualHeight);

                // Thay đổi kích thước để phù hợp với trang in
                e.LayoutTransform = new ScaleTransform(scale, scale);
                Size sz = new Size(capabilities.PageImageableArea.ExtentWidth, capabilities.PageImageableArea.ExtentHeight);

                e.Measure(sz);
                e.Arrange(new Rect(new Point(capabilities.PageImageableArea.OriginWidth, capabilities.PageImageableArea.OriginHeight), sz));

                // In hình ảnh đã điều chỉnh
                pd.PrintVisual(v, "Hóa Đơn");

                // Khôi phục lại kích thước ban đầu
                e.LayoutTransform = originalScale;
            }
        }

    }
}
