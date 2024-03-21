using AdministrationWPF.Interfaces;
using AdministrationWPF.Models;
using Dapper;
using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace AdministrationWPF.Services
{
    class DatabaseService : IDatabaseService
    {

        public string ConnectionString { get; set; }

        public DatabaseService(string connectionString)
        {
            ConnectionString = connectionString;
        }

        public IEnumerable<T> GetAll<T>() where T : class
        {
            IEnumerable<T> list;
            Type type = typeof(T);
            using (SqlConnection connection = new SqlConnection(ConnectionString))
            {
                string query = "";
                if(type == typeof(ApplicationForm))
                {
                    query = "SELECT * FROM application_form";
                }
                else if(type == typeof(HighSchool))
                {
                    query = "SELECT * FROM highschool";
                }
                else if(type == typeof(Student))
                {
                    query = "SELECT * FROM student";
                }
                else if(type == typeof(StudyProgram))
                {
                    query = "SELECT * FROM study_program";
                }
                list = connection.Query<T> (query);
            }
            return list;
        }

        public bool Insert()
        {
            throw new NotImplementedException();
        }

        public bool Update()
        {
            throw new NotImplementedException();
        }
        public bool Delete()
        {
            throw new NotImplementedException();
        }
    }
}
