using DataAccess.Base;
using DataAccess.Entity;
using DataAccess.Pagination;

namespace WEB_CLIENT.Services.IService
{
    public interface IMyCourseService
    {
        Task<ResponseBase<PagedResult<Course>?>> Index(Guid StudentID, int page);
    }
}
