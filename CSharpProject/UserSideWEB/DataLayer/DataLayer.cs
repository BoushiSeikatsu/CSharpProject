using UderSideWEB.Models;
using UserSideWEB.Models;

namespace UserSideWEB.DataLayer
{
    public class DataLayer
    {
        private ORM orm;
        public DataLayer(string dbSource = "Data Source=mydb.db")
        {
            orm = new ORM(dbSource);
        }
        public async Task<List<HighSchool>> GetAllHighSchoolsAsync()
        {
            return await orm.GetAll<HighSchool>();
        }

        public async Task<List<Student>> GetAllStudentsAsync()
        {
            return await orm.GetAll<Student>();
        }

        public async Task<List<StudyProgram>> GetAllStudyProgramsAsync()
        {
            return await orm.GetAll<StudyProgram>();
        }

        public async Task<List<ApplicationForm>> GetAllApplicationFormsAsync()
        {
            return await orm.GetAll<ApplicationForm>();
        }
        public async Task<List<ProgramApplication>> GetProgramApplicationsAsync()
        {
            return await orm.GetAll<ProgramApplication>();
        }
        public async Task<List<StudyProgram>> GetProgramsForSchoolAsync(long id_school)
        {
            List<StudyProgram> programs = await orm.GetAll<StudyProgram>();
            List<StudyProgram> result = new List<StudyProgram>();
            foreach(StudyProgram program in programs)
            {
                if(program.Id_school == id_school)
                {
                    result.Add(program);
                }
            }
            return result;
        }

        public async Task<List<StudyProgram>> GetProgramsForApplicationAsync(long id_form)
        {
            List<ProgramApplication> allJoins = await orm.GetAll<ProgramApplication>();
            List<ProgramApplication> appJoins = new List<ProgramApplication>();
            List<StudyProgram> programs = await orm.GetAll<StudyProgram>();
            List<StudyProgram> result = new List<StudyProgram>();
            foreach(ProgramApplication join in allJoins) 
            {
                if(join.Id_form == id_form)
                {
                    appJoins.Add(join);
                }
            }
            foreach(StudyProgram program in programs)
            {
                foreach(ProgramApplication join in appJoins)
                {
                    if(program.Id_program == join.Id_program)
                    {
                        result.Add(program);
                    }
                }
            }
            return result;
        }

        public async Task<Student> GetStudentByIDAsync(string id_student)
        {
            Student s = await orm.Get<Student,string>(id_student);
            return s;
        }

        public async Task<HighSchool> GetHighSchoolByIDAsync(long id_school)
        {
            HighSchool hs = await orm.Get<HighSchool, long>(id_school);
            return hs;
        }
        public async Task DeleteProgramFromApplications(long id_program)
        {
            await orm.Delete<ProgramApplication>(id_program,1);
        }
        public async Task DeleteStudyProgramAsync(long id_program)
        {
            List<ProgramApplication> connections = await orm.GetAll<ProgramApplication>();
            foreach(ProgramApplication connection in connections)
            {
                await DeleteProgramFromApplications(connection.Id_program);
            }
            await orm.Delete<StudyProgram>(id_program); 
        }
        public async Task DeleteHighSchoolAsync(long id_school)
        {
            List<StudyProgram> programs = await orm.GetAll<StudyProgram>();
            foreach(StudyProgram program in programs)
            {
                DeleteStudyProgramAsync(program.Id_program);
            }
            await orm.Delete<HighSchool>(id_school);
        }
    }
}
