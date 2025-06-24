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
    /// Логика взаимодействия для PageEdit.xaml
    /// </summary>
    public partial class PageEdit : Page
    {
        Service sr = new Service();
        private Service currentService;
        private string selectedImagePath;
        public string seldnull = "";
        public PageEdit()
        {
            InitializeComponent();
            
        }

        private void btnEdit_Click(object sender, RoutedEventArgs e)
        {
            List<string> errors = new List<string>();

            if (string.IsNullOrWhiteSpace(tbName.Text))
                errors.Add("Услуга не может быть пустой");

            if (!int.TryParse(tbCost.Text, out int cost) || cost <= 0)
                errors.Add("Цена должна быть положительным числом");

            if (!int.TryParse(tbSecond.Text, out int duration) || duration <= 0)
                errors.Add("Длительность должна быть положительным числом");

            if (errors.Count > 0)
            {
                MessageBox.Show(string.Join("\n", errors), "Ошибка ввода", MessageBoxButton.OK, MessageBoxImage.Error);
                return; // Прерываем выполнение, если есть ошибки
            }
            var existingService = helper.GetContext().Service
            .FirstOrDefault(s => s.Title.Equals(tbName.Text, StringComparison.OrdinalIgnoreCase));

            if (existingService != null && currentService == null)
            {
                MessageBox.Show("Услуга с таким названием уже существует.", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                return; // Прерываем выполнение, если услуга уже существует
            }

                // Создаем новую услугу
                Service newService = new Service
                {
                    Title = tbName.Text,
                    Cost = cost,
                    DurationInSeconds = duration,
                    Description = tbdescription.Text,
                    Discount = double.TryParse(tbDiscount.Text, out double newDiscount) ? newDiscount : 0,
                    MainImagePath = !string.IsNullOrEmpty(selectedImagePath) ? "/Услуги салона красоты/" + selectedImagePath : null
                };

                helper.GetContext().Service.Add(newService);
                helper.GetContext().SaveChanges();
                MessageBox.Show("Услуга успешно сохранена!", "Успех", MessageBoxButton.OK, MessageBoxImage.Information);
                NavigationService.Navigate(new PageGlav());
            



        }
    }
}
