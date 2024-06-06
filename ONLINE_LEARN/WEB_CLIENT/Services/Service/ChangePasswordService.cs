using DataAccess.Base;
using DataAccess.DTO.UserDTO;
using DataAccess.Entity;
using DataAccess.Model;
using DataAccess.Model.IDAO;
using System.Net;
using WEB_CLIENT.Services.IService;

namespace WEB_CLIENT.Services.Service
{
    public class ChangePasswordService : IChangePasswordService
    {
        private readonly ICommonDAO<User> _daoUser;
        public ChangePasswordService(ICommonDAO<User> daoUser)
        {
            _daoUser = daoUser;
        }

        public async Task<ResponseBase<bool>> Index(string username, ChangePasswordDTO DTO)
        {
            try
            {
                if (DTO.CurrentPassword == null)
                {
                    return new ResponseBase<bool>(false, "Current password must not contain all space", (int)HttpStatusCode.Conflict);
                }
                if (DTO.ConfirmPassword == null)
                {
                    return new ResponseBase<bool>(false, "Confirm password must not contain all space", (int)HttpStatusCode.Conflict);
                }
                if (DTO.NewPassword == null)
                {
                    return new ResponseBase<bool>(false, "New password must not contain all space", (int)HttpStatusCode.Conflict);
                }
                LoginDTO login = new LoginDTO()
                {
                    Username = username,
                    Password = DTO.CurrentPassword
                };
                User? user = await _daoUser.Get(u => u.Username == login.Username && u.IsDeleted == false);
                if (user == null || login.Password == null || string.Compare(user.Password, UserUtil.HashPassword(login.Password), false) != 0)
                {
                    return new ResponseBase<bool>(false, "Your old password not correct", (int)HttpStatusCode.Conflict);
                }
                if (!DTO.ConfirmPassword.Equals(DTO.NewPassword))
                {
                    return new ResponseBase<bool>(false, "Your confirm password not the same new password", (int)HttpStatusCode.Conflict);
                }
                user.Password = UserUtil.HashPassword(DTO.NewPassword);
                user.UpdateAt = DateTime.Now;
                await _daoUser.Update(user);
                await _daoUser.Save();
                return new ResponseBase<bool>(true, "Change successful");
            }
            catch (Exception ex)
            {
                return new ResponseBase<bool>(false, ex.Message + " " + ex, (int)HttpStatusCode.InternalServerError);
            }
        }
    }
}
