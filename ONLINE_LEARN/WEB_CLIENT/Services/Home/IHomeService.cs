using Common.Base;
using Common.Entity;

namespace WEB_CLIENT.Services.Home
{
    public interface IHomeService
    {
        ResponseBase<List<User>?> Index();
    }
}
