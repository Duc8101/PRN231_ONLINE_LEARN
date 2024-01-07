using DataAccess.DTO;
using DataAccess.Entity;
using System.Net;
using WEB_CLIENT.Model.DAO;

namespace WEB_CLIENT.Services
{
    public class ManagerVideoService
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

        public async Task<ResponseDTO<Dictionary<string, object>?>> Create(LessonVideo create, Guid CourseID, Guid CreatorID)
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
                if (create.VideoName == null || create.VideoName.Trim().Length == 0)
                {
                    return new ResponseDTO<Dictionary<string, object>?>(result.Data, "You have to input video name", (int)HttpStatusCode.Conflict);
                }
                create.VideoName = create.VideoName.Trim();
                create.CreatedAt = DateTime.Now;
                create.UpdateAt = DateTime.Now;
                create.IsDeleted = false;
                await daoVideo.CreateVideo(create);
                List<LessonVideo> listVideo = await daoVideo.getList();
                result.Data["listVideo"] = listVideo;
                return new ResponseDTO<Dictionary<string, object>?>(result.Data, "Video " + create.VideoName + " was added successfully!");
            }
            catch (Exception ex)
            {
                return new ResponseDTO<Dictionary<string, object>?>(null, ex.Message + " " + ex, (int)HttpStatusCode.InternalServerError);
            }
        }
    }
}
