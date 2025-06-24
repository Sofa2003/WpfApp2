using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using WpfApp2.Models;
using static WpfApp2.MainWindow;

namespace WpfApp2.Pages
{
    /// <summary>
    /// Логика взаимодействия для PageGlav.xaml
    /// </summary>
    public partial class PageGlav : Page
    {
        private List<Service> listspisokservice;
        public PageGlav()
        {
            InitializeComponent();
            listspisokservice = helper.GetContext().Service.ToList();
            SortBox.Items.Add("Все");
            SortBox.Items.Add("0 - 5%");
            SortBox.Items.Add("5 - 15%");
            //SortBox.Items.Add("15 - 30%");
            //SortBox.Items.Add("30 - 70%");
            //SortBox.Items.Add("70 - 100%");
            SortBox.SelectedIndex = 0;
            DiscountBox.Items.Add("Без сортировки");
            DiscountBox.Items.Add("По возрастанию");
            DiscountBox.Items.Add("По убыванию");
            DiscountBox.SelectedIndex = 0;
            Load();

        }
        public void Load()
        {
            var serviceQuery = listspisokservice.AsQueryable();
           
            
            switch(SortBox.SelectedItem.ToString())
            {
                case "0 - 5%":
                    serviceQuery = serviceQuery.Where(s => s.Discount == null || (s.Discount >= 0 && s.Discount < 5));
                    break;
                case "5 - 15%":
                    serviceQuery = serviceQuery.Where(s => s.Discount >= 5 && s.Discount < 15);
                    break;
                case "Все":
                default:
                    break;
            }
            string selectedDiscount = DiscountBox.SelectedItem?.ToString();
            switch (selectedDiscount)
            {
                case "По возрастанию":
                    serviceQuery = serviceQuery.OrderBy(s => s.Cost); // Сортировка по возрастанию
                    break;
                case "По убыванию":
                    serviceQuery = serviceQuery.OrderByDescending(s => s.Cost); // Сортировка по убыванию
                    break;
                case "Без сортировки":
                default:
                    break;
            }
            // Применяем изменения к ItemsSource
            var filteredServices = serviceQuery.ToList();
            // Обновляем ItemsSource
            serviceGrid.ItemsSource = filteredServices;
            foreach (var service in filteredServices)
            {
                if (string.IsNullOrEmpty(service.MainImagePath))
                {
                    service.MainImagePath = "Услуги салона красоты/96.png"; // Путь к изображению по умолчанию
                }
            }

        
        }

        private void SortBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            Load();
        }

        private void DiscountBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            Load();
        }

        private void btncreate_Click(object sender, RoutedEventArgs e)
        {
            PageEdit pageEdit = new PageEdit(); 
            NavigationService.Navigate(pageEdit);
        }
    }
}
