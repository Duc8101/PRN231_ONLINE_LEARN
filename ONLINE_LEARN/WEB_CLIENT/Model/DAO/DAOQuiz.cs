using DataAccess.Entity;
using Microsoft.EntityFrameworkCore;

namespace WEB_CLIENT.Model.DAO
{
    public class DAOQuiz : MyDbContext
    {
        public async Task<List<Guid>> getListLesson()
        {
            return await Quizzes.Where(q => q.IsDeleted == false).Select(q => q.LessonId).Distinct().ToListAsync();
        }
        public async Task<List<Quiz>> getList(Guid LessonID)
        {
            return await Quizzes.Where(q => q.IsDeleted == false && q.LessonId == LessonID).ToListAsync();
        }

        public async Task<Quiz?> getQuiz(Guid QuestionID, Guid LessonID)
        {
            return await Quizzes.SingleOrDefaultAsync(q => q.QuestionId == QuestionID && q.IsDeleted == false && q.LessonId == LessonID);
        }

        public async Task CreateQuiz(Quiz quiz)
        {
            await Quizzes.AddAsync(quiz);
            await SaveChangesAsync();
        }

        public async Task UpdateQuiz(Quiz quiz)
        {
            Quizzes.Update(quiz);
            await SaveChangesAsync();
        }

        public async Task DeleteQuiz(Quiz quiz)
        {
            quiz.IsDeleted = true;
            await UpdateQuiz(quiz);
        }
    }
}
