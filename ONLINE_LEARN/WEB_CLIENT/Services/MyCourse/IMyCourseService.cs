using Common.Base;
using Common.Entity;
using Common.Paginations;

namespace WEB_CLIENT.Services.MyCourse
{
    public interface IMyCourseService
    {
        ResponseBase<Pagination<Course>?> Index(Guid studentId, int page);
    }
}
