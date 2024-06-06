using DataAccess.Base;

namespace WEB_CLIENT.Services.IService
{
    public interface IManagerLessonService
    {
        Task<ResponseBase<Dictionary<string, object>?>> Index(Guid CourseID, Guid CreatorID, string? video /* file video */, string? name /*video name or pdf name*/, string? PDF /*file PDF */, Guid? LessonID);
        Task<ResponseBase<Dictionary<string, object>?>> Create(string? LessonName, Guid CourseID, Guid CreatorID);
        Task<ResponseBase<Dictionary<string, object>?>> Update(Guid LessonID, string? LessonName, Guid CourseID, Guid CreatorID);
        Task<ResponseBase<Dictionary<string, object>?>> Delete(Guid LessonID, Guid CourseID, Guid CreatorID);
    }
}
