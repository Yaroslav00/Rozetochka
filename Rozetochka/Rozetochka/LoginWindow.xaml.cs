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
            SessionData.Username = Username.Text;
            SessionData.Password = Password.Password;
            this.Close();
        }

        private void RegisterButton_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
            RegisterWindow registerWindow = new RegisterWindow();
            registerWindow.ShowDialog();
        }
    }
}
