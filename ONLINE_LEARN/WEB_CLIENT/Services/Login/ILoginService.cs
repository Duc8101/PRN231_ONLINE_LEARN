using Common.Base;
using Common.DTO.UserDTO;
using Common.Entity;

namespace WEB_CLIENT.Services.Login
{
    public interface ILoginService
    {
        ResponseBase<User?> Index(Guid userId);
        ResponseBase<User?> Index(LoginDTO DTO);
    }
}
