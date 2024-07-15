using Common.Const;
using Common.DTO.UserDTO;
using Common.Entity;
using DataAccess.Model.DBContext;
using DataAccess.Model.Helper;

namespace DataAccess.Model.DAO
{
    public class DAOUser : BaseDAO
    {
        public DAOUser(MyDbContext context) : base(context)
        {

        }

        public List<User> getTop4Teacher()
        {
            return _context.Users.Where(u => u.RoleName == UserConst.ROLE_TEACHER && u.IsDeleted == false).Take(4)
                .ToList();
        }

        public User? getUser(Guid userId)
        {
            return _context.Users.SingleOrDefault(u => u.Id == userId);
        }

        public User? getUser(LoginDTO DTO)
        {
            return _context.Users.FirstOrDefault(u => u.Username == DTO.Username && u.Password.Equals(UserHelper.HashPassword(DTO.Password)));
        }

        public bool isExist(string username, string email)
        {
            return _context.Users.Any(u => u.Username == username || u.Email == email.Trim());
        }

        public void CreateUser(User user)
        {
            _context.Users.Add(user);
            _context.SaveChanges();
        }

        public User? getUser(string email)
        {
            return _context.Users.FirstOrDefault(u => u.Email == email.Trim());
        }

        public void UpdateUser(User user)
        {
            _context.Users.Update(user);
            _context.SaveChanges();
        }

        public bool isExist(Guid userId, string email)
        {
            return _context.Users.Any(u => u.Email == email.Trim() && u.Id != userId);
        }

        public List<User> getListUser(string? name)
        {
            IQueryable<User> query = _context.Users.Where(u => u.IsDeleted == false && u.RoleName != UserConst.ROLE_ADMIN);
            if (name != null && name.Trim().Length > 0)
            {
                query = query.Where(u => u.Username.ToLower().Contains(name.ToLower().Trim()));
            }
            return query.ToList();
        }
       
    }
}
