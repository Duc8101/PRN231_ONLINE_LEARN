using DataAccess.DTO;
using DataAccess.Entity;

namespace WEB_CLIENT.Services.IService
{
    public interface IManagerVideoService
    {
        Task<ResponseDTO<Dictionary<string, object>?>> Create(LessonVideo create, Guid CourseID, Guid CreatorID);
        Task<ResponseDTO<Dictionary<string, object>?>> Update(int VideoID, LessonVideo obj, Guid CourseID, Guid CreatorID);
        Task<ResponseDTO<Dictionary<string, object>?>> Delete(int VideoID, Guid LessonID, Guid CourseID, Guid CreatorID);
    }
}
