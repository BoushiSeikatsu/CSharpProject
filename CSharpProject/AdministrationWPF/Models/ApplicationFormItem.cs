using AdministrationWPF.Core;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UderSideWEB.Models;

namespace AdministrationWPF.Models
{
    public class ApplicationFormItem : ObservableObject
    {
        private DateTime _date_submit;
        private Student _student;
        public long Id_form { get; set; }
        public DateTime Date_submit { get => _date_submit; 
            set 
            {
                _date_submit = value;
                OnPropertyChanged();
            }
        }
        public Student Student { get => _student; 
            set 
            {
                _student = value;
                OnPropertyChanged();
            }
        }
        public ObservableCollection<StudyProgramItem> StudyPrograms { get; set; }

        public ApplicationFormItem()
        {
            _student = new Student();
            StudyPrograms = new ObservableCollection<StudyProgramItem>();
        }
        public ApplicationFormItem(long id_form, DateTime date_submit, Student student, ObservableCollection<StudyProgramItem> studyPrograms)
        {
            Id_form = id_form;
            Date_submit = date_submit;
            Student = student;
            StudyPrograms = studyPrograms;
        }
    }
}
