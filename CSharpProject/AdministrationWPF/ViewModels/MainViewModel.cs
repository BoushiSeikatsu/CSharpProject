using AdministrationWPF.Core;
using AdministrationWPF.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdministrationWPF.ViewModels
{
    internal class MainViewModel : ViewModel
    {
        private INavigationService _navigation;

        public INavigationService Navigation 
        {
            get => _navigation;
            set 
            {
                _navigation = value;
                OnPropertyChanged();
            }
        }
        public RelayCommand NavigateApplicationFormCommand { get; set; }
        public RelayCommand NavigateHighSchoolCommand { get; set; }
        public MainViewModel(INavigationService navService)
        { 
            Navigation = navService;
            NavigateApplicationFormCommand = new RelayCommand(o => { Navigation.NavigateTo<ApplicationFormViewModel>(); }, o => true);
            NavigateHighSchoolCommand = new RelayCommand(o => { Navigation.NavigateTo<HighSchoolViewModel>(); }, o => true);
        }
    }
}
