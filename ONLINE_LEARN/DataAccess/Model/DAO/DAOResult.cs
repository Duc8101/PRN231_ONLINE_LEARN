using Common.Entity;
using DataAccess.Model.DBContext;

namespace DataAccess.Model.DAO
{
    public class DAOResult : BaseDAO
    {
        public DAOResult(MyDbContext context) : base(context)
        {
        }

        public Result? getResult(Guid lessonId, Guid studentId)
        {
            return _context.Results.SingleOrDefault(r => r.LessonId == lessonId && r.StudentId == studentId);
        }

        public void CreateResult(Result result)
        {
            _context.Results.Add(result);
            _context.SaveChanges();
        }

        public void UpdateResult(Result result)
        {
            _context.Results.Update(result);
            _context.SaveChanges();
        }
    }
}
