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
    /// Interaction logic for HighSchoolView.xaml
    /// </summary>
    public partial class HighSchoolView : UserControl
    {
        public HighSchoolView()
        {
            InitializeComponent();
        }
        //Když return type byl Task, tak to vracelo error, že to má špatný error type
        private async void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            //Console.WriteLine(this.DataContext);
            //If highschools werent loaded yet
            if((this.DataContext as HighSchoolViewModel).HighSchools.Count == 0)
            {
                await (this.DataContext as HighSchoolViewModel).CreateHighschoolsAsync();
            }
        }

        private async void DeleteSchool(object sender, RoutedEventArgs e)
        {
            Button btn = sender as Button;
            HighSchoolItem item = btn.DataContext as HighSchoolItem;
            await (this.DataContext as HighSchoolViewModel).DeleteSchool(item);
        }

        private void EditSchool(object sender, RoutedEventArgs e)
        {
            Button btn = sender as Button;
            HighSchoolItem item = btn.DataContext as HighSchoolItem;
            HighSchoolEditView form = new HighSchoolEditView(item);
            form.ShowDialog();

        }

        private void AddSchool(object sender, RoutedEventArgs e)
        {
            HighSchoolItem item = new HighSchoolItem();
            HighSchoolEditView form = new HighSchoolEditView(item);
            form.ShowDialog();
            (this.DataContext as HighSchoolViewModel).InsertSchool(item);
        }
    }
}
