using AdministrationWPF.Core;
using AdministrationWPF.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UderSideWEB.Models;
using UserSideWEB.DataLayer;

namespace AdministrationWPF.ViewModels
{
    public class ApplicationFormViewModel : ViewModel
    {
        private readonly DataLayer _dataLayer;

        public ObservableCollection<ApplicationFormItem> Applications { get; set; }

        public ApplicationFormViewModel()
        {
            /*string path = Directory.GetCurrentDirectory();
            string dbPath = @"CSharpProject\UserSideWEB\\mydb.db";
            string finalPath = Path.Combine(path, dbPath);*/
            string dbPath = @"E:\CSharpProject\CSharpProject\UserSideWEB\mydb.db";
            _dataLayer = new DataLayer(@"Data source=" + dbPath);
            Applications = new ObservableCollection<ApplicationFormItem>();
        }

        public async Task createApplicationsAsync()
        {
            List<ApplicationForm> allApps = await _dataLayer.GetAllApplicationFormsAsync();
            foreach (ApplicationForm app in allApps)
            {
                ApplicationFormItem item = new ApplicationFormItem();
                item.Id_form = app.Id_form;
                item.Date_submit = app.Date_submit;
                item.Student = await _dataLayer.GetStudentByIDAsync(app.Id_student);
                item.StudyPrograms = new ObservableCollection<StudyProgramItem>();
                List<StudyProgram> programs = await _dataLayer.GetProgramsForApplicationAsync(app.Id_form);
                foreach (StudyProgram program in programs)
                {
                    StudyProgramItem subItem = new StudyProgramItem();
                    subItem.Id_program = program.Id_program;
                    subItem.School = await _dataLayer.GetHighSchoolByIDAsync(program.Id_school);
                    subItem.Name = program.Name;
                    subItem.Description = program.Description;
                    subItem.Capacity = program.Capacity;
                    item.StudyPrograms.Add(subItem);
                }
                Applications.Add(item);
            }
        }

    }
}
