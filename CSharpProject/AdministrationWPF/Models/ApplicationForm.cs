using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdministrationWPF.Models
{
    internal class ApplicationForm
    {
        public Student Student { get; }
        public List<StudyProgram> StudyPrograms { get; }
        public ApplicationForm(Student student)
        {
            Student = student;
            StudyPrograms = new List<StudyProgram>();
        }
    }
}
