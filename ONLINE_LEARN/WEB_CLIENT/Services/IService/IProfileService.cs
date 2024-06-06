using DataAccess.Base;
using DataAccess.DTO.UserDTO;

namespace WEB_CLIENT.Services.IService
{
    public interface IProfileService
    {
        Task<ResponseBase<Dictionary<string, object>?>> Index(Guid UserID);
        Task<ResponseBase<Dictionary<string, object>?>> Index(Guid UserID, ProfileDTO DTO, string valueImg);
    }
}
