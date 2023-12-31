using DataAccess.DTO;
using DataAccess.Entity;

namespace WEB_CLIENT.Services.IService
{
    public interface IManagerCourseService
    {
        Task<ResponseDTO<PagedResultDTO<Course>?>> Index(int? page, Guid TeacherID);
    }
}
