using Common.Base;
using Common.Entity;

namespace WEB_CLIENT.Services.IService
{
    public interface IRegisterService
    {
        Task<ResponseBase<bool>> Index(User user);
    }
}
