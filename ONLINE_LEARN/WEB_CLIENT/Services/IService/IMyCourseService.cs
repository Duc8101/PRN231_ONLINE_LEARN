using DataAccess.DTO;
using DataAccess.Entity;

namespace WEB_CLIENT.Services.IService
{
    public interface IMyCourseService
    {
        Task<ResponseDTO<PagedResultDTO<Course>?>> Index(Guid StudentID, int page);
    }
}
