using Common.Base;
using Common.DTO.LessonDTO;

namespace WEB_CLIENT.Services.ManagerLesson
{
    public interface IManagerLessonService
    {
        ResponseBase<Dictionary<string, object>?> Index(Guid courseId, Guid creatorId, string? video /* file video */, string? name /*video name or pdf name*/, string? PDF /*file PDF */, Guid? lessonId);
        ResponseBase<Dictionary<string, object>?> Create(LessonCreateUpdateDTO DTO, Guid creatorId);
        ResponseBase<Dictionary<string, object>?> Update(Guid lessonId, LessonCreateUpdateDTO DTO, Guid creatorId);
        ResponseBase<Dictionary<string, object>?> Delete(Guid lessonId, Guid courseId, Guid creatorId);
    }
}
