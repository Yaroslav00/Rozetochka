using DataAccess;
using System;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using DataAccess;
using Business.Interfaces;
using Business.Services;
using System;
using System.Runtime.CompilerServices;
using DataAccess.Dto;

namespace Rozetochka
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private ObservableCollection<Goods> goods { get; set; }

        private readonly ICategoryService _categoryService = new CategoryService();
        private readonly IGoodsService _goodsService = new GoodsService();
        private readonly IOrderService _orderService = new OrderService();
        private string orderBy = "За алфавітом";
        public MainWindow()
        {
            InitializeComponent();

            Fetch_Data();

            AddCategory.Visibility = Visibility.Collapsed;
            AddGood.Visibility = Visibility.Collapsed;
            Categories.Visibility = Visibility.Collapsed;
            PersonalInfo.Visibility = Visibility.Collapsed;
            Cart.Visibility = Visibility.Collapsed;
            OrderHistory.Visibility = Visibility.Collapsed;

            this.DataContext = SessionData.Username;
            SessionData.UsernameChangedEvent += this.HandleUsernameChanged;
        }


        private void HandleUsernameChanged(string username)
        {
            if (username != null)
            {
                Greeting.Content = $"Вітаємо, {username}";
                Username.Text = username;

                LoginLabel.Content = "Вийти";
                LoginLabel.Foreground = Brushes.Red;

                PersonalInfo.Visibility = Visibility.Visible;
                Cart.Visibility = Visibility.Visible;
                OrderHistory.Visibility = Visibility.Visible;

                if (SessionData.IsAdmin == true)
                {
                    OrderHistory.Visibility = Visibility.Collapsed;
                    Cart.Visibility = Visibility.Collapsed;

                    AddCategory.Visibility = Visibility.Visible;
                    AddGood.Visibility = Visibility.Visible;
                    Categories.Visibility = Visibility.Visible;
                }
                Fetch_Data();
                return;
            }

            Goods.IsSelected = true;

            OrderHistory.Visibility = Visibility.Visible;
            Cart.Visibility = Visibility.Visible;

            AddCategory.Visibility = Visibility.Collapsed;
            AddGood.Visibility = Visibility.Collapsed;
            Categories.Visibility = Visibility.Collapsed;
            PersonalInfo.Visibility = Visibility.Collapsed;
            Cart.Visibility = Visibility.Collapsed;
            OrderHistory.Visibility = Visibility.Collapsed;


            Username.Text = null;
            Greeting.Content = null;

            LoginLabel.Content = "Увійти";
            LoginLabel.Foreground = new SolidColorBrush(Color.FromRgb(76, 175, 80));
        }
        private void TabControl_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void LoginLabel_MouseEnter(object sender, MouseEventArgs e)
        {
            Label loginLabel = (Label)sender;
            loginLabel.FontWeight = FontWeights.SemiBold;
        }

        private void LoginLabel_MouseLeave(object sender, MouseEventArgs e)
        {
            Label loginLabel = (Label)sender;
            loginLabel.FontWeight = FontWeights.Normal;
        }

        private void LoginButton_Click(object sender, RoutedEventArgs e)
        {
            if (SessionData.Username == null)
            {
                LoginWindow loginWindow = new LoginWindow();
                loginWindow.ShowDialog();
            }
            else
            {
                SessionData.Username = null;
                SessionData.Password = null;
            }
        }

        private void AddGoodButton_Click(object sender, RoutedEventArgs e)
        {
            GoodWindow goodWindow = new GoodWindow();
            goodWindow.ShowDialog();
        }
        private void AddCategoryButton_Click(object sender, RoutedEventArgs e)
        {
            CategoryWindow categoryWindow = new CategoryWindow();
            categoryWindow.ShowDialog();
        }
        private void goodsList_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            
        }
        private void ComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ComboBoxItem selectedItem = (ComboBoxItem)orderByBox.SelectedItem;
            try
            {
                orderBy = selectedItem.Content.ToString().Replace("System.Windows.Controls.Label: ", "");
                goodsList.ItemsSource = _goodsService.GetGoods(null, orderBy);
                //InitializeComponent();
            }
            catch (Exception)
            {
                //object still loading
            }
        }

        private async void ToCart_Button(object sender, RoutedEventArgs e)
        {
            var sndr = sender as System.Windows.Controls.Button;
            var goodItem = sndr.DataContext as GoodDto;

            await _orderService.AddGoodsToOrdered(goodItem.ID, 1, SessionData.ID);

            Fetch_Data();
        }

        public void Fetch_Data(int? categoryId = null)
        {
            //для фільтрування за категорією в параметри передаєм id категорії, в майбутньому за потреби зроблю колекцію айдішок
            //щоб отримати всі продукти передаємо null
            goodsList.ItemsSource = _goodsService.GetGoods(categoryId, orderBy);

            categoriesList.ItemsSource = _categoryService.GetCategories();

            if (SessionData.ID > 0)
            {

                ordersList.ItemsSource = new ObservableCollection<CartDto> { _orderService.GetCart(SessionData.ID) };

                cartedGoods.ItemsSource = _orderService.GetAllOrderedGoodsByBuyerId(SessionData.ID);
            }
        }
    }

    public class CartedGoodDto
    {
        public int Amount { get; set; }
        public  Goods Goods { get; set; }
    }
}
