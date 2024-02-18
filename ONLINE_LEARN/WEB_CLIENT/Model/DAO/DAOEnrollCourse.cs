using DataAccess.Const;
using DataAccess.Entity;
using Microsoft.EntityFrameworkCore;

namespace WEB_CLIENT.Model.DAO
{
    public class DAOEnrollCourse : MyDbContext
    {
        private IQueryable<EnrollCourse> getQuery(Guid StudentID)
        {
            IQueryable<EnrollCourse> query = EnrollCourses.Include(e => e.Course).ThenInclude(e => e.Creator)
                .Where(e => e.StudentId == StudentID && e.Course.IsDeleted == false);
            return query;
        }
        public async Task<List<EnrollCourse>> getList(Guid StudentID)
        {
            IQueryable<EnrollCourse> query = getQuery(StudentID);
            return await query.ToListAsync();
        }
        public async Task<List<Course>> getList(Guid StudentID, int page)
        {
            IQueryable<EnrollCourse> query = getQuery(StudentID);
            return await query.OrderByDescending(e => e.Course.UpdateAt)
                .Skip(PageSizeConst.MAX_COURSE_IN_PAGE * (page - 1)).Take(PageSizeConst.MAX_COURSE_IN_PAGE)
                .Select(e => e.Course).ToListAsync();
        }
        public async Task<int> getNumberPage(Guid StudentID)
        {
            IQueryable<EnrollCourse> query = getQuery(StudentID);
            int count = await query.CountAsync();
            return (int) Math.Ceiling((double) count / PageSizeConst.MAX_COURSE_IN_PAGE);
        }
        public async Task<bool> isExist(Guid CourseID, Guid StudentID)
        {
            return await EnrollCourses.AnyAsync(e => e.CourseId == CourseID && e.StudentId == StudentID);
        }
        public async Task CreateEnrollCourse(EnrollCourse enroll)
        {
            await EnrollCourses.AddAsync(enroll);
            await SaveChangesAsync();
        }
    }
}
