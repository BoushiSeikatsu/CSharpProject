using AdministrationWPF.Models;
using AdministrationWPF.ViewModels;
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

namespace AdministrationWPF.Views
{
    /// <summary>
    /// Interaction logic for ApplicationFormView.xaml
    /// </summary>
    public partial class ApplicationFormView : UserControl
    {
        public ApplicationFormView()
        {

            InitializeComponent();
        }

        private async void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            //If application forms werent loaded yet
            if((this.DataContext as ApplicationFormViewModel).Applications.Count == 0)
            {
                await (this.DataContext as ApplicationFormViewModel).CreateApplicationsAsync();
            }
        }

        private async void DeleteApplicationForm(object sender, RoutedEventArgs e)
        {
            Button btn = sender as Button;
            ApplicationFormItem item = btn.DataContext as ApplicationFormItem;
            await (this.DataContext as ApplicationFormViewModel).DeleteApplicationForm(item);
        }

        private async void AddApplicationForm(object sender, RoutedEventArgs e)
        {
            ApplicationFormItem item = new ApplicationFormItem();
            ApplicationFormEditView form = new ApplicationFormEditView(item);
            form.ShowDialog();
            await (this.DataContext as ApplicationFormViewModel).InsertApplicationForm(item);
        }

        private async void EditApplicationForm(object sender, RoutedEventArgs e)
        {
            Button btn = sender as Button;
            ApplicationFormItem item = btn.DataContext as ApplicationFormItem;
            ApplicationFormEditView form = new ApplicationFormEditView(item);
            form.ShowDialog();
            await (this.DataContext as ApplicationFormViewModel).UpdateApplicationForm(item);
        }
    }
}
