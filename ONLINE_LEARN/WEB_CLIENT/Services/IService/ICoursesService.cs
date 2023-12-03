using DataAccess.DTO;
using DataAccess.Entity;

namespace WEB_CLIENT.Services.IService
{
    public interface ICoursesService
    {
        Task<ResponseDTO<Dictionary<string, object>?>> Index(int? CategoryID, string? properties, string? flow, int? page);
        Task<ResponseDTO<Dictionary<string, object>?>> Detail(Guid CourseID);
    }
}
