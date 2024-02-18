using DataAccess.Const;
using DataAccess.DTO.UserDTO;
using DataAccess.Entity;
using Microsoft.EntityFrameworkCore;
namespace WEB_CLIENT.Model.DAO
{
    public class DAOUser : MyDbContext
    {
        public async Task<User?> getUser(Guid UserID)
        {
            return await Users.SingleOrDefaultAsync(u => u.Id == UserID && u.IsDeleted == false);
        }

        public async Task<List<User>> getTop4Teacher()
        {
            return await Users.Where(u => u.RoleName == UserConst.ROLE_TEACHER && u.IsDeleted == false).Take(4).ToListAsync();
        }

        public async Task<User?> getUser(LoginDTO DTO)
        {
            User? user = await Users.SingleOrDefaultAsync(u => u.Username == DTO.Username && u.IsDeleted == false);
            if (user == null || DTO.Password == null || string.Compare(user.Password, UserUtil.HashPassword(DTO.Password), false) != 0)
            {
                return null;
            }
            return user;
        }
        public async Task<bool> isExist(string username, string email)
        {
            return await Users.AnyAsync(u => u.Username == username || u.Email == email.Trim());
        }

        public async Task CreateUser(User user)
        {
            await Users.AddAsync(user);
            await SaveChangesAsync();
        }

        public async Task<User?> getUser(string email)
        {
            return await Users.FirstOrDefaultAsync(u => u.Email == email.Trim());
        }

        public async Task<bool> isExist(Guid UserID, string email)
        {
            return await Users.AnyAsync(u => u.Email == email.Trim() && u.Id != UserID);
        }

        public async Task UpdateUser(User user)
        {
            Users.Update(user);
            await SaveChangesAsync();
        }

        public async Task<List<User>> getList(string? name)
        {
            IQueryable<User> query = Users.Where(u => u.IsDeleted == false && u.RoleName != UserConst.ROLE_ADMIN);
            if(name != null && name.Trim().Length > 0)
            {
                query = query.Where(u => u.Username.ToLower().Contains(name.ToLower().Trim()));
            }
            return await query.ToListAsync();
        }
    }
}
