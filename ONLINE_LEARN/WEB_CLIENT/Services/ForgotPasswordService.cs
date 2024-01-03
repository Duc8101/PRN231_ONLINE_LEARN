using DataAccess.DTO;
using DataAccess.Entity;
using System.Net;
using WEB_CLIENT.Model;
using WEB_CLIENT.Model.DAO;

namespace WEB_CLIENT.Services
{
    public class ForgotPasswordService
    {
        private readonly DAOUser dao = new DAOUser();
        public async Task<ResponseDTO<bool>> Index(string email)
        {
            try
            {
                User? user = await dao.getUser(email);
                if (user == null)
                {
                    return new ResponseDTO<bool>(false, "Not found email", (int)HttpStatusCode.NotFound);
                }
                string body = UserUtil.BodyEmailForForgetPassword(email);
                string newPw = UserUtil.RandomPassword();
                string hashPw = UserUtil.HashPassword(newPw);
                // send email
                await UserUtil.sendEmail("Welcome to E-Learning", body, user.Email);
                return new ResponseDTO<bool>(true, "Password changed successful. Please check your email");
            }
            catch (Exception ex)
            {
                return new ResponseDTO<bool>(false, ex.Message + " " + ex, (int)HttpStatusCode.InternalServerError);
            }
        }
    }
}
