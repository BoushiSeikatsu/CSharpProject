using AdministrationWPF.Core;
using AdministrationWPF.Models;
using System;
using System.Collections;
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
        private readonly object _applicationsLock = new object();
        public ObservableCollection<ApplicationFormItem> Applications { get; set; }

        public ApplicationFormViewModel()
        {
            /*string path = Directory.GetCurrentDirectory();
            string dbPath = @"CSharpProject\UserSideWEB\\mydb.db";
            string finalPath = Path.Combine(path, dbPath);*/
            //D:\CSharpProjects\CSharpProject\CSharpProject\UserSideWEB\mydb.db
            //E:\CSharpProject\CSharpProject\UserSideWEB\mydb.db
            string dbPath = @"D:\CSharpProjects\CSharpProject\CSharpProject\UserSideWEB\mydb.db";
            _dataLayer = new DataLayer(@"Data source=" + dbPath);
            Applications = new ObservableCollection<ApplicationFormItem>();
        }

        public async Task CreateApplicationsAsync()
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
                lock(_applicationsLock)
                {
                    Applications.Add(item);
                }
            }
           //Create watcher for changes in db
            Thread refreshChanges = new Thread(async () =>
            {
                while (true)
                {
                    //Refresh applications every 5 seconds to reflect changes in db
                    //that might be created by users sending their applications on web
                    List<ApplicationForm> allApps = await _dataLayer.GetAllApplicationFormsAsync();
                    //If count changed
                    int appCount = 0;
                    //Musíme uzavřít kritickou sekci pro část kdy se ptáme na počet prvků 
                    lock(_applicationsLock)
                    {
                        appCount = Applications.Count;
                    }
                    if(allApps.Count != appCount)
                    {
                        //New applications are added as last records, we care only about inserted records
                        //Deleted records are handled by wpf application
                        for(int i = appCount; i < allApps.Count; i++)
                        {
                            ApplicationFormItem item = new ApplicationFormItem();
                            //Current applicationForm record to be added
                            var app = allApps.ElementAt(i);
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
                            lock (_applicationsLock)
                            {
                                //ObservableCollection je vytvořena na UI threadu a může se upravovat jenom tím UI threadem
                                /*Abychom mohli teda upravit ObservableCollection z jiného threadu tak musíme
                                  použít delegáta na UI Dispatcher a tím zařídíme, že UI thread provede úpravu
                                 */
                                App.Current.Dispatcher.Invoke((Action)(() =>
                                {
                                    Applications.Add(item);
                                })); 
                            }
                        }
                    }
                    Thread.Sleep(5000);
                }
            });
            //Thread se nastaví na background thread aby nebránil procesu v ukončení když se ukončí aplikace
            refreshChanges.IsBackground = true;
            refreshChanges.Start();
        }
        public async Task DeleteApplicationForm(ApplicationFormItem item)
        {
            await _dataLayer.DeleteApplicationFormAsync(item.Id_form);
            lock(_applicationsLock)
            {
                Applications.Remove(item);
            }
        }
        public async Task InsertApplicationForm(ApplicationFormItem item)
        {
            ApplicationForm form = new ApplicationForm();
            form.Date_submit = item.Date_submit;
            form.Id_student = item.Student.Id_card;
            await _dataLayer.InsertApplicationFormAsync(form);
            //Item has been added, now we can assign its auto generated id
            List<ApplicationForm> allApps = await _dataLayer.GetAllApplicationFormsAsync();
            item.Id_form = allApps.Last().Id_form;
            lock( _applicationsLock)
            {
                Applications.Add(item);
            }
        }
        public async Task UpdateApplicationForm(ApplicationFormItem item)
        {
            ApplicationForm form = new ApplicationForm();
            form.Date_submit = item.Date_submit;
            form.Id_student = item.Student.Id_card;
            form.Id_form = item.Id_form;
            await _dataLayer.UpdateApplicationFormAsync(form);
            lock(_applicationsLock)
            {
                for (int i = 0;i < Applications.Count;i++)
                {
                    if (Applications[i].Id_form == item.Id_form)
                    {
                        Applications[i] = item;
                        break;
                    }
                }
            }
        }
    }
}
