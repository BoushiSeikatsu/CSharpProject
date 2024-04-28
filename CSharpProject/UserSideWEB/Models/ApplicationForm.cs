using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UderSideWEB.Models
{
    public class ApplicationForm
    {
        public int ID { get;}
        public Student Student { get; }
        public List<StudyProgram> StudyPrograms { get; }
        public ApplicationForm(Student student)
        {
            Student = student;
            StudyPrograms = new List<StudyProgram>();
        }
    }
}
