using DataAccess.Entity;
using Microsoft.EntityFrameworkCore;

namespace WEB_CLIENT.Model.DAO
{
    public class DAOUser : BaseDAO 
    {
        public async Task<User?> getUser(Guid UserID)
        {
            return await context.Users.SingleOrDefaultAsync(u => u.Id == UserID);
        }
    }
}
