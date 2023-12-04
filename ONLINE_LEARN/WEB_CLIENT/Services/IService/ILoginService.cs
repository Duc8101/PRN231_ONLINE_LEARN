using DataAccess.DTO;
using DataAccess.Entity;

namespace WEB_CLIENT.Services.IService
{
    public interface ILoginService
    {
        Task<ResponseDTO<User?>> Index(Guid UserID);
        Task<ResponseDTO<User?>> Index(LoginDTO DTO);
    }
}
