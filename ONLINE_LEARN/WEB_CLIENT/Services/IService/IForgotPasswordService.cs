using Common.Base;

namespace WEB_CLIENT.Services.IService
{
    public interface IForgotPasswordService
    {
        Task<ResponseBase<bool>> Index(string email);
    }
}
