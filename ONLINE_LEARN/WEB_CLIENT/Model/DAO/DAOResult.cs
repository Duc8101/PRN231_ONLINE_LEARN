using DataAccess.Entity;
using Microsoft.EntityFrameworkCore;

namespace WEB_CLIENT.Model.DAO
{
    public class DAOResult : MyDbContext
    {
        public async Task<Result?> getResult(Guid LessonID, Guid StudentID)
        {
            return await Results.SingleOrDefaultAsync(r => r.LessonId == LessonID && r.StudentId == StudentID);
        }
        public async Task CreateResult(Result result)
        {
            await Results.AddAsync(result);
            await SaveChangesAsync();
        }
        public async Task UpdateResult(Result result)
        {
            Results.Update(result);
            await SaveChangesAsync();
        }
    }
}
