using Common.Base;
using Common.DTO.UserDTO;
using Common.Entity;
using DataAccess.Model.DAO;
using DataAccess.Model.Helper;
using System.Net;

namespace WEB_CLIENT.Services.ChangePassword
{
    public class ChangePasswordService : IChangePasswordService
    {
        private readonly DAOUser _daoUser;
        public ChangePasswordService(DAOUser daoUser)
        {
            _daoUser = daoUser;
        }

        public ResponseBase<bool> Index(string username, ChangePasswordDTO DTO)
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
                User? user = _daoUser.getUser(login);
                if (user == null)
                {
                    return new ResponseBase<bool>(false, "Your old password not correct", (int)HttpStatusCode.Conflict);
                }
                if (!DTO.ConfirmPassword.Equals(DTO.NewPassword))
                {
                    return new ResponseBase<bool>(false, "Your confirm password not the same new password", (int)HttpStatusCode.Conflict);
                }
                user.Password = UserHelper.HashPassword(DTO.NewPassword);
                user.UpdateAt = DateTime.Now;
                _daoUser.UpdateUser(user);
                return new ResponseBase<bool>(true, "Change successful");
            }
            catch (Exception ex)
            {
                return new ResponseBase<bool>(false, ex.Message + " " + ex, (int)HttpStatusCode.InternalServerError);
            }
        }
    }
}
