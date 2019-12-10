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
        private string orderBy = "За алфавітом";

        public MainWindow()
        {
            InitializeComponent();

            //для фільтрування за категорією в параметри передаєм id категорії, в майбутньому за потреби зроблю колекцію айдішок
            //щоб отримати всі продукти передаємо null
            userGoodsList.ItemsSource = _goodsService.GetGoods(null, orderBy);
            adminGoodsList.ItemsSource = _goodsService.GetGoods(null, orderBy); 
            
            categoriesList.ItemsSource = _categoryService.GetCategories();

            ordersList.ItemsSource = new ObservableCollection<Order>
            {
                new Order{ID = 1,
                    Data = DateTime.Now,
                    TotalPrice = 1000,
                    PaymentStatus = true,
                    OrderedGood=new System.Collections.Generic.List<OrderedGood>(){
                        new OrderedGood(){
                            Goods = new Goods(4,"Rtx 2080ti", 1500, "rtx enabled"),
                            Amount = 2,
                            CurrentPrice = 2*1500
                        },
                        new OrderedGood(){
                            Goods = new Goods(5,"RX 580", 799, "simple radeon"),
                            Amount = 3,
                            CurrentPrice = 3*799
                        },
                },
            },
                new Order{ID = 1,
                    Data = DateTime.Now,
                    TotalPrice = 1000,
                    PaymentStatus = true,
                    OrderedGood=new System.Collections.Generic.List<OrderedGood>(){
                        new OrderedGood(){
                            Goods = new Goods(4,"Rtx 2080ti", 1500, "rtx enabled"),
                            Amount = 2,
                            CurrentPrice = 1500
                        },
                        new OrderedGood(){
                            Goods = new Goods(5,"RX 580", 799, "simple radeon"),
                            Amount = 3,
                            CurrentPrice = 799
                        },
                },
            },
                new Order{ID = 1,
                    Data = DateTime.Now,
                    TotalPrice = 1000,
                    PaymentStatus = true,
                    OrderedGood=new System.Collections.Generic.List<OrderedGood>(){
                        new OrderedGood(){
                            Goods = new Goods(4,"Rtx 2080ti", 1500, "rtx enabled"),
                            Amount = 2,
                            CurrentPrice = 1500
                        },
                        new OrderedGood(){
                            Goods = new Goods(5,"RX 580", 799, "simple radeon"),
                            Amount = 3,
                            CurrentPrice = 799
                        },
                },
            },
        };

            cartedGoods.ItemsSource = new ObservableCollection<CartedGoodDto>
            {
                new CartedGoodDto(){Amount = 2, Goods = new Goods(4,"Rtx 2080ti", 1500, "rtx enabled"), TotalSum = 2*1500},
                new CartedGoodDto(){Amount = 1, Goods = new Goods(5,"RX 580", 799, "simple radeon"), TotalSum = 2*799},
                new CartedGoodDto(){Amount = 3, Goods =  new Goods(6,"hyperx Alloy Fps", 190, "descent keyboard"), TotalSum = 2*190},
              new CartedGoodDto(){Amount = 2, Goods = new Goods(4,"Rtx 2080ti", 1500, "rtx enabled"), TotalSum = 2*1500},
                new CartedGoodDto(){Amount = 1, Goods = new Goods(5,"RX 580", 799, "simple radeon"), TotalSum = 2*799},
                new CartedGoodDto(){Amount = 3, Goods =  new Goods(6,"hyperx Alloy Fps", 190, "descent keyboard"), TotalSum = 2*190},
              new CartedGoodDto(){Amount = 2, Goods = new Goods(4,"Rtx 2080ti", 1500, "rtx enabled"), TotalSum = 2*1500},
                new CartedGoodDto(){Amount = 1, Goods = new Goods(5,"RX 580", 799, "simple radeon"), TotalSum = 2*799},
                new CartedGoodDto(){Amount = 3, Goods =  new Goods(6,"hyperx Alloy Fps", 190, "descent keyboard"), TotalSum = 2*190},
            };

            decimal totalOrderSum = 0.0M;

            foreach(CartedGoodDto cartedGoodDTO in cartedGoods.ItemsSource)
            {
                totalOrderSum += cartedGoodDTO.TotalSum;
            }

            TotalOrderSum.Content = totalOrderSum;

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

                if (username == "admin")
                {
                    OrderHistory.Visibility = Visibility.Collapsed;
                    Cart.Visibility = Visibility.Collapsed;

                    AddCategory.Visibility = Visibility.Visible;
                    AddGood.Visibility = Visibility.Visible;
                    Categories.Visibility = Visibility.Visible;
                    
                    adminGoodsList.Visibility = Visibility.Visible;
                    userGoodsList.Visibility = Visibility.Collapsed;
                }
                
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
        
        private void GoodAddToCartButton_Click(object sender, RoutedEventArgs e)
        {
        
        }

        private void GoodDeleteButton_Click(object sender, RoutedEventArgs e)
        {
        
        }
        
        private void ConfirmOrderButton_Click(object sender, RoutedEventArgs e)
        {
        
        } 
        private void CartedGoodDeleteButton_Click(object sender, RoutedEventArgs e)
        {
        
        }
        private void SearchButton_Click(object sender, RoutedEventArgs e)
        {
            Console.WriteLine(SearchBox.Text);
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
            GoodWindow goodWindow = new GoodWindow();
            goodWindow.ShowDialog();
        }
        private void AddCategoryButton_Click(object sender, RoutedEventArgs e)
        {
            CategoryWindow categoryWindow = new CategoryWindow();
            categoryWindow.ShowDialog();
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
    }

    public class CartedGoodDto
    {
        public int Amount { get; set; }
        public Goods Goods { get; set; }
        public decimal TotalSum {get;set;}
    }
}
