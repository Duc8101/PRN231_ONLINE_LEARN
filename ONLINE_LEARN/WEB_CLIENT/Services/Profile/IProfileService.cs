using Common.Base;
using Common.DTO.UserDTO;

namespace WEB_CLIENT.Services.Profile
{
    public interface IProfileService
    {
        Task<ResponseBase<Dictionary<string, object>?>> Index(Guid UserID);
        Task<ResponseBase<Dictionary<string, object>?>> Index(Guid UserID, ProfileDTO DTO, string valueImg);
    }
}
