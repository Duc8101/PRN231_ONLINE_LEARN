using DataAccess.DTO;
using DataAccess.Entity;

namespace WEB_CLIENT.Services.IService
{
    public interface IHomeService
    {
        Task<ResponseDTO<List<User>?>> Index();
    }
}
