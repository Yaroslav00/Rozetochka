using System;
using DataAccess;
using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Controls;
using Business.Interfaces;
using Business.Services;
using DataAccess.Dto;

namespace Rozetochka
{
    /// <summary>
    /// Interaction logic for LoginWindow.xaml
    /// </summary>
    public partial class GoodWindow : Window
    {
        private readonly IGoodsService _goodsService;
        private readonly ICategoryService _categoryService;
        private readonly MainWindow.Fetch _fetch;


        private int selectedCatId = 0;

        public GoodWindow(MainWindow.Fetch fetch)
        {
            _goodsService = new GoodsService();
            _categoryService = new CategoryService();
            _fetch = fetch;

            InitializeComponent();

            CategorySelect.ItemsSource = _categoryService.GetCategories();
        }

        private async void Button_Click(object sender, RoutedEventArgs e)
        {
            await _goodsService.AddGood(selectedCatId, GoodName.Text, Description.Text, (decimal) GoodPrice.Value);
            _fetch();
            this.Close();
        }

        private void CategorySelect_SelectionChanged(object sender, System.Windows.Controls.SelectionChangedEventArgs e)
        {
            var comboBox = sender as ComboBox;
            var item = comboBox.SelectedItem as CategoryDto;
            selectedCatId = item.ID;
        }
    }
}
