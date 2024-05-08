
using UderSideWEB.Models;
using UserSideWEB.Models;

namespace UserSideWEB.Services
{
    public class DatabaseService
    {
        private readonly DataLayer.DataLayer _dataLayer;

        public DatabaseService()
        {
            _dataLayer = new DataLayer.DataLayer();
        }

        public async Task<bool> IsValidSchoolNameAsync(string schoolName)
        {
            List<HighSchool> highSchools = await _dataLayer.GetAllHighSchoolsAsync();
            foreach(var highSchool in highSchools)
            {
                if(highSchool.Name ==  schoolName)
                {
                    return true;
                }
            }
            return false;
        }
        public async Task<bool> InsertStudentAsync(string id_card, string firstName, string lastName, string email, string street, string city, string psc)
        {
            Student s = new Student(id_card, firstName, lastName, email, street, city, psc);
            if( ! await _dataLayer.StudentExists(id_card))
            {
                await _dataLayer.InsertStudentAsync(s);
                return true;
            }
            return false;
        }
        public async Task<long> InsertApplicationFormAsync(string id_card, DateTime dateTime)
        {
            ApplicationForm form = new ApplicationForm();
            form.Id_student = id_card;
            form.Date_submit = dateTime;
            await _dataLayer.InsertApplicationFormAsync(form);
            //Newest applicationForm is inserted to the end of records, therefore is last
            List<ApplicationForm> applicationForms = await _dataLayer.GetAllApplicationFormsAsync();
            return applicationForms.Last().Id_form;
        }
        public async Task ConnectProgramsToApplicationFormAsync(long id_form, string schoolName, string programName)
        {
            HighSchool school = await _dataLayer.GetHighSchoolByNameAsync(schoolName);
            List<StudyProgram> programs = await _dataLayer.GetAllStudyProgramsForSchoolAsync(school);
            long id_prog = 0;
            foreach(StudyProgram program in programs)
            {
                if(program.Name == programName)
                {
                    id_prog = program.Id_program;
                    break;
                }
            }
            ProgramApplication programApplication = new ProgramApplication();
            programApplication.Id_form = id_form;
            programApplication.Id_program = id_prog;
            await _dataLayer.InsertProgramApplicationAsync(programApplication);
        }
    }
}
