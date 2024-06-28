using Common.Base;
using Common.Entity;

namespace WEB_CLIENT.Services.TakeQuiz
{
    public interface ITakeQuizService
    {
        Task<ResponseBase<Dictionary<string, object?>?>> Index(Guid LessonID, Guid StudentID);
        Task<ResponseBase<List<Quiz>?>> Finish(Guid LessonID);
        Task<ResponseBase<Result?>> Finish(Guid LessonID, Guid StudentID, float score, string status);
        Task<ResponseBase<Dictionary<string, object?>?>> Index(Guid LessonID, Guid StudentID, string? button, int minutes, int question_no, int seconds);
    }
}
