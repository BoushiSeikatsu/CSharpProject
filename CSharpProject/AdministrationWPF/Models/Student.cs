using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdministrationWPF.Models
{
    internal class Student
    {
        public string ID_Card { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string Street { get; set; }
        public string City { get; set; }
        public string PSC { get; set; }
        public ApplicationForm? Form {  get; set; }
    }
}
