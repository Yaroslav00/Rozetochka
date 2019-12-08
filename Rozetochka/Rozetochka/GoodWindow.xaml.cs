using DataAccess;
using System.Collections.ObjectModel;
using System.Windows;
using Business.Interfaces;
using Business.Services;

namespace Rozetochka
{
    /// <summary>
    /// Interaction logic for LoginWindow.xaml
    /// </summary>
    public partial class GoodWindow : Window
    {
        private readonly IOrderService _orderService;
        public GoodWindow()
        {
            _orderService = new OrderService();
            InitializeComponent();

            CategorySelect.ItemsSource = new ObservableCollection<Category>
            {
                new Category("Phones"),
                new Category("GPUs"),
                new Category("Monitors"),
                new Category("Perifirals"),
                new Category("Laptops"),
            };
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
