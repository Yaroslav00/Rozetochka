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
            if (Password.Password == Password_Copy.Password && !string.IsNullOrEmpty(Username.Text))
            {
                var registeredUser = await _userService.Register(Username.Text, Password.Password);
                if (registeredUser != UserDto.ErrorUser)
                {
                    SessionData.Username = registeredUser.UserName;
                    SessionData.Password = Password.Password;
                    SessionData.ID = registeredUser.ID;
                    SessionData.IsAdmin = registeredUser.IsAdmin;
                    this.Close();
                }
                else
                {
                    //хендлим невірні креди
                }
            }
        }
    }
}
