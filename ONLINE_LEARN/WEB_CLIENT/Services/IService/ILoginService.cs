using DataAccess.Base;
using DataAccess.DTO.UserDTO;
using DataAccess.Entity;

namespace WEB_CLIENT.Services.IService
{
    public interface ILoginService
    {
        Task<ResponseBase<User?>> Index(Guid UserID);
        Task<ResponseBase<User?>> Index(LoginDTO DTO);
    }
}
