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
        /// <summary>
        /// Returns all programs that selected school has
        /// </summary>
        /// <param name="id_school">Id of school that we want programs for</param>
        /// <returns></returns>
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
        /// <summary>
        /// Returns all programs that are in applicationForm
        /// </summary>
        /// <param name="id_form">Id of applicationForm that we want programs for</param>
        /// <returns></returns>
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
        /// <summary>
        /// Returns Student for selected id
        /// </summary>
        /// <param name="id_student">Id of student to be selected</param>
        /// <returns></returns>
        public async Task<Student> GetStudentByIDAsync(string id_student)
        {
            Student s = await orm.Get<Student,string>(id_student);
            return s;
        }
        /// <summary>
        /// Returns HighSchool for selected id
        /// </summary>
        /// <param name="id_school">Id of school to be selected</param>
        /// <returns></returns>
        public async Task<HighSchool> GetHighSchoolByIDAsync(long id_school)
        {
            HighSchool hs = await orm.Get<HighSchool, long>(id_school);
            return hs;
        }
        /// <summary>
        /// Removes selected program - applicationForm connection in ProgramApplication table
        /// </summary>
        /// <param name="id_program"></param>
        /// <returns></returns>
        public async Task DeleteProgramFromApplications(long id_program)
        {
            await orm.Delete<ProgramApplication>(id_program,1);
        }
        /// <summary>
        /// Removes selected study program from all applications, its M:N connection so function
        /// goes through ProgramApplication records. Then removes the program.
        /// </summary>
        /// <param name="id_program">Program to be removed</param>
        /// <returns></returns>
        public async Task DeleteStudyProgramAsync(long id_program)
        {
            List<ProgramApplication> connections = await orm.GetAll<ProgramApplication>();
            //If program that is to be deleted is under application, remove it
            foreach(ProgramApplication connection in connections)
            {
                if(connection.Id_program == id_program)
                {
                    await DeleteProgramFromApplications(connection.Id_program);
                }
            }
            await orm.Delete<StudyProgram>(id_program); 
        }
        /// <summary>
        /// Deletes all programs that are under selected school that we want to remove,
        /// then removes the school.
        /// </summary>
        /// <param name="id_school">Id school that is to be deleted</param>
        /// <returns></returns>
        public async Task DeleteHighSchoolAsync(long id_school)
        {
            List<StudyProgram> programs = await orm.GetAll<StudyProgram>();
            //If program is under school that is to be removed, remove it
            foreach(StudyProgram program in programs)
            {
                if(program.Id_school == id_school)
                {
                    await DeleteStudyProgramAsync(program.Id_program);
                }
            }
            await orm.Delete<HighSchool>(id_school);
        }
        /// <summary>
        /// Deletes connection of study programs for selected applicationForm
        /// </summary>
        /// <param name="id_form">if of applicationForm to delete connections for</param>
        /// <returns></returns>
        public async Task DeleteApplicationProgramConnectionAsync(long id_form)
        {
            await orm.Delete<ProgramApplication>(id_form);
        }
        /// <summary>
        /// Deletes application form, first removes all connections with study programs
        /// </summary>
        /// <param name="id_form">id of applicationForm to delete</param>
        /// <returns></returns>
        public async Task DeleteApplicationFormAsync(long id_form)
        {
            List<ProgramApplication> connections = await orm.GetAll<ProgramApplication>();
            foreach(ProgramApplication connection in connections)
            {
                if(connection.Id_form == id_form)
                {
                    await DeleteApplicationProgramConnectionAsync(id_form);
                }
            }
            await orm.Delete<ApplicationForm>(id_form);
        }
        /// <summary>
        /// inserts new applicationForm row into applicationForm table
        /// </summary>
        /// <param name="applicationForm">applicationForm to be inserted</param>
        /// <returns></returns>
        public async Task InsertApplicationFormAsync(ApplicationForm applicationForm)
        {
            await orm.Insert<ApplicationForm>(applicationForm);
        }
        /// <summary>
        /// Updates applicationForm with new data
        /// </summary>
        /// <param name="applicationForm">applicationForm with new values</param>
        /// <returns></returns>
        public async Task UpdateApplicationFormAsync(ApplicationForm applicationForm)
        {
            await orm.Update<ApplicationForm>(applicationForm);
        }
        /// <summary>
        /// Updates highSchool with new data
        /// </summary>
        /// <param name="highSchool">highSchool with new values</param>
        /// <returns></returns>
        public async Task UpdateHighSchoolAsync(HighSchool highSchool)
        {
            await orm.Update<HighSchool>(highSchool);
        }
        public async Task InsertHighSchoolAsync(HighSchool highSchool)
        {
            await orm.Insert<HighSchool>(highSchool);
        }
    }
}
