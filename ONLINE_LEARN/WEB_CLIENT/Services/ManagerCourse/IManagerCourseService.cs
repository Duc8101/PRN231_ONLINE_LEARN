using Common.Base;
using Common.DTO.CourseDTO;
using Common.Entity;
using Common.Paginations;

namespace WEB_CLIENT.Services.ManagerCourse
{
    public interface IManagerCourseService
    {
        ResponseBase<Pagination<Course>?> Index(int? page, Guid teacherId);
        ResponseBase<List<Category>?> Create();
        ResponseBase<List<Category>?> Create(CourseCreateUpdateDTO DTO, Guid creatorId);
        ResponseBase<Dictionary<string, object>?> Update(Guid courseId, Guid creatorId);
        ResponseBase<Dictionary<string, object>?> Update(Guid courseId, CourseCreateUpdateDTO DTO);
        ResponseBase<bool> Delete(Guid courseId, Guid creatorId);
    }
}
