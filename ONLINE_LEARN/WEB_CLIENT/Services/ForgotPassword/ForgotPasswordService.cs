using Common.Base;
using Common.Entity;
using DataAccess.Model.DAO;
using DataAccess.Model.Helper;
using System.Net;

namespace WEB_CLIENT.Services.ForgotPassword
{
    public class ForgotPasswordService : IForgotPasswordService
    {
        private readonly DAOUser _daoUser;

        public ForgotPasswordService(DAOUser daoUser)
        {
            _daoUser = daoUser;
        }

        public async Task<ResponseBase<bool>> Index(string email)
        {
            try
            {
                User? user = _daoUser.getUser(email);
                if (user == null)
                {
                    return new ResponseBase<bool>(false, "Not found email", (int)HttpStatusCode.NotFound);
                }
                string body = UserHelper.BodyEmailForForgetPassword(email);
                string newPw = UserHelper.RandomPassword();
                string hashPw = UserHelper.HashPassword(newPw);
                // send email
                await UserHelper.sendEmail("Welcome to E-Learning", body, user.Email);
                user.Password = hashPw;
                user.UpdateAt = DateTime.Now;
                _daoUser.UpdateUser(user);
                return new ResponseBase<bool>(true, "Password changed successful. Please check your email");
            }
            catch (Exception ex)
            {
                return new ResponseBase<bool>(false, ex.Message + " " + ex, (int)HttpStatusCode.InternalServerError);
            }
        }
    }
}
