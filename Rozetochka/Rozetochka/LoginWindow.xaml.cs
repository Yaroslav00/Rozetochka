using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;
using System.Windows;
using Business.Interfaces;
using Business.Services;
using DataAccess.Dto;

namespace Rozetochka
{
    /// <summary>
    /// Interaction logic for LoginWindow.xaml
    /// </summary>
    public partial class LoginWindow : Window
    {
        private IUserService _userService;

        public LoginWindow()
        {
            _userService = new UserService();
            InitializeComponent();
        }

        private async void Button_Click(object sender, RoutedEventArgs e)
        {
            if (!await AreUserCredentialsValid(Username.Text, Password.Password))
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

        private async Task<bool> AreUserCredentialsValid(string username, string password)
        {
            if (!string.IsNullOrEmpty(username) && !string.IsNullOrEmpty(password))
            {
                var loginedUser = await _userService.Login(Username.Text, Password.Password);

                if (loginedUser != UserDto.ErrorUser)
                {
                    SessionData.Password = Password.Password;
                    SessionData.ID = loginedUser.ID;
                    SessionData.IsAdmin = loginedUser.IsAdmin;
                    SessionData.Username = Username.Text;
                    this.Close();
                    return true;
                }
                return false;
            }
            return false;
        }
    }
}
