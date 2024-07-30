using AutoMapper;
using Common.Base;
using Common.DTO.LessonPdfDTO;
using Common.Entity;
using DataAccess.Model.DAO;
using System.Net;
using WEB_CLIENT.Services.Base;

namespace WEB_CLIENT.Services.ManagerPDF
{
    public class ManagerPDFService : BaseService, IManagerPDFService
    {
        private readonly DAOCourse _daoCourse;
        private readonly DAOLesson _daoLesson;
        private readonly DAOLessonPDF _daoPDF;
        private readonly DAOLessonVideo _daoVideo;

        public ManagerPDFService(IMapper mapper, DAOCourse daoCourse, DAOLesson daoLesson, DAOLessonPDF daoPDF
            , DAOLessonVideo daoVideo) : base(mapper)
        {
            _daoCourse = daoCourse;
            _daoLesson = daoLesson;
            _daoPDF = daoPDF;
            _daoVideo = daoVideo;
        }

        private ResponseBase<Dictionary<string, object>?> getResponse(Guid courseId, Guid creatorId)
        {
            try
            {
                Course? course = _daoCourse.getCourse(courseId, creatorId);
                if (course == null)
                {
                    return new ResponseBase<Dictionary<string, object>?>("Not found course", (int)HttpStatusCode.NotFound);
                }
                List<Lesson> listLesson = _daoLesson.getListLesson(courseId);
                List<LessonPdf> listPDF = _daoPDF.getListPDF();
                List<LessonVideo> listVideo = _daoVideo.getListVideo();
                LessonVideo? lessonVideo = _daoVideo.getVideo(courseId);
                string video = string.Empty;
                string name = string.Empty;
                Guid lessonId = Guid.NewGuid();
                if (lessonVideo != null)
                {
                    video = lessonVideo.FileVideo;
                    name = lessonVideo.VideoName;
                    lessonId = lessonVideo.LessonId;
                }
                Dictionary<string, object> response = new Dictionary<string, object>()
                {
                    {"listLesson", listLesson },
                    {"listPDF", listPDF },
                    {"listVideo", listVideo },
                    {"video", video },
                    {"PDF", string.Empty },
                    {"name", name },
                    {"lId", lessonId },
                    {"courseId", courseId },
                };
                return new ResponseBase<Dictionary<string, object>?>(response);
            }
            catch (Exception ex)
            {
                return new ResponseBase<Dictionary<string, object>?>(ex.Message + " " + ex, (int)HttpStatusCode.InternalServerError);
            }
        }

        public ResponseBase<Dictionary<string, object>?> Create(LessonPdfCreateUpdateDTO DTO, Guid courseId, Guid creatorId)
        {
            try
            {
                ResponseBase<Dictionary<string, object>?> response = getResponse(courseId, creatorId);
                if (response.Data == null)
                {
                    return new ResponseBase<Dictionary<string, object>?>(response.Message, response.Code);
                }
                Lesson? lesson = _daoLesson.getLesson(DTO.LessonId, courseId);
                if (lesson == null)
                {
                    return new ResponseBase<Dictionary<string, object>?>("Not found lesson", (int)HttpStatusCode.NotFound);
                }
                if (DTO.Pdfname == null || DTO.Pdfname.Trim().Length == 0)
                {
                    return new ResponseBase<Dictionary<string, object>?>(response.Data, "You have to input PDF name", (int)HttpStatusCode.Conflict);
                }
                LessonPdf obj = _mapper.Map<LessonPdf>(DTO);
                obj.CreatedAt = DateTime.Now;
                obj.UpdateAt = DateTime.Now;
                obj.IsDeleted = false;
                _daoPDF.CreatePDF(obj);
                List<LessonPdf> listPDF = _daoPDF.getListPDF();
                response.Data["listPDF"] = listPDF;
                return new ResponseBase<Dictionary<string, object>?>(response.Data, "PDF " + DTO.Pdfname.Trim() + " was added successfully!");
            }
            catch (Exception ex)
            {
                return new ResponseBase<Dictionary<string, object>?>(ex.Message + " " + ex, (int)HttpStatusCode.InternalServerError);
            }
        }

        public ResponseBase<Dictionary<string, object>?> Update(int pdfId, LessonPdfCreateUpdateDTO DTO, Guid courseId, Guid creatorId)
        {
            try
            {
                ResponseBase<Dictionary<string, object>?> response = getResponse(courseId, creatorId);
                if (response.Data == null)
                {
                    return new ResponseBase<Dictionary<string, object>?>(response.Message, response.Code);
                }
                Lesson? lesson = _daoLesson.getLesson(DTO.LessonId, courseId);
                if (lesson == null)
                {
                    return new ResponseBase<Dictionary<string, object>?>("Not found lesson", (int)HttpStatusCode.NotFound);
                }
                LessonPdf? PDF = _daoPDF.getPDF(pdfId, DTO.LessonId);
                if (PDF == null)
                {
                    return new ResponseBase<Dictionary<string, object>?>("Not found PDF", (int)HttpStatusCode.NotFound);
                }
                if (DTO.Pdfname == null || DTO.Pdfname.Trim().Length == 0)
                {
                    return new ResponseBase<Dictionary<string, object>?>(response.Data, "You have to input PDF name", (int)HttpStatusCode.Conflict);
                }
                PDF.Pdfname = DTO.Pdfname.Trim();
                if (DTO.FilePdf != null && DTO.FilePdf.Length > 0)
                {
                    PDF.FilePdf = DTO.FilePdf;
                }
                PDF.UpdateAt = DateTime.Now;
                _daoPDF.UpdatePDF(PDF);
                List<LessonPdf> listPDF = _daoPDF.getListPDF();
                response.Data["listPDF"] = listPDF;
                return new ResponseBase<Dictionary<string, object>?>(response.Data, "Update successful");
            }
            catch (Exception ex)
            {
                return new ResponseBase<Dictionary<string, object>?>(ex.Message + " " + ex, (int)HttpStatusCode.InternalServerError);
            }
        }

        public ResponseBase<Dictionary<string, object>?> Delete(int pdfId, Guid lessonId, Guid courseId, Guid creatorId)
        {
            try
            {
                ResponseBase<Dictionary<string, object>?> response = getResponse(courseId, creatorId);
                if (response.Data == null)
                {
                    return new ResponseBase<Dictionary<string, object>?>(null, response.Message, response.Code);
                }
                Lesson? lesson = _daoLesson.getLesson(lessonId, courseId);
                if (lesson == null)
                {
                    return new ResponseBase<Dictionary<string, object>?>("Not found lesson", (int)HttpStatusCode.NotFound);
                }
                LessonPdf? PDF = _daoPDF.getPDF(pdfId, lessonId);
                if (PDF == null)
                {
                    return new ResponseBase<Dictionary<string, object>?>("Not found PDF", (int)HttpStatusCode.NotFound);
                }
                _daoPDF.DeletePDF(PDF);
                List<LessonPdf> listPDF = _daoPDF.getListPDF();
                response.Data["listPDF"] = listPDF;
                return new ResponseBase<Dictionary<string, object>?>(response.Data, "Delete successful");
            }
            catch (Exception ex)
            {
                return new ResponseBase<Dictionary<string, object>?>(ex.Message + " " + ex, (int)HttpStatusCode.InternalServerError);
            }
        }

    }
}
