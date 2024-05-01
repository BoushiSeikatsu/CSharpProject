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
        [Display(Name = "Ulic")]
        [Required]
        public string Street { get; set; }
        [Display(Name = "Město")]
        [Required]
        public string City { get; set; }
        [Display(Name = "PSČ")]
        [Required]
        public string PSC { get; set; }
    }
}
