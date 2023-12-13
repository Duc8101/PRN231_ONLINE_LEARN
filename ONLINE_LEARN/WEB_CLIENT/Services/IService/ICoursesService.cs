using DataAccess.DTO;

namespace WEB_CLIENT.Services.IService
{
    public interface ICoursesService
    {
        Task<ResponseDTO<Dictionary<string, object>?>> Index(int? CategoryID, string? properties, string? flow, int? page, Guid? CreatorID);
        Task<ResponseDTO<Dictionary<string, object>?>> Detail(Guid CourseID);
        Task<ResponseDTO<Dictionary<string, object>?>> EnrollCourse(Guid CourseID, Guid UserID);
    }
}
