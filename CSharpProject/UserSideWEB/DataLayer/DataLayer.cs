using UderSideWEB.Models;

namespace UserSideWEB.DataLayer
{
    public class DataLayer
    {
        private ORM orm;
        public DataLayer()
        {
            orm = new ORM("Data Source=mydb.db");
        }
        public async Task<List<HighSchool>> GetAllHighSchoolsAsync()
        {
            return await orm.GetAll<HighSchool>();
        }
    }
}
