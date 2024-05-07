using AdministrationWPF.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdministrationWPF.Models
{
    public class HighSchoolItem : ObservableObject
    {
        private string _name;
        private string _street;
        private string _city;
        private string _psc;

        public long Id_school { get; set; }
        public string Name { get => _name; set 
            {
                _name = value;
                OnPropertyChanged();
            }
        }
        public string Street { get => _street; set 
            {
                _street = value;
                OnPropertyChanged();
            }
        }
        public string City { get => _city; set 
            {
                _city = value;
                OnPropertyChanged();
            }
        }
        public string PSC { get => _psc; set 
            {
                _psc = value;
                OnPropertyChanged();
            }
        }
        public HighSchoolItem() { }
    }
}
