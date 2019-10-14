using System.Collections.ObjectModel;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using DataAccess;
using Business.Interfaces;
using Business.Services;

namespace Rozetochka
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private bool isUserAdmin = true;
        private ObservableCollection<Goods> goods { get; set; }

        private readonly ICategoryService _categoryService = new CategoryService();
        private readonly IGoodsService _goodsService = new GoodsService();

        public MainWindow()
        {
            InitializeComponent();

            //для фільтрування за категорією в параметри передаєм id категорії, в майбутньому за потреби зроблю колекцію айдішок
            //щоб отримати всі продукти передаємо null
            goodsList.ItemsSource = _goodsService.GetGoods(null); 

            categoriesList.ItemsSource = _categoryService.GetCategories();

            if (isUserAdmin)
            {
                OrderHistory.Visibility = Visibility.Collapsed;
                Cart.Visibility = Visibility.Collapsed;
            }
            else
            {
                Categories.Visibility = Visibility.Collapsed;
            }
        //    this.DataContext = SessionData.Username;
        //    SessionData.UsernameChangedEvent += this.HandleUsernameChanged;
        }


        private void HandleUsernameChanged(string username)
        {
            if (username != null)
            {
                Greeting.Content = $"Вітаємо, {username}";

                LoginLabel.Content = "Вийти";
                LoginLabel.Foreground = Brushes.Red;
                return;
            }

                Greeting.Content = null;

                LoginLabel.Content = "Увійти";
                LoginLabel.Foreground = new SolidColorBrush(Color.FromRgb(76, 175, 80));
        }
        private void TabControl_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void LoginLabel_MouseEnter(object sender, MouseEventArgs e)
        {
            Label loginLabel = (Label) sender;
            loginLabel.FontWeight = FontWeights.SemiBold;
        }

        private void LoginLabel_MouseLeave(object sender, MouseEventArgs e)
        {
            Label loginLabel = (Label)sender;
            loginLabel.FontWeight = FontWeights.Normal;
        }

        //private void LoginButton_Click(object sender, RoutedEventArgs e)
        //{
        //    if (SessionData.Username == null)
        //    {
        //        LoginWindow loginWindow = new LoginWindow();
        //        loginWindow.ShowDialog();
        //    }
        //    else
        //    {
        //        SessionData.Username = null;
        //        SessionData.Password = null;
        //    }
        //}

        private void goodsList_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void ComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }
    }
}
