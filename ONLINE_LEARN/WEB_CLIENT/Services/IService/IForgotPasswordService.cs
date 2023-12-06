using DataAccess.DTO;

namespace WEB_CLIENT.Services.IService
{
    public interface IForgotPasswordService
    {
        Task<ResponseDTO<bool>> Index(string email);
    }
}
