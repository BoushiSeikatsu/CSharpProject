using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdministrationWPF.Interfaces
{
    internal interface IDatabaseService
    {
        string ConnectionString { get;}
        IEnumerable<T> GetAll<T>() where T : class;
        bool Insert();
        bool Update();
        bool Delete();
    }
}
