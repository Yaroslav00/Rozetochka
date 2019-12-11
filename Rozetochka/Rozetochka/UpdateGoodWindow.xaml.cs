using System;
using DataAccess;
using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Controls;
using Business.Interfaces;
using Business.Services;
using DataAccess.Dto;
using Microsoft.Win32;

namespace Rozetochka
{
    /// <summary>
    /// Interaction logic for LoginWindow.xaml
    /// </summary>
    public partial class UpdateGoodWindow : Window
    {
        private readonly IGoodsService _goodsService;
        private readonly ICategoryService _categoryService;
        private readonly MainWindow.Fetch _fetch;
        private int _goodId;
        private Uri uri = new Uri("/default.png", UriKind.Relative);
        private int selectedCatId = 0;

        public UpdateGoodWindow(MainWindow.Fetch fetch, int goodId)
        {
            _goodsService = new GoodsService();
            _categoryService = new CategoryService();
            _fetch = fetch;
            _goodId = goodId;
            InitializeComponent();

            CategorySelect.ItemsSource = _categoryService.GetCategories();
        }
        private void BtnLoadFromFile_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            if (openFileDialog.ShowDialog() == true)
            {
                uri = new Uri(openFileDialog.FileName);

            }
        }

        private async void Button_Click(object sender, RoutedEventArgs e)
        {
            await _goodsService.UpdateGood(_goodId,selectedCatId, GoodName.Text, Description.Text, (decimal) GoodPrice.Value, uri.ToString());
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
