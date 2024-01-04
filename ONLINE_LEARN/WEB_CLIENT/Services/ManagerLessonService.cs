using DataAccess.Const;
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
                result["CourseID"] = CourseID;
                return new ResponseDTO<Dictionary<string, object>?>(result, string.Empty);
            }
            catch (Exception ex)
            {
                return new ResponseDTO<Dictionary<string, object>?>(null, ex.Message + " " + ex, (int)HttpStatusCode.InternalServerError);
            }
        }
        public async Task<ResponseDTO<Dictionary<string, object>?>> Create(string? LessonName, Guid CourseID, Guid CreatorID)
        {
            try
            {
                ResponseDTO<Dictionary<string, object>?> response = await Index(CourseID, CreatorID, null, null, null, null);
                if(response.Data == null)
                {
                    return new ResponseDTO<Dictionary<string, object>?>(null, response.Message, response.Code);
                }
                if (LessonName == null)
                {
                    return new ResponseDTO<Dictionary<string, object>?>(response.Data, "You have to input lesson name", (int) HttpStatusCode.Conflict);
                }
                if (LessonName.Trim().Length > LessonConst.MAX_LENGTH_LESSON_NAME)
                {
                    return new ResponseDTO<Dictionary<string, object>?>(response.Data, "Lesson name max " + LessonConst.MAX_LENGTH_LESSON_NAME + " characters", (int)HttpStatusCode.Conflict);
                }
                if(await daoLesson.isExist(LessonName.Trim(), CourseID))
                {
                    return new ResponseDTO<Dictionary<string, object>?>(response.Data, "Lesson existed", (int) HttpStatusCode.Conflict);
                }
                List<Lesson> list = await daoLesson.getList(CourseID);
                Lesson lesson = new Lesson()
                {
                    LessonId = Guid.NewGuid(),
                    LessonName = LessonName.Trim(),
                    CourseId = CourseID,
                    LessonNo = list.Count + 1,
                    CreatedAt = DateTime.Now,
                    UpdateAt = DateTime.Now,
                    IsDeleted = false,
                };
                await daoLesson.CreateLesson(lesson);
                list = await daoLesson.getList(CourseID);
                response.Data["listLesson"] = list;
                return new ResponseDTO<Dictionary<string, object>?>(response.Data, "Create successful");
            }
            catch (Exception ex)
            {
                return new ResponseDTO<Dictionary<string, object>?>(null, ex.Message + " " + ex, (int)HttpStatusCode.InternalServerError);
            }
        }
    }
}
