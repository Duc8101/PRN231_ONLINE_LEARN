using DataAccess.Entity;
using Microsoft.EntityFrameworkCore;

namespace WEB_CLIENT.Model.DAO
{
    public class DAOEnrollCourse : BaseDAO
    {
        public async Task<List<EnrollCourse>> getList(Guid StudentID)
        {
            return await context.EnrollCourses.Where(e => e.StudentId == StudentID).ToListAsync();
        }
    }
}
