using DataAccess.DTO;
using DataAccess.Entity;
using DataAccess.Model.DAO;
using System.Net;

namespace WEB_CLIENT.Services
{
    public class ManagerPDFService
    {
        private readonly DAOCourse daoCourse = new DAOCourse();
        private readonly DAOLesson daoLesson = new DAOLesson();
        private readonly DAOLessonPDF daoPDF = new DAOLessonPDF();
        private readonly DAOLessonVideo daoVideo = new DAOLessonVideo();

        private async Task<ResponseDTO<Dictionary<string, object>?>> getResult(Guid CourseID, Guid CreatorID)
        {
            try
            {
                Course? course = await daoCourse.getCourse(CourseID, CreatorID);
                if (course == null)
                {
                    return new ResponseDTO<Dictionary<string, object>?>(null, "Not found course", (int)HttpStatusCode.NotFound);
                }
                List<Lesson> listLesson = await daoLesson.getList(CourseID);
                List<LessonPdf> listPDF = await daoPDF.getList();
                List<LessonVideo> listVideo = await daoVideo.getList();
                LessonVideo? lessonVideo = await daoVideo.getFirstVideo(CourseID);
                string video = string.Empty;
                string name = string.Empty;
                Guid LessonID = Guid.NewGuid();
                if (lessonVideo != null)
                {
                    video = lessonVideo.FileVideo;
                    name = lessonVideo.VideoName;
                    LessonID = lessonVideo.LessonId;
                }
                Dictionary<string, object> result = new Dictionary<string, object>();
                result["listLesson"] = listLesson;
                result["listPDF"] = listPDF;
                result["listVideo"] = listVideo;
                result["video"] = video;
                result["PDF"] = string.Empty;
                result["name"] = name;
                result["lID"] = LessonID;
                result["CourseID"] = CourseID;
                return new ResponseDTO<Dictionary<string, object>?>(result, string.Empty);
            }
            catch (Exception ex)
            {
                return new ResponseDTO<Dictionary<string, object>?>(null, ex.Message + " " + ex, (int)HttpStatusCode.InternalServerError);
            }
        }
        public async Task<ResponseDTO<Dictionary<string, object>?>> Create(LessonPdf create, Guid CourseID, Guid CreatorID)
        {
            try
            {
                ResponseDTO<Dictionary<string, object>?> result = await getResult(CourseID, CreatorID);
                if (result.Data == null)
                {
                    return new ResponseDTO<Dictionary<string, object>?>(null, result.Message, result.Code);
                }
                Lesson? lesson = await daoLesson.getLesson(create.LessonId);
                if (lesson == null || lesson.CourseId != CourseID)
                {
                    return new ResponseDTO<Dictionary<string, object>?>(null, "Not found lesson", (int)HttpStatusCode.NotFound);
                }
                if (create.Pdfname == null || create.Pdfname.Trim().Length == 0)
                {
                    return new ResponseDTO<Dictionary<string, object>?>(result.Data, "You have to input PDF name", (int)HttpStatusCode.Conflict);
                }
                create.Pdfname = create.Pdfname.Trim();
                create.CreatedAt = DateTime.Now;
                create.UpdateAt = DateTime.Now;
                create.IsDeleted = false;
                await daoPDF.CreatePDF(create);
                List<LessonPdf> listPDF = await daoPDF.getList();
                result.Data["listPDF"] = listPDF;
                return new ResponseDTO<Dictionary<string, object>?>(result.Data, "PDF " + create.Pdfname.Trim() + " was added successfully!");
            }
            catch (Exception ex)
            {
                return new ResponseDTO<Dictionary<string, object>?>(null, ex.Message + " " + ex, (int)HttpStatusCode.InternalServerError);
            }
        }
        public async Task<ResponseDTO<Dictionary<string, object>?>> Update(int PdfID, LessonPdf obj, Guid CourseID, Guid CreatorID)
        {
            try
            {
                ResponseDTO<Dictionary<string, object>?> result = await getResult(CourseID, CreatorID);
                if (result.Data == null)
                {
                    return new ResponseDTO<Dictionary<string, object>?>(null, result.Message, result.Code);
                }
                Lesson? lesson = await daoLesson.getLesson(obj.LessonId);
                if (lesson == null || lesson.CourseId != CourseID)
                {
                    return new ResponseDTO<Dictionary<string, object>?>(null, "Not found lesson", (int)HttpStatusCode.NotFound);
                }
                LessonPdf? PDF = await daoPDF.getPDF(PdfID, obj.LessonId);
                if (PDF == null)
                {
                    return new ResponseDTO<Dictionary<string, object>?>(null, "Not found PDF", (int)HttpStatusCode.NotFound);
                }
                if (obj.Pdfname == null || obj.Pdfname.Trim().Length == 0)
                {
                    return new ResponseDTO<Dictionary<string, object>?>(result.Data, "You have to input PDF name", (int)HttpStatusCode.Conflict);
                }
                PDF.Pdfname = obj.Pdfname.Trim();
                if (obj.FilePdf != null && obj.FilePdf.Length > 0)
                {
                    PDF.FilePdf = obj.FilePdf;
                }
                PDF.UpdateAt = DateTime.Now;
                await daoPDF.UpdatePDF(PDF);
                List<LessonPdf> listPDF = await daoPDF.getList();
                result.Data["listPDF"] = listPDF;
                return new ResponseDTO<Dictionary<string, object>?>(result.Data, "Update successful");
            }
            catch (Exception ex)
            {
                return new ResponseDTO<Dictionary<string, object>?>(null, ex.Message + " " + ex, (int)HttpStatusCode.InternalServerError);
            }
        }
        public async Task<ResponseDTO<Dictionary<string, object>?>> Delete(int PdfID, Guid LessonID, Guid CourseID, Guid CreatorID)
        {
            try
            {
                ResponseDTO<Dictionary<string, object>?> result = await getResult(CourseID, CreatorID);
                if (result.Data == null)
                {
                    return new ResponseDTO<Dictionary<string, object>?>(null, result.Message, result.Code);
                }
                Lesson? lesson = await daoLesson.getLesson(LessonID);
                if (lesson == null || lesson.CourseId != CourseID)
                {
                    return new ResponseDTO<Dictionary<string, object>?>(null, "Not found lesson", (int)HttpStatusCode.NotFound);
                }
                LessonPdf? PDF = await daoPDF.getPDF(PdfID, LessonID);
                if (PDF == null)
                {
                    return new ResponseDTO<Dictionary<string, object>?>(null, "Not found PDF", (int)HttpStatusCode.NotFound);
                }
                await daoPDF.DeletePDF(PDF);
                List<LessonPdf> listPDF = await daoPDF.getList();
                result.Data["listPDF"] = listPDF;
                return new ResponseDTO<Dictionary<string, object>?>(result.Data, "Delete successful");
            }
            catch (Exception ex)
            {
                return new ResponseDTO<Dictionary<string, object>?>(null, ex.Message + " " + ex, (int)HttpStatusCode.InternalServerError);
            }
        }

    }
}
