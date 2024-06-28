using Common.Base;
using Common.DTO.UserDTO;
using Common.Entity;

namespace WEB_CLIENT.Services.Login
{
    public interface ILoginService
    {
        Task<ResponseBase<User?>> Index(Guid UserID);
        Task<ResponseBase<User?>> Index(LoginDTO DTO);
    }
}
