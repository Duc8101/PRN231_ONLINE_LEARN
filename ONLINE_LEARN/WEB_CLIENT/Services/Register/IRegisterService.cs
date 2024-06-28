using Common.Base;
using Common.Entity;

namespace WEB_CLIENT.Services.Register
{
    public interface IRegisterService
    {
        Task<ResponseBase<bool>> Index(User user);
    }
}
