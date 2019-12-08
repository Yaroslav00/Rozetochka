using System.Windows;
using Business.Interfaces;
using Business.Services;

namespace Rozetochka
{
    /// <summary>
    /// Interaction logic for LoginWindow.xaml
    /// </summary>
    
    public partial class CategoryWindow : Window
    {
        private MainWindow.Fetch _fetch;
        private readonly ICategoryService _categoryService;
        public CategoryWindow(MainWindow.Fetch fetch)
        {
            _fetch = fetch;
            _categoryService = new CategoryService();

            InitializeComponent();
        }

        private async void Button_Click(object sender, RoutedEventArgs e)
        {
            await _categoryService.AddCategory(GoodName.Text);
            _fetch();
            this.Close();
        }
    }
}
