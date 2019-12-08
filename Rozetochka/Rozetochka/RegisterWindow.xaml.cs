using System.Threading.Tasks;
using System.Windows;
using Business.Interfaces;
using Business.Services;
using DataAccess.Dto;

namespace Rozetochka
{
    /// <summary>
    /// Interaction logic for RegisterWindow.xaml
    /// </summary>
    public partial class RegisterWindow : Window
    {
        private readonly IUserService _userService;
        public RegisterWindow()
        {
            _userService = new UserService();
            InitializeComponent();
        }

        private async void RegisterButton_Click(object sender, RoutedEventArgs e)
        {

            if (await AreUserCredentialsValid(Password.Password, PasswordRepeat.Password, Username.Text))
            {
                MessageBox.Show($"Ви успішно зареєструвалися. Вітаємо у нашій Розеточці, {Username.Text}!",
                    "Вітаємо!",
                    MessageBoxButton.OK);
                this.Close();
            }
            else
            {
                MessageBox.Show("Помилка реєстрації",
                    "Помилка реєстрації",
                    MessageBoxButton.OK,
                    MessageBoxImage.Error);
            }
        }
        
        private async Task<bool> AreUserCredentialsValid(string password, string passwordRepeat, string username)
        {
            if (password.Equals(passwordRepeat) && !string.IsNullOrEmpty(username))
            {
                var registeredUser = await _userService.Register(Username.Text, Password.Password);
                if (registeredUser != UserDto.ErrorUser)
                {
                    SessionData.Password = Password.Password;
                    SessionData.ID = registeredUser.ID;
                    SessionData.IsAdmin = registeredUser.IsAdmin;
                    SessionData.Username = registeredUser.UserName;
                    return true;
                }
                return false;
            }
            return false;
        }
    }
}
