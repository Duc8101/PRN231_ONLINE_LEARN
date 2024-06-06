using DataAccess.Base;
using DataAccess.Entity;

namespace WEB_CLIENT.Services.IService
{
    public interface IRegisterService
    {
        Task<ResponseBase<bool>> Index(User user);
    }
}
