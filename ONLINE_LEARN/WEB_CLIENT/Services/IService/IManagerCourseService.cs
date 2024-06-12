using Common.Base;
using Common.Entity;
using Common.Pagination;

namespace WEB_CLIENT.Services.IService
{
    public interface IManagerCourseService
    {
        Task<ResponseBase<PagedResult<Course>?>> Index(int? page, Guid TeacherID);
        Task<ResponseBase<List<Category>?>> Create();
        Task<ResponseBase<List<Category>?>> Create(Course course, Guid CreatorID);
        Task<ResponseBase<Dictionary<string, object>?>> Update(Guid CourseID, Guid CreatorID);
        Task<ResponseBase<Dictionary<string, object>?>> Update(Guid CourseID, Course course);
        Task<ResponseBase<PagedResult<Course>?>> Delete(Guid CourseID, Guid CreatorID);
    }
}
