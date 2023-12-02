using DataAccess.Const;
using DataAccess.Entity;
using Microsoft.EntityFrameworkCore;

namespace WEB_CLIENT.Model.DAO
{
    public class DAOCourse : BaseDAO
    {
        private IQueryable<Course> getQuery(int? CategoryID, string? properties, string? flow,  Guid? TeacherID)
        {
            IQueryable<Course> query = context.Courses.Where(c => c.IsDeleted == false);
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

            if(TeacherID != null)
            {
                query = query.Where(c => c.TeacherId == TeacherID);
            }
            return query;
        }

        public async Task<List<Course>> getList(int? CategoryID, string? properties, string? flow, int page)
        {
            IQueryable<Course> query = getQuery(CategoryID, properties, flow, null);
            return await query.Take(PageSizeConst.MAX_COURSE_IN_PAGE).Take(PageSizeConst.MAX_COURSE_IN_PAGE * (page - 1)).ToListAsync();
        }
        public async Task<int> getNumberPage(int? CategoryID, string? properties, string? flow)
        {
            IQueryable<Course> query = getQuery(CategoryID, properties, flow, null);
            int count = await query.CountAsync();
            return (int )Math.Ceiling((double) count / PageSizeConst.MAX_COURSE_IN_PAGE);
        }
    }
}
