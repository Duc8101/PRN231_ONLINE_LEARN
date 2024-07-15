using Common.Base;
using Common.DTO.LessonPdfDTO;
using Common.Entity;

namespace WEB_CLIENT.Services.ManagerPDF
{
    public interface IManagerPDFService
    {
        ResponseBase<Dictionary<string, object>?> Create(LessonPdfCreateUpdateDTO DTO, Guid courseId, Guid creatorId);
        ResponseBase<Dictionary<string, object>?> Update(int pdfId, LessonPdfCreateUpdateDTO DTO, Guid courseId, Guid creatorId);
        ResponseBase<Dictionary<string, object>?> Delete(int pdfId, Guid lessonId, Guid courseId, Guid creatorId);
    }
}
