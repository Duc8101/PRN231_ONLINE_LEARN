using Common.Base;

namespace WEB_CLIENT.Services.ForgotPassword
{
    public interface IForgotPasswordService
    {
        Task<ResponseBase<bool>> Index(string email);
    }
}
