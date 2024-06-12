using Common.Base;
using Common.Entity;
using DataAccess.Model.IDAO;
using DataAccess.Model.Util;
using System.Net;
using WEB_CLIENT.Services.IService;

namespace WEB_CLIENT.Services.Service
{
    public class ForgotPasswordService : IForgotPasswordService
    {
        private readonly ICommonDAO<User> _daoUser;

        public ForgotPasswordService(ICommonDAO<User> daoUser)
        {
            _daoUser = daoUser;
        }
        public async Task<ResponseBase<bool>> Index(string email)
        {
            try
            {
                User? user = await _daoUser.Get(u => u.Email == email.Trim());
                if (user == null)
                {
                    return new ResponseBase<bool>(false, "Not found email", (int)HttpStatusCode.NotFound);
                }
                string body = UserUtil.BodyEmailForForgetPassword(email);
                string newPw = UserUtil.RandomPassword();
                string hashPw = UserUtil.HashPassword(newPw);
                // send email
                await UserUtil.sendEmail("Welcome to E-Learning", body, user.Email);
                user.Password = hashPw;
                user.UpdateAt = DateTime.Now;
                await _daoUser.Update(user);
                await _daoUser.Save();
                return new ResponseBase<bool>(true, "Password changed successful. Please check your email");
            }
            catch (Exception ex)
            {
                return new ResponseBase<bool>(false, ex.Message + " " + ex, (int)HttpStatusCode.InternalServerError);
            }
        }
    }
}
