using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
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

namespace AyupovAutoService
{
    /// <summary>
    /// Логика взаимодействия для AddEditPage.xaml
    /// </summary>
    public partial class AddEditPage : Page
    {
        private Service _currentServise = new Service();
        public bool IsServiceExist=false;
       
        public AddEditPage(Service SelectedService)
        {
            InitializeComponent();
            if (SelectedService != null)
            {
                _currentServise = SelectedService;
                IsServiceExist=true;    
            }
            DataContext = _currentServise;
        }
        
        private void SaveButton_Click(object sender, RoutedEventArgs e)
        {
            var errors = new StringBuilder();
            if (string.IsNullOrWhiteSpace(_currentServise.Title)) errors.AppendLine("Укажите название услуги");
            if (_currentServise.Cost == 0) errors.AppendLine("Укажите стоимость услуги");

            if (_currentServise.Discount < 0)
                errors.AppendLine("Укажите скидку");
            if (_currentServise.Discount > 100)
                errors.AppendLine("Невозможно указать такую скидку");

            if (string.IsNullOrWhiteSpace(_currentServise.Discount.ToString()))
                _currentServise.Discount = 0;
            if (_currentServise.DurationInSeconds < 0 || _currentServise.DurationInSeconds > 240)
                errors.AppendLine("Невозможно указать такую длительность");
            if (string.IsNullOrWhiteSpace(_currentServise.DurationInSeconds.ToString()))
                _currentServise.DurationInSeconds = 0;

            if (errors.Length > 0)
            {
                MessageBox.Show(errors.ToString());
                return;
            }

            var allServices = Ayupov_ServiceEntities.GetContext().Service.ToList();
            allServices = allServices.Where(p => p.Title == _currentServise.Title).ToList();
            if (allServices.Count == 0 || IsServiceExist == true)
            {
                if (_currentServise.ID == 0)
                {
                    Ayupov_ServiceEntities.GetContext().Service.Add(_currentServise);
                }

                try
                {
                    Ayupov_ServiceEntities.GetContext().SaveChanges();
                    MessageBox.Show("Информация сохранена");
                    Manager.MainFrame.GoBack();
                }
                catch (Exception exception)
                {
                    MessageBox.Show(exception.Message);
                }
            }
            else
            {
                Ayupov_ServiceEntities.GetContext().SaveChanges();
                MessageBox.Show("Уже существует такая услуга");
            }
        }
    }
}