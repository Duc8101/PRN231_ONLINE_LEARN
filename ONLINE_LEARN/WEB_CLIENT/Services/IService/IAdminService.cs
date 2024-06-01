using DataAccess.DTO;
using DataAccess.Entity;

namespace WEB_CLIENT.Services.IService
{
    public interface IAdminService
    {
        Task<ResponseDTO<Dictionary<string, object>?>> Index(string? name);
        Task<ResponseDTO<Dictionary<string, object>?>> Detail(Guid UserID);
        List<string> Create();
        Task<ResponseDTO<List<string>?>> Create(User user);
    }
}
