using System.Windows;

namespace Rozetochka
{
    /// <summary>
    /// Interaction logic for LoginWindow.xaml
    /// </summary>
    public partial class LoginWindow : Window
    {
        public LoginWindow()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            if (AreUserCredentialsValid(Username.Text, Password.Password))
            {
                SessionData.Username = Username.Text;
                SessionData.Password = Password.Password;
                this.Close();
            }
            else
            {
                MessageBox.Show("Невірний логін або пароль",
                    "Помилка авторизації",
                    MessageBoxButton.OK,
                    MessageBoxImage.Error);
            }
        }

        private void RegisterButton_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
            RegisterWindow registerWindow = new RegisterWindow();
            registerWindow.ShowDialog();
        }

        private static bool AreUserCredentialsValid(string username, string password)
        {
            return (username.Equals("admin") && password.Equals("admin")) ||
                   (username.Equals("user") && password.Equals("pass"));
        }
    }
}
