using DataAccess.Entity;
using Microsoft.EntityFrameworkCore;

namespace DataAccess.Model.DAO
{
    public class DAOEnrollCourse : BaseDAO
    {
        private IQueryable<EnrollCourse> getQuery(Guid StudentID)
        {
            IQueryable<EnrollCourse> query = context.EnrollCourses.Include(e => e.Course).ThenInclude(e => e.Creator)
                .Where(e => e.StudentId == StudentID && e.Course.IsDeleted == false);
            return query;
        }
        public async Task<List<EnrollCourse>> getList(Guid StudentID)
        {
            IQueryable<EnrollCourse> query = getQuery(StudentID);
            return await query.ToListAsync();
        }

    }
}
