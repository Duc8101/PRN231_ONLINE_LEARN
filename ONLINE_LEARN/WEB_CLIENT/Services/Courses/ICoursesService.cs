using Common.Base;

namespace WEB_CLIENT.Services.Courses
{
    public interface ICoursesService
    {
        ResponseBase<Dictionary<string, object?>?> Index(int? categoryId, string? properties, bool? asc, int? page, string? userId);
        ResponseBase<Dictionary<string, object?>?> Detail(Guid courseId, string? userId);
        ResponseBase<Dictionary<string, object?>?> EnrollCourse(Guid courseId, Guid userId);
        ResponseBase<Dictionary<string, object>?> LearnCourse(Guid courseId, Guid userId, string? video /* file video */, string? name /*video name or pdf name*/, string? PDF /*file PDF */, Guid? lessonId, int? videoId, int? PDFId);
    }
}
