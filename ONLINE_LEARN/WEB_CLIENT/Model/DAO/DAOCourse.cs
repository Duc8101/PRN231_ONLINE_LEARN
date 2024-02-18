using DataAccess.Const;
using DataAccess.Entity;
using Microsoft.EntityFrameworkCore;

namespace WEB_CLIENT.Model.DAO
{
    public class DAOCourse : MyDbContext
    {
        private IQueryable<Course> getQuery(int? CategoryID, string? properties, string? flow,  Guid? CreatorID)
        {
            IQueryable<Course> query = Courses.Include(c => c.Creator).Where(c => c.IsDeleted == false);
            if(CategoryID.HasValue)
            {
                query = query.Where(c => c.CategoryId == CategoryID);
            }

            if(properties == null)
            {
                query = query.OrderByDescending(c => c.UpdateAt);
            }else if(flow != null)
            {
                if(flow == "asc")
                {
                    query = query.OrderBy(c => c.CourseName);
                }else if(flow == "desc")
                {
                    query = query.OrderByDescending(c => c.CourseName);
                }
            }

            if(CreatorID.HasValue)
            {
                query = query.Where(c => c.CreatorId == CreatorID);
            }
            return query;
        }
        public async Task<List<Course>> getList(int? CategoryID, string? properties, string? flow, int page, Guid? CreatorID)
        {
            IQueryable<Course> query = getQuery(CategoryID, properties, flow, CreatorID);
            return await query.Skip(PageSizeConst.MAX_COURSE_IN_PAGE * (page - 1)).Take(PageSizeConst.MAX_COURSE_IN_PAGE).ToListAsync();
        }
        public async Task<int> getNumberPage(int? CategoryID, string? properties, string? flow, Guid? CreatorID)
        {
            IQueryable<Course> query = getQuery(CategoryID, properties, flow, CreatorID);
            int count = await query.CountAsync();
            return (int) Math.Ceiling((double) count / PageSizeConst.MAX_COURSE_IN_PAGE);
        }
        public async Task<Course?> getCourse(Guid CourseID)
        {
            return await Courses.Include(c => c.Creator).SingleOrDefaultAsync(c => c.CourseId == CourseID && c.IsDeleted == false);
        }
        public async Task<bool> isExist(string CourseName, int CategoryID)
        {
            return await Courses.AnyAsync(c => c.CourseName == CourseName.Trim() && c.CategoryId == CategoryID && c.IsDeleted == false);
        }
        public async Task CreateCourse(Course course)
        {
            await Courses.AddAsync(course);
            await SaveChangesAsync();
        }
        public async Task<Course?> getCourse(Guid CourseID, Guid CreatorID)
        {
            return await Courses.Include(c => c.Creator).SingleOrDefaultAsync(c => c.CourseId == CourseID && c.IsDeleted == false && c.CreatorId == CreatorID);
        }
        public async Task<bool> isExist(string CourseName, int CategoryID, Guid CourseID)
        {
            return await Courses.AnyAsync(c => c.CourseName == CourseName.Trim() && c.CategoryId == CategoryID && c.IsDeleted == false && c.CourseId != CourseID);
        }
        public async Task UpdateCourse(Course course)
        {
            Courses.Update(course);
            await SaveChangesAsync();
        }
        public async Task DeleteCourse(Course course)
        {
            course.IsDeleted = true;
            await UpdateCourse(course);
        }

    }
}
