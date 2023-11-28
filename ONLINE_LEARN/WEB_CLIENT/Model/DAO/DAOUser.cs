using DataAccess.Const;
using DataAccess.Entity;
using Microsoft.EntityFrameworkCore;
namespace WEB_CLIENT.Model.DAO
{
    public class DAOUser : BaseDAO
    {
        public async Task<User?> getUser(Guid UserID)
        {
            return await context.Users.SingleOrDefaultAsync(u => u.Id == UserID && u.IsDeleted == false);
        }

        public async Task<List<User>> getTop4Teacher()
        {
            return await context.Users.Where(u => u.RoleName == UserConst.ROLE_TEACHER && u.IsDeleted == false).Take(4).ToListAsync();
        }
    }
}
