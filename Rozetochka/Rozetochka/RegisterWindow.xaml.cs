using System.Windows;

namespace Rozetochka
{
    /// <summary>
    /// Interaction logic for RegisterWindow.xaml
    /// </summary>
    public partial class RegisterWindow : Window
    {
        public RegisterWindow()
        {
            InitializeComponent();
        }

        private void RegisterButton_Click(object sender, RoutedEventArgs e)
        {
            if (AreUserCredentialsValid(Password.Password, PasswordRepeat.Password))
            {
                MessageBox.Show($"Ви успішно зареєструвалися. Вітаємо у нашій Розеточці, {Username.Text}!",
                    "Вітаємо!",
                    MessageBoxButton.OK);
            }
            else
            {
                MessageBox.Show("Паролі повинні збігатися",
                    "Помилка реєстрації",
                    MessageBoxButton.OK,
                    MessageBoxImage.Error);
            }
        }
        
        private static bool AreUserCredentialsValid(string password, string passwordRepeat)
        {
            return (password.Equals(passwordRepeat));
        }
    }
}
