using Common.Entity;
using DataAccess.Model.DBContext;

namespace DataAccess.Model.DAO
{
    public class DAOQuiz : BaseDAO
    {
        public DAOQuiz(MyDbContext context) : base(context)
        {
        }

        public List<Quiz> getListQuiz(Guid lessonId)
        {
            return _context.Quizzes.Where(q => q.IsDeleted == false && q.LessonId == lessonId).ToList();
        }

        public Quiz? getQuiz(Guid questionId, Guid lessonId)
        {
            return _context.Quizzes.SingleOrDefault(q => q.QuestionId == questionId && q.IsDeleted == false && q.LessonId == lessonId);
        }

        public void CreateQuiz(Quiz quiz)
        {
            _context.Quizzes.Add(quiz);
            _context.SaveChanges();
        }

        public void UpdateQuiz(Quiz quiz)
        {
            _context.Quizzes.Update(quiz);
            _context.SaveChanges();
        }

        public void DeleteQuiz(Quiz quiz)
        {
            quiz.IsDeleted = true;
            UpdateQuiz(quiz);
        }
    }
}
