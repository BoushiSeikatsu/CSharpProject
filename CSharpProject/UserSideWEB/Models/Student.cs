using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UderSideWEB.Models
{
    public class Student
    {
        public string Id_card { get; set; }
        public string First_name { get; set; }
        public string Last_name { get; set; }
        public string Email { get; set; }
        public string Street { get; set; }
        public string City { get; set; }
        public string PSC { get; set; }
        //public ApplicationForm? Form {  get; set; }
        public Student()
        {
            
        }
        public Student(string id_card, string first_name, string last_name, string email, string street, string city, string pSC)
        {
            Id_card = id_card;
            First_name = first_name;
            Last_name = last_name;
            Email = email;
            Street = street;
            City = city;
            PSC = pSC;
        }
    }
}
