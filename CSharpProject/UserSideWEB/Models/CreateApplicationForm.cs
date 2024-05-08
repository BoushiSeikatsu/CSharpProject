using System.ComponentModel.DataAnnotations;

namespace UserSideWEB.Models
{
    public class CreateApplicationForm
    {
        [Display(Name = "Číslo občanského průkazu")]
        [Required]
        public string id_card { get; set; }
        [Display(Name = "Křestní jméno")]
        [Required]
        public string FirstName { get; set; }
        [Display(Name = "Rodné jméno")]
        [Required]
        public string LastName { get; set; }
        [EmailAddress]
        [Required]
        public string Email { get; set; }
        [Display(Name = "Ulice")]
        [Required]
        public string Street { get; set; }
        [Display(Name = "Město")]
        [Required]
        public string City { get; set; }
        [Display(Name = "PSČ")]
        [Required]
        public string PSC { get; set; }
        [Display(Name = "Název školy")]
        [Required]
        public string FirstSchool { get; set; }
        [Display(Name = "První studijní program")]
        [Required]
        public string FirstProgram { get; set; }
        [Display(Name = "Název školy")]
        public string? SecondSchool { get; set; }
        [Display(Name = "Druhý studijní program")]
        public string? SecondProgram { get; set; }
        [Display(Name = "Název školy")]
        public string? ThirdSchool { get; set; }
        [Display(Name = "Třetí studijní program")]
        public string? ThirdProgram {  get; set; }
    }
}
