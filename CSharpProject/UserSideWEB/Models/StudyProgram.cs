using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UderSideWEB.Models
{
    public class StudyProgram
    {
        public long Id_program { get; set; }
        public long Id_school { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public long Capacity { get; set; }
        //public List<ApplicationForm> Forms { get; set; }

        public StudyProgram()
        {
            //Forms = new List<ApplicationForm>();
        }
    }
}
