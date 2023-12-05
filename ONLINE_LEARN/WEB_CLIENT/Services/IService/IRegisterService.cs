using DataAccess.DTO;
using DataAccess.Entity;

namespace WEB_CLIENT.Services.IService
{
    public interface IRegisterService
    {
        Task<ResponseDTO<bool>> Index(User user);
    }
}
