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
            return await context.Lessons.SingleOrDefaultAsync(l => l.IsDeleted == false && l.LessonId == LessonID && l.Course.IsDeleted == false);
        }

        public async Task<bool> isExist(string LessonName, Guid CourseID)
        {
            return await context.Lessons.AnyAsync(l => l.LessonName == LessonName.Trim() && l.CourseId == CourseID && l.IsDeleted == false);
        }

        public async Task CreateLesson(Lesson lesson)
        {
            await context.Lessons.AddAsync(lesson);
            await context.SaveChangesAsync();
        }

        public async Task<bool> isExist(string LessonName, Guid CourseID, Guid LessonID)
        {
            return await context.Lessons.AnyAsync(l => l.LessonName == LessonName.Trim() && l.CourseId == CourseID && l.IsDeleted == false && l.LessonId != LessonID);
        }

        public async Task UpdateLesson(Lesson lesson)
        {
            context.Lessons.Update(lesson);
            await context.SaveChangesAsync();
        }
    }
}
