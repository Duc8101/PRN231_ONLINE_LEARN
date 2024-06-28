using Common.Base;
using Common.Entity;
using Common.Pagination;

namespace WEB_CLIENT.Services.MyCourse
{
    public interface IMyCourseService
    {
        Task<ResponseBase<PagedResult<Course>?>> Index(Guid StudentID, int page);
    }
}
