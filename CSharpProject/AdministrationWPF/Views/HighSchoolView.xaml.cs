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

        private async void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            //Console.WriteLine(this.DataContext);
            await (this.DataContext as HighSchoolViewModel).CreateHighschoolsAsync();
        }

        private async void DeleteSchool(object sender, RoutedEventArgs e)
        {
            Button btn = sender as Button;
            HighSchoolItem item = btn.DataContext as HighSchoolItem;
            await (this.DataContext as HighSchoolViewModel).DeleteSchool(item);
        }
    }
}
