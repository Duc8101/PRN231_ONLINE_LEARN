using DataAccess.Entity;
using Microsoft.EntityFrameworkCore;

namespace DataAccess.Model.DAO
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

        public async Task<Quiz?> getQuiz(Guid QuestionID, Guid LessonID)
        {
            return await context.Quizzes.SingleOrDefaultAsync(q => q.QuestionId == QuestionID && q.IsDeleted == false && q.LessonId == LessonID);
        }

        public async Task CreateQuiz(Quiz quiz)
        {
            await context.Quizzes.AddAsync(quiz);
            await context.SaveChangesAsync();
        }

        public async Task UpdateQuiz(Quiz quiz)
        {
            context.Quizzes.Update(quiz);
            await context.SaveChangesAsync();
        }

        public async Task DeleteQuiz(Quiz quiz)
        {
            quiz.IsDeleted = true;
            await UpdateQuiz(quiz);
        }

    }
}
