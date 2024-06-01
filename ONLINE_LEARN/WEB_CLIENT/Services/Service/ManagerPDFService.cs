using DataAccess.DTO;
using DataAccess.Entity;
using DataAccess.Model.IDAO;
using Microsoft.EntityFrameworkCore;
using System.Net;
using WEB_CLIENT.Services.IService;

namespace WEB_CLIENT.Services.Service
{
    public class ManagerPDFService : IManagerPDFService
    {
        private readonly ICommonDAO<Course> _daoCourse;
        private readonly ICommonDAO<Lesson> _daoLesson;
        private readonly ICommonDAO<LessonPdf> _daoPDF;
        private readonly ICommonDAO<LessonVideo> _daoVideo;
        public ManagerPDFService(ICommonDAO<Course> daoCourse, ICommonDAO<Lesson> daoLesson, ICommonDAO<LessonPdf> daoPDF
            , ICommonDAO<LessonVideo> daoVideo)
        {
            _daoCourse = daoCourse;
            _daoLesson = daoLesson;
            _daoPDF = daoPDF;
            _daoVideo = daoVideo;
        }

        private async Task<ResponseDTO<Dictionary<string, object>?>> getResult(Guid CourseID, Guid CreatorID)
        {
            try
            {
                Course? course = await _daoCourse.FindAll(c => c.CourseId == CourseID && c.IsDeleted == false && c.CreatorId == CreatorID)
                    .Include(c => c.Creator).FirstOrDefaultAsync();
                if (course == null)
                {
                    return new ResponseDTO<Dictionary<string, object>?>(null, "Not found course", (int)HttpStatusCode.NotFound);
                }
                List<Lesson> listLesson = await _daoLesson.FindAll(l => l.IsDeleted == false && l.CourseId == CourseID)
                  .OrderBy(l => l.LessonNo).ToListAsync();
                List<LessonPdf> listPDF = await _daoPDF.FindAll(p => p.IsDeleted == false).ToListAsync(); ;
                List<LessonVideo> listVideo = await _daoVideo.FindAll(v => v.IsDeleted == false).ToListAsync();
                LessonVideo? lessonVideo = await _daoVideo.Get(v => v.IsDeleted == false && v.Lesson.IsDeleted == false && v.Lesson.LessonNo == 1
                        && v.Lesson.CourseId == CourseID);
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
                Lesson? lesson = await _daoLesson.Get(l => l.IsDeleted == false && l.LessonId == create.LessonId
                && l.Course.IsDeleted == false && l.CourseId == CourseID);
                if (lesson == null)
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
                await _daoPDF.Create(create);
                await _daoPDF.Save();
                List<LessonPdf> listPDF = await _daoPDF.FindAll(p => p.IsDeleted == false).ToListAsync();
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
                Lesson? lesson = await _daoLesson.Get(l => l.IsDeleted == false && l.LessonId == obj.LessonId
                && l.Course.IsDeleted == false && l.CourseId == CourseID);
                if (lesson == null)
                {
                    return new ResponseDTO<Dictionary<string, object>?>(null, "Not found lesson", (int)HttpStatusCode.NotFound);
                }
                LessonPdf? PDF = await _daoPDF.Get(p => p.Pdfid == PdfID && p.IsDeleted == false && p.LessonId == obj.LessonId);
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
                await _daoPDF.Update(PDF);
                await _daoPDF.Save();
                List<LessonPdf> listPDF = await _daoPDF.FindAll(p => p.IsDeleted == false).ToListAsync();
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
                Lesson? lesson = await _daoLesson.Get(l => l.IsDeleted == false && l.LessonId == LessonID
                && l.Course.IsDeleted == false && l.CourseId == CourseID);
                if (lesson == null)
                {
                    return new ResponseDTO<Dictionary<string, object>?>(null, "Not found lesson", (int)HttpStatusCode.NotFound);
                }
                LessonPdf? PDF = await _daoPDF.Get(p => p.Pdfid == PdfID && p.IsDeleted == false && p.LessonId == LessonID);
                if (PDF == null)
                {
                    return new ResponseDTO<Dictionary<string, object>?>(null, "Not found PDF", (int)HttpStatusCode.NotFound);
                }
                PDF.IsDeleted = true;
                await _daoPDF.Update(PDF);
                await _daoPDF.Save();
                List<LessonPdf> listPDF = await _daoPDF.FindAll(p => p.IsDeleted == false).ToListAsync();
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
