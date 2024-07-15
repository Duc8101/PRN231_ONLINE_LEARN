using Common.Base;
using Common.DTO.QuizDTO;

namespace WEB_CLIENT.Services.ManagerQuiz
{
    public interface IManagerQuizService
    {
        ResponseBase<Dictionary<string, object>?> Index(Guid lessonId, Guid courseId, Guid creatorId);
        ResponseBase<Dictionary<string, object>?> Detail(Guid questionId, Guid lessonId, Guid courseId, Guid creatorId);
        ResponseBase<Dictionary<string, Guid>?> Create(Guid lessonId, Guid courseId, Guid creatorId);
        ResponseBase<Dictionary<string, Guid>?> Create(QuizCreateUpdateDTO DTO, Guid courseId, Guid creatorId);
        ResponseBase<Dictionary<string, object>?> Update(Guid questionId, QuizCreateUpdateDTO DTO, Guid courseId, Guid creatorId);
        ResponseBase<Dictionary<string, object>?> Delete(Guid questionId, Guid lessonId, Guid courseId, Guid creatorId);
    }
}
