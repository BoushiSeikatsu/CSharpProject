using AdministrationWPF.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UderSideWEB.Models;

namespace AdministrationWPF.Models
{
    public class StudyProgramItem : ObservableObject
    {
        private string _name;
        private string _description;
        private long _capacity;

        public long Id_program { get; set; }
        public HighSchool School {  get; set; }
        public string Name { get => _name; 
            set
            {
                _name = value;
                OnPropertyChanged();
            }
        }
        public string Description { get => _description; set 
            {
                _description = value;
                OnPropertyChanged();
            }
        }
        public long Capacity { get => _capacity; set 
            {
                _capacity = value;
                OnPropertyChanged();
            }
        }
    }
}
