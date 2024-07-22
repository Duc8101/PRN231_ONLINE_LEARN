using Common.Entity;
using Common.Enums;
using DataAccess.Model.DBContext;
using Microsoft.EntityFrameworkCore;

namespace DataAccess.Model.DAO
{
    public class DAOCourse : BaseDAO
    {
        public DAOCourse(MyDbContext context) : base(context)
        {

        }

        private IQueryable<Course> getQuery(int? categoryId, string? properties, bool? asc, Guid? creatorId)
        {
            IQueryable<Course> query = _context.Courses.Include(c => c.Creator).Where(c => c.IsDeleted == false);
            if (categoryId.HasValue)
            {
                query = query.Where(c => c.CategoryId == categoryId);
            }

            if (properties == null || asc == null)
            {
                query = query.OrderByDescending(c => c.UpdateAt);
            }
            else if (asc == true)
            {
                query = query.OrderBy(c => c.CourseName);
            }
            else
            {
                query = query.OrderByDescending(c => c.CourseName);
            }

            if (creatorId.HasValue)
            {
                query = query.Where(c => c.CreatorId == creatorId);
            }
            return query;
        }

        public List<Course> getListCourse(int? categoryId, string? properties, bool? asc, Guid? creatorId, int page)
        {
            IQueryable<Course> query = getQuery(categoryId, properties, asc, creatorId);
            return query.Skip((int)PageSize.Course_Page * (page - 1)).Take((int)PageSize.Course_Page)
                .ToList();
        }

        public int getNumberPage(int? categoryId, string? properties, bool? asc, Guid? creatorId)
        {
            IQueryable<Course> query = getQuery(categoryId, properties, asc, creatorId);
            int count = query.Count();
            return (int)Math.Ceiling((double)count / (int)PageSize.Course_Page);
        }

        public Course? getCourse(Guid courseId)
        {
            return _context.Courses.Include(c => c.Creator).SingleOrDefault(c => c.CourseId == courseId && c.IsDeleted == false);
        }

        public bool isExist(string courseName, int categoryId)
        {
            return _context.Courses.Any(c => c.CourseName == courseName.Trim() && c.CategoryId == categoryId
            && c.IsDeleted == false);
        }

        public void CreateCourse(Course course)
        {
            _context.Courses.Add(course);
            _context.SaveChanges();
        }

        public Course? getCourse(Guid courseId, Guid creatorId)
        {
            return _context.Courses.Include(c => c.Creator).SingleOrDefault(c => c.CourseId == courseId && c.IsDeleted == false
            && c.CreatorId == creatorId);
        }

        public bool isExist(string courseName, int categoryId, Guid courseId)
        {
            return _context.Courses.Any(c => c.CourseName == courseName.Trim() && c.CategoryId == categoryId
            && c.IsDeleted == false && c.CourseId != courseId);
        }

        public void UpdateCourse(Course course)
        {
            _context.Courses.Update(course);
            _context.SaveChanges();
        }

        public void DeleteCourse(Course course)
        {
            course.IsDeleted = true;
            UpdateCourse(course);
        }
    }
}
