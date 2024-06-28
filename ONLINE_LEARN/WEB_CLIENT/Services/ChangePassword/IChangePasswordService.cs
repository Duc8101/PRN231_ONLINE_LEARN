using Common.Base;
using Common.DTO.UserDTO;

namespace WEB_CLIENT.Services.ChangePassword
{
    public interface IChangePasswordService
    {
        Task<ResponseBase<bool>> Index(string username, ChangePasswordDTO DTO);
    }
}
