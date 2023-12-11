using DataAccess.Const;
using DataAccess.Entity;
using Microsoft.EntityFrameworkCore;

namespace WEB_CLIENT.Model.DAO
{
    public class DAOCourse : BaseDAO
    {
        private IQueryable<Course> getQuery(int? CategoryID, string? properties, string? flow,  Guid? CreatorID)
        {
            IQueryable<Course> query = context.Courses.Include(c => c.Creator).Where(c => c.IsDeleted == false);
            if(CategoryID != null)
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

            if(CreatorID != null)
            {
                query = query.Where(c => c.CreatorId == CreatorID);
            }
            return query;
        }
        public async Task<List<Course>> getList(int? CategoryID, string? properties, string? flow, int page, Guid? CreatorID)
        {
            IQueryable<Course> query = getQuery(CategoryID, properties, flow, null);
            return await query.Skip(PageSizeConst.MAX_COURSE_IN_PAGE * (page - 1)).Take(PageSizeConst.MAX_COURSE_IN_PAGE).ToListAsync();
        }
        public async Task<int> getNumberPage(int? CategoryID, string? properties, string? flow, Guid? CreatorID)
        {
            IQueryable<Course> query = getQuery(CategoryID, properties, flow, CreatorID);
            int count = await query.CountAsync();
            return (int )Math.Ceiling((double) count / PageSizeConst.MAX_COURSE_IN_PAGE);
        }
        public async Task<Course?> getCourse(Guid CourseID)
        {
            return await context.Courses.Include(c => c.Creator).SingleOrDefaultAsync(c => c.CourseId == CourseID && c.IsDeleted == false);
        } 
    }
}
