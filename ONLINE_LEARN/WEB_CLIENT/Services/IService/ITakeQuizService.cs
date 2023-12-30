using DataAccess.DTO;
using DataAccess.Entity;

namespace WEB_CLIENT.Services.IService
{
    public interface ITakeQuizService
    {
        Task<ResponseDTO<Dictionary<string, object?>?>> Index(Guid LessonID, Guid StudentID);
        Task<ResponseDTO<List<Quiz>?>> Finish(Guid LessonID);
        Task<ResponseDTO<Result?>> Finish(Guid LessonID, Guid StudentID, float score, string status);
        Task<ResponseDTO<Dictionary<string, object?>?>> Index(Guid LessonID, Guid StudentID, string? button, int minutes, int question_no, int seconds);
    }
}
