using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using System.Text.RegularExpressions;
using UserSideWEB.Models;
using UserSideWEB.Services;

namespace UserSideWEB.Controllers
{
    public class HomeController : BaseController
    {
        private readonly ILogger<HomeController> _logger;
        private readonly HintService _hintService;
        private readonly DatabaseService _databaseService;

        public HomeController(ILogger<HomeController> logger, HintService hintService, DatabaseService databaseService)
        {
            _logger = logger;
            _hintService = hintService;
            _databaseService = databaseService;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Form(CreateApplicationForm form)
        {
            if(ModelState.IsValid)
            {
                //Check if id_card length isnt 9 or it containst something else than digits
                if(form.id_card.Length != 9 || form.id_card.Any( x => !char.IsDigit(x)))
                {
                    return View();
                }
                //Name contains something thats not a letter?
                if(form.FirstName.Any(x => !char.IsLetter(x)) || form.LastName.Any(x => !char.IsLetter(x)))
                {
                    return View();
                }
                //Mozna osetrit email?
                //Street check
                string regexString = @"\p{L}* [0-9]*";
                Regex r = new Regex(regexString);
                var match = r.Match(form.Street);
                if(match.Value != form.Street)
                {
                    return View();
                }
                //City contains something thats not a letter?
                if(form.City.Any(x => !char.IsLetter(x)))
                {
                    return View();
                }
                //PSC check
                regexString = @"[0-9]{3} [0-9]{2}";
                r = new Regex(regexString);
                match = r.Match(form.PSC);
                if(match.Value != form.PSC)
                {
                    return View();
                }
                //Check if schools names are valid
                if(! await _databaseService.IsValidSchoolNameAsync(form.FirstSchool))
                {
                    return View();
                }
                if (form.SecondSchool != null && form.SecondProgram != null)
                {
                    if (!await _databaseService.IsValidSchoolNameAsync(form.SecondSchool))
                    {
                        return View();
                    }
                }
                if (form.ThirdSchool != null && form.ThirdProgram != null)
                {
                    if (!await _databaseService.IsValidSchoolNameAsync(form.ThirdSchool))
                    {
                        return View();
                    }
                }
                //If student already exists
                if (! await _databaseService.InsertStudentAsync(form.id_card, form.FirstName, form.LastName, form.Email, form.Street, form.City, form.PSC))
                {
                    return View();
                }
                long id_form = await _databaseService.InsertApplicationFormAsync(form.id_card, DateTime.Today);
                await _databaseService.ConnectProgramsToApplicationFormAsync(id_form, form.FirstSchool, form.FirstProgram);
                if(form.SecondSchool != null && form.SecondProgram != null)
                {
                    await _databaseService.ConnectProgramsToApplicationFormAsync(id_form, form.SecondSchool, form.SecondProgram);
                }
                if (form.ThirdSchool != null && form.ThirdProgram != null)
                {
                    await _databaseService.ConnectProgramsToApplicationFormAsync(id_form, form.ThirdSchool, form.ThirdProgram);
                }
                return RedirectToAction("Confirm");
            }
            return View();
        }
        public IActionResult Confirm()
        {
            return View();
        }
        public async Task<IActionResult> Form()
        {
            //var listOfHighSchools = await _dataLayer.GetAllStudyProgramsAsync();
            return View();
        }
        [HttpGet]
        public async Task<IActionResult> GetSchoolHint(string inputText)
        {
            List<string> hints = await _hintService.GetSchoolHints(inputText);
            string returnText = "";
            IEnumerable<string> reducedHints;
            if (hints.Count >= 3)
            {
                reducedHints = hints.Take(3);
            }
            else
            {
                reducedHints = hints;
            }
            foreach(string hint in reducedHints)
            {
                returnText += hint + " ";
            }
            return Json(new { text = returnText });
        }
        [HttpGet]
        public async Task<IActionResult> GetOptions(string schoolName)
        {
            var options = await _hintService.GetSchoolPrograms(schoolName);
            return Json(options);
        }
        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
