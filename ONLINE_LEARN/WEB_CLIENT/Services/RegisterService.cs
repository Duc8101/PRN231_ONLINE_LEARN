using DataAccess.Const;
using DataAccess.DTO;
using DataAccess.Entity;
using System.Net;
using WEB_CLIENT.Model;
using WEB_CLIENT.Model.DAO;

namespace WEB_CLIENT.Services
{
    public class RegisterService
    {
        private readonly DAOUser dao = new DAOUser();
        public async Task<ResponseDTO<bool>> Index(User user)
        {
            try
            {
                if (await dao.isExist(user.Username, user.Email))
                {
                    return new ResponseDTO<bool>(false, "Username or email has existed", (int)HttpStatusCode.Conflict);
                }
                string newPw = UserUtil.RandomPassword();
                string hashPw = UserUtil.HashPassword(newPw);
                // get body email
                string body = UserUtil.BodyEmailForRegister(newPw);
                // send email
                await UserUtil.sendEmail("Welcome to E-Learning", body, user.Email);
                user.Id = Guid.NewGuid();
                user.Image = UserConst.AVATAR;
                user.Address = user.Address == null || user.Address.Trim().Length == 0 ? null : user.Address.Trim();
                user.Password = hashPw;
                user.RoleName = UserConst.ROLE_STUDENT;
                user.CreatedAt = DateTime.Now;
                user.UpdateAt = DateTime.Now;
                user.IsDeleted = false;
                await dao.CreateUser(user);
                return new ResponseDTO<bool>(true, "Register successful");
            }
            catch (Exception ex)
            {
                return new ResponseDTO<bool>(false, ex.Message + " " + ex, (int)HttpStatusCode.InternalServerError);
            }
        }
    }
}
