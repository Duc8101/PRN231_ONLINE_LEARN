using Common.Entity;
using Common.Enums;
using DataAccess.Model.DBContext;
using Microsoft.EntityFrameworkCore;

namespace DataAccess.Model.DAO
{
    public class DAOEnrollCourse : BaseDAO
    {
        public DAOEnrollCourse(MyDbContext context) : base(context)
        {
        }

        public List<EnrollCourse> getListEnrollCourse(Guid studentId)
        {
            return _context.EnrollCourses.Include(e => e.Course).ThenInclude(e => e.Creator).Where(e => e.StudentId == studentId && e.Course.IsDeleted == false)
                .ToList();
        }

        private IQueryable<EnrollCourse> getQuery(Guid studentId)
        {
            return _context.EnrollCourses.Include(e => e.Course).ThenInclude(e => e.Creator).Where(e => e.StudentId == studentId && e.Course.IsDeleted == false);
        }

        public List<Course> getListCourse(Guid studentId, int page)
        {
            IQueryable<EnrollCourse> query = getQuery(studentId);
            return query.OrderByDescending(e => e.Course.UpdateAt).Skip((int)PageSize.Course_Page * (page - 1))
                .Take((int)PageSize.Course_Page).Select(e => e.Course).ToList();
        }

        public int getNumberPage(Guid studentId)
        {
            IQueryable<EnrollCourse> query = getQuery(studentId);
            int count = query.Count();
            return (int)Math.Ceiling((double)count / (int)PageSize.Course_Page);
        }

        public bool isExist(Guid courseId, Guid studentId)
        {
            return _context.EnrollCourses.Any(e => e.CourseId == courseId && e.StudentId == studentId);
        }

        public void CreateEnrollCourse(EnrollCourse enroll)
        {
            _context.EnrollCourses.Add(enroll);
            _context.SaveChanges();
        }

    }
}
