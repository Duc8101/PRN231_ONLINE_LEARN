using Common.Base;
using Common.Const;
using Common.Entity;
using DataAccess.Model.IDAO;
using DataAccess.Model.Util;
using System.Net;
using WEB_CLIENT.Services.IService;

namespace WEB_CLIENT.Services.Service
{
    public class RegisterService : IRegisterService
    {
        private readonly ICommonDAO<User> _daoUser;
        public RegisterService(ICommonDAO<User> daoUser)
        {
            _daoUser = daoUser;
        }
        public async Task<ResponseBase<bool>> Index(User user)
        {
            try
            {
                if (await _daoUser.Any(u => u.Username == user.Username || u.Email == user.Email.Trim()))
                {
                    return new ResponseBase<bool>(false, "Username or email has existed", (int)HttpStatusCode.Conflict);
                }
                string newPw = UserUtil.RandomPassword();
                string hashPw = UserUtil.HashPassword(newPw);
                // get body email
                string body = UserUtil.BodyEmailForRegister(newPw);
                // send email
                await UserUtil.sendEmail("Welcome to E-Learning", body, user.Email);
                user.Id = Guid.NewGuid();
                user.FullName = user.FullName.Trim();
                user.Image = UserConst.AVATAR;
                user.Address = user.Address == null || user.Address.Trim().Length == 0 ? null : user.Address.Trim();
                user.Password = hashPw;
                user.RoleName = UserConst.ROLE_STUDENT;
                user.CreatedAt = DateTime.Now;
                user.UpdateAt = DateTime.Now;
                user.IsDeleted = false;
                await _daoUser.Create(user);
                await _daoUser.Save();
                return new ResponseBase<bool>(true, "Register successful");
            }
            catch (Exception ex)
            {
                return new ResponseBase<bool>(false, ex.Message + " " + ex, (int)HttpStatusCode.InternalServerError);
            }
        }
    }
}
