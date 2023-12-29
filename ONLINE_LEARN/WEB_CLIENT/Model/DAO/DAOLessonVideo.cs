using DataAccess.Entity;
using Microsoft.EntityFrameworkCore;

namespace WEB_CLIENT.Model.DAO
{
    public class DAOLessonVideo : BaseDAO
    {
        public async Task<List<LessonVideo>> getList()
        {
            return await context.LessonVideos.Where(v => v.IsDeleted == false).ToListAsync();
        }

        public async Task<LessonVideo?> getFirstVideo(Guid CourseID)
        {
            return await context.LessonVideos.Where(v => v.IsDeleted == false && v.Lesson.IsDeleted == false && v.Lesson.LessonNo == 1
            && v.Lesson.CourseId == CourseID).FirstOrDefaultAsync();
        }
    }
}
