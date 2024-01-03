using DataAccess.DTO;
using DataAccess.DTO.UserDTO;
using DataAccess.Entity;
using System.Net;
using WEB_CLIENT.Model;
using WEB_CLIENT.Model.DAO;

namespace WEB_CLIENT.Services
{
    public class ChangePasswordService
    {
        private readonly DAOUser dao = new DAOUser();
        public async Task<ResponseDTO<bool>> Index(string username, ChangePasswordDTO DTO)
        {
            try
            {
                if (DTO.CurrentPassword == null)
                {
                    return new ResponseDTO<bool>(false, "Current password must not contain all space", (int)HttpStatusCode.Conflict);
                }
                if (DTO.ConfirmPassword == null)
                {
                    return new ResponseDTO<bool>(false, "Confirm password must not contain all space", (int)HttpStatusCode.Conflict);
                }
                if (DTO.NewPassword == null)
                {
                    return new ResponseDTO<bool>(false, "New password must not contain all space", (int)HttpStatusCode.Conflict);
                }
                LoginDTO login = new LoginDTO()
                {
                    Username = username,
                    Password = DTO.CurrentPassword
                };
                User? user = await dao.getUser(login);
                if (user == null)
                {
                    return new ResponseDTO<bool>(false, "Your old password not correct", (int)HttpStatusCode.Conflict);
                }
                if (!DTO.ConfirmPassword.Equals(DTO.NewPassword))
                {
                    return new ResponseDTO<bool>(false, "Your confirm password not the same new password", (int)HttpStatusCode.Conflict);
                }
                user.Password = UserUtil.HashPassword(DTO.NewPassword);
                user.UpdateAt = DateTime.Now;
                await dao.UpdateUser(user);
                return new ResponseDTO<bool>(true, "Change successful");
            }
            catch (Exception ex)
            {
                return new ResponseDTO<bool>(false, ex.Message + " " + ex, (int)HttpStatusCode.InternalServerError);
            }
        }
    }
}
