using DataAccess.DTO;
using DataAccess.Entity;
using WEB_CLIENT.Services.IService;

namespace WEB_CLIENT.Services.Service
{
    public class CoursesService : ICoursesService
    {
        public Task<ResponseDTO<PagedResultDTO<Course>?>> Index(int? CategoryID, string? properties, string? flow, int? page)
        {
            throw new NotImplementedException();
        }
    }
}
