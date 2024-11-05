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
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace HighLand
{
    /// <summary>
    /// Interaction logic for TableWindow.xaml
    /// </summary>
    public partial class TableWindow : Window
    {
        private IGenericRepository<Table> tableRepository = new GenericRepository<Table>();
        public ObservableCollection<Table> Tables { get; set; }
        public TableWindow()
        {
            InitializeComponent();
            LoadTable();
        }

        private void LoadTable()
        {
            Tables = new ObservableCollection<Table>();
            var list = tableRepository.GetAll();
            foreach (var item in list)
            {
                Tables.Add(item);
            }
            DataContext = this;
        }

        private void TableListBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var selectedTable = (Table)TableListBox.SelectedItem;
            if (selectedTable != null)
            {
               if(selectedTable.Status == true)
                {
                    MainWindow mainWindow = new MainWindow(selectedTable);
                    mainWindow.Show();
                    this.Close();
                }
            }
        }
    }
}
