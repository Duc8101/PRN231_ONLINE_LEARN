using Common.Base;
using Common.Entity;
using Common.Pagination;

namespace WEB_CLIENT.Services.IService
{
    public interface IMyCourseService
    {
        Task<ResponseBase<PagedResult<Course>?>> Index(Guid StudentID, int page);
    }
}
