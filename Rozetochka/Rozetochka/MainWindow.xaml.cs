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
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using DataAccess.Dto;
using System.Linq;

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
        private readonly IUserService _userService = new UserService();

        private string orderBy = "За алфавітом";

        public delegate void Fetch(int? category = null);

        private Fetch fetch_delegate;

        public MainWindow()
        {
            fetch_delegate = Fetch_Data;

            InitializeComponent();

            Fetch_Data();

            decimal totalOrderSum = 0.0M;

            //foreach(CartedGoodDto cartedGoodDTO in cartedGoods.ItemsSource)
            //{
            //    totalOrderSum += cartedGoodDTO.TotalSum;
            //}

            //TotalOrderSum.Content = totalOrderSum;

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

                    adminGoodsList.Visibility = Visibility.Visible;
                    userGoodsList.Visibility = Visibility.Collapsed;
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

            adminGoodsList.Visibility = Visibility.Collapsed;
            userGoodsList.Visibility = Visibility.Visible;

            Username.Text = null;
            Greeting.Content = null;

            LoginLabel.Content = "Увійти";
            LoginLabel.Foreground = new SolidColorBrush(Color.FromRgb(76, 175, 80));
        }

        private void TabControl_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void SearchButton_Click(object sender, RoutedEventArgs e)
        {
            userGoodsList.ItemsSource = _goodsService.GetGoods(null, orderBy).Where(x => x.Name.ToLower().Contains(SearchBox.Text.ToLower()));
            adminGoodsList.ItemsSource = _goodsService.GetGoods(null, orderBy).Where(x => x.Name.ToLower().Contains(SearchBox.Text.ToLower()));
        }
        private void LoginLabel_MouseEnter(object sender, MouseEventArgs e)
        {
            Label loginLabel = (Label) sender;
            loginLabel.FontWeight = FontWeights.SemiBold;
        }

        private void LoginLabel_MouseLeave(object sender, MouseEventArgs e)
        {
            Label loginLabel = (Label) sender;
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

        private void GoodAddToCartButton_Click(object sender, RoutedEventArgs e)
        {

        }

        private async void GoodDeleteButton_Click(object sender, RoutedEventArgs e)
        {
            var btn = sender as Button;
            var good = btn.DataContext as GoodDto;

            await _goodsService.DeleteGood(good.ID);

            Fetch_Data();
        }

        private async void ConfirmOrderButton_Click(object sender, RoutedEventArgs e)
        {
            if (SessionData.ID > 0)
            {
                await _orderService.Checkout(SessionData.ID);
                Fetch_Data();
            }
        }

    private void CartedGoodDeleteButton_Click(object sender, RoutedEventArgs e)
        {
        
        }
        private void PersonalInfoSaveButton_Click(object sender, RoutedEventArgs e)
        {
            if (AreUserCredentialsValid(Password.Password, PasswordRepeat.Password))
            {
                MessageBox.Show("Особисті дані успішно зміненно",
                    "Збережено",
                    MessageBoxButton.OK);
            }
            else
            {
                MessageBox.Show("Паролі повинні збігатися",
                    "Помилка редагування",
                    MessageBoxButton.OK,
                    MessageBoxImage.Error);
            }
        }
        private static bool AreUserCredentialsValid(string password, string passwordRepeat)
        {
            return (password.Equals(passwordRepeat));
        }
        private void AddGoodButton_Click(object sender, RoutedEventArgs e)
        {
            GoodWindow goodWindow = new GoodWindow(fetch_delegate);
            goodWindow.ShowDialog();
        }
        private void AddCategoryButton_Click(object sender, RoutedEventArgs e)
        {
            CategoryWindow categoryWindow = new CategoryWindow(fetch_delegate);
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
                userGoodsList.ItemsSource = _goodsService.GetGoods(null, orderBy);
                adminGoodsList.ItemsSource = _goodsService.GetGoods(null, orderBy);
                //InitializeComponent();
            }
            catch (Exception)
            {
                //object still loading
            }
        }

        private async void Delete_category(object sender, RoutedEventArgs e)
        {
            var btn = sender as Button;
            var category = btn.DataContext as CategoryDto;

            if (await _categoryService.DeleteCategory(category.ID))
            {
                Fetch_Data();
            }
            else
            {
                //handle category cant be deleted
            }
        }

        private async void DeleteFromCartButton(object sender, RoutedEventArgs e)
        
        {
            var btn = sender as Button;
            var item = btn.DataContext as OrderedGoodDto;

            await _orderService.DeleteGoodFromOrder(item.GoodsID, item.OrderID);
            
            Fetch_Data();
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
            adminGoodsList.ItemsSource = userGoodsList.ItemsSource = _goodsService.GetGoods(categoryId, orderBy);

            categoriesList.ItemsSource = _categoryService.GetCategories();

            if (SessionData.ID > 0)
            {

                ordersList.ItemsSource = _orderService.GetCart(SessionData.ID);

                cartedGoods.ItemsSource = _orderService.GetAllOrderedGoodsByBuyerId(SessionData.ID);

                TotalOrderSum.Content = _orderService.SumCart(cartedGoods.ItemsSource as List<OrderedGoodDto>);
            }
        }

        

        private async void Button_Click(object sender, RoutedEventArgs e)
        {
            if (Password.Password.Equals(PasswordRepeat.Password) && !string.IsNullOrEmpty(Username.Text)
                && SessionData.ID > 0)
            {
                await _userService.ChangeUserCredentials(SessionData.ID, Username.Text, Password.Password);
            }
        }
    }

    public class CartedGoodDto
    {
        public int Amount { get; set; }
        public Goods Goods { get; set; }
        public decimal TotalSum {get;set;}
    }
}
