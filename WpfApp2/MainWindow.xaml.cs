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
using WpfApp2.Pages;
using static WpfApp2.MainWindow;
using static WpfApp2.Pages.PageGlav;

namespace WpfApp2
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            frame.Content = new Pages.PageGlav();
        }
        public class helper 
        {
            public static SalonKrasotiEntities ent;
            public static SalonKrasotiEntities GetContext()
            {
                if (ent == null)
                {
                    ent = new SalonKrasotiEntities();

                }
                return ent;
            }
        }

        private void btnBack_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (frame.CanGoBack) // Проверяем, можно ли вернуться назад
                {
                    frame.GoBack();
                }
                else
                {
                    // Можно добавить дополнительную логику, например, показать сообщение пользователю
                    MessageBox.Show("Нет предыдущей страницы для возврата.");
                }
            }
            catch (InvalidOperationException ex)
            {
                // Обработка исключения, если оно все же возникло
                MessageBox.Show($"Произошла ошибка: {ex.Message}");
            }
        }
    }
}
