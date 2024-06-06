using DataAccess.DTO.UserDTO;
using DataAccess.Base;

namespace WEB_CLIENT.Services.IService
{
    public interface IChangePasswordService
    {
        Task<ResponseBase<bool>> Index(string username, ChangePasswordDTO DTO);
    }
}
