using Common.Base;
using Common.Entity;

namespace WEB_CLIENT.Services.IService
{
    public interface IManagerVideoService
    {
        Task<ResponseBase<Dictionary<string, object>?>> Create(LessonVideo create, Guid CourseID, Guid CreatorID);
        Task<ResponseBase<Dictionary<string, object>?>> Update(int VideoID, LessonVideo obj, Guid CourseID, Guid CreatorID);
        Task<ResponseBase<Dictionary<string, object>?>> Delete(int VideoID, Guid LessonID, Guid CourseID, Guid CreatorID);
    }
}
