using DataAccess.Entity;
using Microsoft.EntityFrameworkCore;

namespace WEB_CLIENT.Model.DAO
{
    public class DAOLesson : BaseDAO
    {
        public async Task<List<Lesson>> getList(Guid? CourseID)
        {
            IQueryable<Lesson> query = context.Lessons.Where(l => l.IsDeleted == false);
            if(CourseID != null)
            {
                query = query.Where(l => l.CourseId == CourseID);
            }
            return await query.ToListAsync();
        }
    }
}
