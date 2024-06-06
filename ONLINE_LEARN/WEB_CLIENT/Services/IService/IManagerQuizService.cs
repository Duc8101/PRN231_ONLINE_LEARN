using DataAccess.Base;
using DataAccess.Entity;

namespace WEB_CLIENT.Services.IService
{
    public interface IManagerQuizService
    {
        Task<ResponseBase<Dictionary<string, object>?>> Index(Guid LessonID, Guid CourseID, Guid CreatorID);
        Task<ResponseBase<Dictionary<string, object>?>> Detail(Guid QuestionID, Guid LessonID, Guid CourseID, Guid CreatorID);
        Task<ResponseBase<Dictionary<string, Guid>?>> Create(Guid LessonID, Guid CourseID, Guid CreatorID);
        Task<ResponseBase<Dictionary<string, Guid>?>> Create(Quiz create, Guid LessonID, Guid CourseID, Guid CreatorID);
        Task<ResponseBase<Dictionary<string, object>?>> Update(Guid QuestionID, Quiz obj, Guid LessonID, Guid CourseID, Guid CreatorID);
        Task<ResponseBase<Dictionary<string, object>?>> Delete(Guid QuestionID, Guid LessonID, Guid CourseID, Guid CreatorID);
    }
}
