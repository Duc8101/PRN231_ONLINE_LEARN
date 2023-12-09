using DataAccess.DTO;
using DataAccess.DTO.UserDTO;

namespace WEB_CLIENT.Services.IService
{
    public interface IChangePasswordService
    {
        Task<ResponseDTO<bool>> Index(string username, ChangePasswordDTO DTO);
    }
}
