using DataAccess;
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
        private bool isUserAdmin = true;
        private ObservableCollection<Goods> goods { get; set; }

        public MainWindow()
        {
            InitializeComponent();

            goodsList.ItemsSource = new ObservableCollection<Goods>
            {
                new Goods(1,"iPhone X", 799, "expensive as shit"),
                new Goods(2,"Macbook Pro 2019", 2199, "macbook"),
                new Goods(3,"One plus 7 pro", 699, "oneplus"),
                new Goods(4,"Rtx 2080ti", 1500, "rtx enabled"),
                new Goods(5,"RX 580", 799, "simple radeon"),
                new Goods(6,"hyperx Alloy Fps", 190, "descent keyboard")
            };

            categoriesList.ItemsSource = new ObservableCollection<Category>
            {
                new Category("Phones"),
                new Category("GPUs"),
                new Category("Monitors"),
                new Category("Perifirals"),
                new Category("Laptops"),
            };

            if (isUserAdmin)
            {
                OrderHistory.Visibility = Visibility.Collapsed;
                Cart.Visibility = Visibility.Collapsed;
            }
            else
            {
                Categories.Visibility = Visibility.Collapsed;
            }
            this.DataContext = SessionData.Username;
            SessionData.UsernameChangedEvent += this.HandleUsernameChanged;
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

        private void goodsList_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void ComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }
    }
}
