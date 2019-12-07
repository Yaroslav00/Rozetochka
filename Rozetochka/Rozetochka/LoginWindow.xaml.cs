using System.ComponentModel.DataAnnotations;
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
            var loginedUser = await _userService.Login(Username.Text, Password.Password);

            if (loginedUser != UserDto.ErrorUser)
            {
                SessionData.Username = Username.Text;
                SessionData.Password = Password.Password;
                SessionData.ID = loginedUser.ID;
                SessionData.IsAdmin = loginedUser.IsAdmin;
                this.Close();
            }
            else
            {
                //хендлим невірні креди
            }
        }

        private void RegisterButton_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
            RegisterWindow registerWindow = new RegisterWindow();
            registerWindow.ShowDialog();
        }
    }
}
