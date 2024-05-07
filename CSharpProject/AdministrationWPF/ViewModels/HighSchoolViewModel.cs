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
            string dbPath = @"E:\CSharpProject\CSharpProject\UserSideWEB\mydb.db";
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
    }
}
