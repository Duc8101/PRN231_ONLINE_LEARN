using Common.Base;
using Common.DTO.UserDTO;

namespace WEB_CLIENT.Services.Profile
{
    public interface IProfileService
    {
        ResponseBase<Dictionary<string, object>?> Index(Guid userId);
        ResponseBase<Dictionary<string, object>?> Index(Guid userId, ProfileDTO DTO, string valueImg);
    }
}
