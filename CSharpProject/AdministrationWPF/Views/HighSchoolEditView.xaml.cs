using AdministrationWPF.Models;
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
using System.Windows.Shapes;

namespace AdministrationWPF.Views
{
    /// <summary>
    /// Interaction logic for HighSchoolEditView.xaml
    /// </summary>
    public partial class HighSchoolEditView : Window
    {
        public HighSchoolEditView(HighSchoolItem item)
        {
            this.DataContext = item;
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
