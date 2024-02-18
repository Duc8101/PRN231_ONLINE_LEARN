using DataAccess.Entity;
using Microsoft.EntityFrameworkCore;

namespace WEB_CLIENT.Model.DAO
{
    public class DAOCategory: MyDbContext
    {
        public async Task<List<Category>> getList()
        {
            return await Categories.ToListAsync();
        }
    }
}
