using DataAccess.DTO.UserDTO;
using DataAccess.DTO;

namespace WEB_CLIENT.Services.IService
{
    public interface IChangePasswordService
    {
        Task<ResponseDTO<bool>> Index(string username, ChangePasswordDTO DTO);
    }
}
