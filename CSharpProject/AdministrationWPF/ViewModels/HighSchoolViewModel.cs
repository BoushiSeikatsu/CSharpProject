using AdministrationWPF.Core;
using AdministrationWPF.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UderSideWEB.Models;
using UserSideWEB.DataLayer;

namespace AdministrationWPF.ViewModels
{
    public class HighSchoolViewModel : ViewModel
    {
        private readonly DataLayer _dataLayer;

        public ObservableCollection<HighSchoolItem> HighSchools { get; set; }

        public HighSchoolViewModel()
        {
            //D:\CSharpProjects\CSharpProject\CSharpProject\UserSideWEB\mydb.db
            //E:\CSharpProject\CSharpProject\UserSideWEB\mydb.db
            string dbPath = @"D:\CSharpProjects\CSharpProject\CSharpProject\UserSideWEB\mydb.db";
            _dataLayer = new DataLayer(@"Data source=" + dbPath);
            HighSchools = new ObservableCollection<HighSchoolItem>();
        }

        public async Task CreateHighschoolsAsync()
        {
            var dbResult = await _dataLayer.GetAllHighSchoolsAsync();
            foreach(HighSchool school in dbResult)
            {
                HighSchoolItem item = new HighSchoolItem();
                item.Id_school = school.Id_school;
                item.Name = school.Name;
                item.City = school.City;
                item.Street = school.Street;
                item.PSC = school.PSC;
                HighSchools.Add(item);
            }
        }

        public async Task DeleteSchool(HighSchoolItem item)
        {
            await _dataLayer.DeleteHighSchoolAsync(item.Id_school);
            HighSchools.Remove(item);
        }

        public async Task UpdateSchool(HighSchoolItem item)
        {
            HighSchool school = new HighSchool();
            school.Id_school = item.Id_school;
            school.Name = item.Name;
            school.Street = item.Street;
            school.City = item.City;
            school.PSC = item.PSC;
            //Update in db
            await _dataLayer.UpdateHighSchoolAsync(school);
            //Update in app
            for(int i = 0;i<HighSchools.Count;i++)
            {
                if (HighSchools[i].Id_school ==  item.Id_school)
                {
                    HighSchools[i] = item; break;
                }
            }
        }
        public async Task InsertSchool(HighSchoolItem item)
        {
            HighSchool school = new HighSchool();
            school.Name = item.Name;
            school.Street = item.Street;
            school.City = item.City;
            school.PSC = item.PSC;
            await _dataLayer.InsertHighSchoolAsync(school);
            //Get last record, that is newly inserted one and get its generated id
            List<HighSchool> allApps = await _dataLayer.GetAllHighSchoolsAsync();
            item.Id_school = allApps.Last().Id_school;
            HighSchools.Add(item);
        }
    }
}
