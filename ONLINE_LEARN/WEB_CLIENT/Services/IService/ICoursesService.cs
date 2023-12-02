using DataAccess.DTO;
using DataAccess.Entity;

namespace WEB_CLIENT.Services.IService
{
    public interface ICoursesService
    {
        Task<ResponseDTO<PagedResultDTO<Course>?>> Index(int? CategoryID, string? properties, string? flow, int? page);
    }
}
