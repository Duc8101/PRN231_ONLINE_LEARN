using DataAccess.Base;
using DataAccess.Entity;

namespace WEB_CLIENT.Services.IService
{
    public interface IHomeService
    {
        Task<ResponseBase<List<User>?>> Index();
    }
}
