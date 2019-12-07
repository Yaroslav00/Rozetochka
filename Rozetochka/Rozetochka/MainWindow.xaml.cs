using DataAccess;
using System;
using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;

namespace Rozetochka
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private ObservableCollection<Goods> goods { get; set; }

        public MainWindow()
        {
            InitializeComponent();

            goodsList.ItemsSource = new ObservableCollection<Goods>
            {
                new Goods(1,"iPhone X", 799, "expensive as shit"){ Category=new Category("Phones")},
                new Goods(2,"Macbook Pro 2019", 2199, "macbook"){ Category=new Category("Laptops")},
                new Goods(3,"One plus 7 pro", 699, "oneplus"){ Category=new Category("Phones")},
                new Goods(4,"Rtx 2080ti", 1500, "rtx enabled"){ Category=new Category("GPUs")},
                new Goods(5,"RX 580", 799, "simple radeon"){ Category=new Category("GPUs")},
                new Goods(6,"hyperx Alloy Fps", 190, "descent keyboard"){ Category=new Category("Perifirals")},
            };

            categoriesList.ItemsSource = new ObservableCollection<Category>
            {
                new Category("Phones"),
                new Category("GPUs"),
                new Category("Monitors"),
                new Category("Perifirals"),
                new Category("Laptops"),
                new Category("Laptops"),
                new Category("Laptops"),
            };

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

            cartedGoods.ItemsSource = new ObservableCollection<CartedGoodDTO>
            {
                new CartedGoodDTO(){Amount = 2, Goods = new Goods(4,"Rtx 2080ti", 1500, "rtx enabled")},
                new CartedGoodDTO(){Amount = 1, Goods = new Goods(5,"RX 580", 799, "simple radeon")},
                new CartedGoodDTO(){Amount = 3, Goods =  new Goods(6,"hyperx Alloy Fps", 190, "descent keyboard")},
                new CartedGoodDTO(){Amount = 2, Goods = new Goods(4,"Rtx 2080ti", 1500, "rtx enabled")},
                new CartedGoodDTO(){Amount = 1, Goods = new Goods(5,"RX 580", 799, "simple radeon")},
                new CartedGoodDTO(){Amount = 3, Goods =  new Goods(6,"hyperx Alloy Fps", 190, "descent keyboard")},
                new CartedGoodDTO(){Amount = 2, Goods = new Goods(4,"Rtx 2080ti", 1500, "rtx enabled")},
                new CartedGoodDTO(){Amount = 1, Goods = new Goods(5,"RX 580", 799, "simple radeon")},
                new CartedGoodDTO(){Amount = 3, Goods =  new Goods(6,"hyperx Alloy Fps", 190, "descent keyboard")},
            };

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

        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
