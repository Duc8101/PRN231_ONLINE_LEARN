using Common.Base;
using Common.DTO.UserDTO;

namespace WEB_CLIENT.Services.Register
{
    public interface IRegisterService
    {
        Task<ResponseBase<bool>> Index(UserCreateDTO DTO);
    }
}
