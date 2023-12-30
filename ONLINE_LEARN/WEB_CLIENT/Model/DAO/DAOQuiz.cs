using DataAccess.Entity;
using Microsoft.EntityFrameworkCore;

namespace WEB_CLIENT.Model.DAO
{
    public class DAOQuiz : BaseDAO
    {
        public async Task<List<Guid>> getListLesson()
        {
            return await context.Quizzes.Where(q => q.IsDeleted == false).Select(q => q.LessonId).Distinct().ToListAsync();
        }
        public async Task<List<Quiz>> getList(Guid LessonID)
        {
            return await context.Quizzes.Where(q => q.IsDeleted == false && q.LessonId == LessonID).ToListAsync();
        }
    }
}
