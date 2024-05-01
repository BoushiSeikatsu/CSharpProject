using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UderSideWEB.Models
{
    public class HighSchool
    {
        public long Id_school { get; set; }
        public string Name { get; set; }
        public string Street { get; set; }
        public string City { get; set; }
        public string PSC { get; set; }
        //public List<StudyProgram> StudyProgramList { get; set; }
        public HighSchool() 
        {
            //StudyProgramList = new List<StudyProgram>();
        }
    }
}
