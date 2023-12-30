using DataAccess.Entity;
using Microsoft.EntityFrameworkCore;

namespace WEB_CLIENT.Model.DAO
{
    public class DAOLesson : BaseDAO
    {
        public async Task<List<Lesson>> getList(Guid CourseID)
        {
            return await context.Lessons.Where(l => l.IsDeleted == false && l.CourseId == CourseID).OrderBy(l => l.LessonNo).ToListAsync();
        }

        public async Task<Lesson?> getLesson(Guid LessonID)
        {
            return await context.Lessons.SingleOrDefaultAsync(l => l.IsDeleted == false && l.LessonId == LessonID);
        }
    }
}
