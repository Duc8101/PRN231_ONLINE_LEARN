using Common.Base;
using Common.Entity;

namespace WEB_CLIENT.Services.Home
{
    public interface IHomeService
    {
        Task<ResponseBase<List<User>?>> Index();
    }
}
