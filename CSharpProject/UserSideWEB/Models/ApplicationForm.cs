using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UderSideWEB.Models
{
    public class ApplicationForm
    {
        public long Id_form { get; set; }
        public string Id_student { get; set; }
        public DateTime Date_submit {  get; set; }
        public ApplicationForm()
        {
        }
    }
}
