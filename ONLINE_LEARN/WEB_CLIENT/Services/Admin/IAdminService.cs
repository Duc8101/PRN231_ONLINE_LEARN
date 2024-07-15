using Common.Base;
using Common.DTO.UserDTO;

namespace WEB_CLIENT.Services.Admin
{
    public interface IAdminService
    {
        ResponseBase<Dictionary<string, object>?> Index(string? name);
        ResponseBase<Dictionary<string, object>?> Detail(Guid userId);
        List<string> Create();
        Task<ResponseBase<List<string>?>> Create(UserCreateDTO DTO);
    }
}
