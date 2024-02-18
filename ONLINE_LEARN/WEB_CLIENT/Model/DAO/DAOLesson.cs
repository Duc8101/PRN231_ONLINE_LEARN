using DataAccess.Entity;
using Microsoft.EntityFrameworkCore;

namespace WEB_CLIENT.Model.DAO
{
    public class DAOLesson : MyDbContext
    {
        public async Task<List<Lesson>> getList(Guid CourseID)
        {
            return await Lessons.Where(l => l.IsDeleted == false && l.CourseId == CourseID).OrderBy(l => l.LessonNo).ToListAsync();
        }

        public async Task<Lesson?> getLesson(Guid LessonID)
        {
            return await Lessons.SingleOrDefaultAsync(l => l.IsDeleted == false && l.LessonId == LessonID && l.Course.IsDeleted == false);
        }

        public async Task<bool> isExist(string LessonName, Guid CourseID)
        {
            return await Lessons.AnyAsync(l => l.LessonName == LessonName.Trim() && l.CourseId == CourseID && l.IsDeleted == false);
        }

        public async Task CreateLesson(Lesson lesson)
        {
            await Lessons.AddAsync(lesson);
            await SaveChangesAsync();
        }

        public async Task<bool> isExist(string LessonName, Guid CourseID, Guid LessonID)
        {
            return await Lessons.AnyAsync(l => l.LessonName == LessonName.Trim() && l.CourseId == CourseID && l.IsDeleted == false && l.LessonId != LessonID);
        }

        public async Task UpdateLesson(Lesson lesson)
        {
            Lessons.Update(lesson);
            await SaveChangesAsync();
        }

        public async Task DeleteLesson(Lesson lesson)
        {
            lesson.IsDeleted = true;
            await UpdateLesson(lesson);
        }
    }
}
