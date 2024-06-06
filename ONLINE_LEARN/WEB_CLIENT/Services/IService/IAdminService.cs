using DataAccess.Base;
using DataAccess.Entity;

namespace WEB_CLIENT.Services.IService
{
    public interface IAdminService
    {
        Task<ResponseBase<Dictionary<string, object>?>> Index(string? name);
        Task<ResponseBase<Dictionary<string, object>?>> Detail(Guid UserID);
        List<string> Create();
        Task<ResponseBase<List<string>?>> Create(User user);
    }
}
