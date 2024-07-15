using Common.Base;
using Common.DTO.UserDTO;

namespace WEB_CLIENT.Services.ChangePassword
{
    public interface IChangePasswordService
    {
        ResponseBase<bool> Index(string username, ChangePasswordDTO DTO);
    }
}
