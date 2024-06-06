using DataAccess.Base;
using DataAccess.Entity;

namespace WEB_CLIENT.Services.IService
{
    public interface IManagerPDFService
    {
        Task<ResponseBase<Dictionary<string, object>?>> Create(LessonPdf create, Guid CourseID, Guid CreatorID);
        Task<ResponseBase<Dictionary<string, object>?>> Update(int PdfID, LessonPdf obj, Guid CourseID, Guid CreatorID);
        Task<ResponseBase<Dictionary<string, object>?>> Delete(int PdfID, Guid LessonID, Guid CourseID, Guid CreatorID);
    }
}
