using DataAccess.Entity;
using Microsoft.EntityFrameworkCore;

namespace WEB_CLIENT.Model.DAO
{
    public class DAOResult : BaseDAO
    {
        public async Task<Result?> getResult(Guid LessonID, Guid StudentID)
        {
            return await context.Results.SingleOrDefaultAsync(r => r.LessonId == LessonID && r.StudentId == StudentID);
        }

        public async Task CreateResult(Result result)
        {
            await context.Results.AddAsync(result);
            await context.SaveChangesAsync();
        }

        public async Task UpdateResult(Result result)
        {
            context.Results.Update(result);
            await context.SaveChangesAsync();
        }
    }
}
