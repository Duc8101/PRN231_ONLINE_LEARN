using Common.Base;
using Common.DTO.LessonVideoDTO;

namespace WEB_CLIENT.Services.ManagerVideo
{
    public interface IManagerVideoService
    {
        ResponseBase<Dictionary<string, object>?> Create(LessonVideoCreateUpdateDTO DTO, Guid courseId, Guid creatorId);
        ResponseBase<Dictionary<string, object>?> Update(int videoId, LessonVideoCreateUpdateDTO DTO, Guid courseId, Guid creatorId);
        ResponseBase<Dictionary<string, object>?> Delete(int videoId, Guid lessonId, Guid courseId, Guid creatorId);
    }
}
