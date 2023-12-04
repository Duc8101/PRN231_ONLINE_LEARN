using DataAccess;
using DataAccess.Const;
using DataAccess.DTO;
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

        public async Task<User?> getUser(LoginDTO DTO)
        {
            User? user = await context.Users.SingleOrDefaultAsync(u => u.Username == DTO.Username && u.IsDeleted == false);
            if (user == null || string.Compare(user.Password, UserUtil.HashPassword(DTO.Password), false) != 0)
            {
                return null;
            }
            return user;
        }
    }
}
