using Microsoft.AspNetCore.Components.Forms;
using UderSideWEB.Models;
using UserSideWEB.DataLayer;
namespace UserSideWEB.Services
{
    public class HintService
    {
        private readonly DataLayer.DataLayer _dataLayer;

        public HintService()
        {
            _dataLayer = new DataLayer.DataLayer();
        }

        public async Task<List<string>> GetSchoolHints(string inputText)
        {
            List<string> outputStrings = new List<string>();
            List<HighSchool> highSchools = await _dataLayer.GetAllHighSchoolsAsync();
            foreach (var highSchool in highSchools)
            {
                if(highSchool.Name.Contains(inputText))
                {
                    outputStrings.Add(highSchool.Name);
                }
            }
            return outputStrings;
        }
        /// <summary>
        /// Returns names of programs that are under school. 
        /// </summary>
        /// <param name="schoolName">Name of school to get programs of</param>
        /// <returns>List of name of programs under school, returns empty list if name doesnt match any school</returns>
        public async Task<List<string>> GetSchoolPrograms(string schoolName)
        {
            HighSchool selectedSchool = await _dataLayer.GetHighSchoolByNameAsync(schoolName);
            List<string> programNames = new List<string>();
            if (selectedSchool != null)
            {
                List<StudyProgram> programs = await _dataLayer.GetAllStudyProgramsForSchoolAsync(selectedSchool);
                foreach(StudyProgram program in programs)
                {
                    programNames.Add(program.Name);
                }
            }
            return programNames;
        }
    }
}
