using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdministrationWPF.Models
{
    internal class HighSchool
    {
        public List<StudyProgram> StudyProgramList { get; set; }
        public HighSchool() 
        {
            StudyProgramList = new List<StudyProgram>();
        }
    }
}
