using DataAccess.DTO;
using DataAccess.Entity;
using System.Net;
using WEB_CLIENT.Model.DAO;

namespace WEB_CLIENT.Services
{
    public class ManagerLessonService
    {
        private readonly DAOCourse daoCourse = new DAOCourse();
        private readonly DAOLesson daoLesson = new DAOLesson();
        private readonly DAOLessonPDF daoPDF = new DAOLessonPDF();
        private readonly DAOLessonVideo daoVideo = new DAOLessonVideo();
        public async Task<ResponseDTO<Dictionary<string, object>?>> Index(Guid CourseID, Guid CreatorID, string? video /* file video */, string? name /*video name or pdf name*/, string? PDF /*file PDF */, Guid? LessonID)
        {
            try
            {
                Course? course = await daoCourse.getCourse(CourseID, CreatorID);
                if (course == null)
                {
                    return new ResponseDTO<Dictionary<string, object>?>(null, "Not found course", (int) HttpStatusCode.NotFound);
                }
                List<Lesson> listLesson = await daoLesson.getList(CourseID);
                List<LessonPdf> listPDF = await daoPDF.getList();
                List<LessonVideo> listVideo = await daoVideo.getList();
                // if start to learn course
                if (video == null && PDF == null)
                {
                    LessonVideo? lessonVideo = await daoVideo.getFirstVideo(CourseID);
                    if (lessonVideo != null)
                    {
                        video = lessonVideo.FileVideo;
                        name = lessonVideo.VideoName;
                        LessonID = lessonVideo.LessonId;
                    }
                }
                Dictionary<string, object> result = new Dictionary<string, object>();
                result["listLesson"] = listLesson;
                result["listPDF"] = listPDF;
                result["listVideo"] = listVideo;
                result["video"] = video == null ? "" : video;
                result["PDF"] = PDF == null ? "" : PDF;
                result["name"] = name == null ? "" : name;
                result["lID"] = LessonID == null ? Guid.NewGuid() : LessonID;
                return new ResponseDTO<Dictionary<string, object>?>(result, string.Empty);
            }
            catch (Exception ex)
            {
                return new ResponseDTO<Dictionary<string, object>?>(null, ex.Message + " " + ex, (int)HttpStatusCode.InternalServerError);
            }
        }
    }
}
