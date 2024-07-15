using Common.Base;
using Common.Entity;

namespace WEB_CLIENT.Services.TakeQuiz
{
    public interface ITakeQuizService
    {
        ResponseBase<Dictionary<string, object?>?> Index(Guid lessonId, Guid studentId);
        ResponseBase<List<Quiz>?> Finish(Guid lessonId);
        ResponseBase<Result?> Finish(Guid lessonId, Guid studentId, float score, string status);
        ResponseBase<Dictionary<string, object?>?> Index(Guid lessonId, Guid studentId, string? button, int minutes, int question_no, int seconds);
    }
}
