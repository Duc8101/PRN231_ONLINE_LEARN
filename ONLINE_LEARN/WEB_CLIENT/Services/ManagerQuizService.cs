using DataAccess.DTO;
using DataAccess.Entity;
using System.Net;
using WEB_CLIENT.Model.DAO;

namespace WEB_CLIENT.Services
{
    public class ManagerQuizService
    {
        private readonly DAOCourse daoCourse = new DAOCourse();
        private readonly DAOLesson daoLesson = new DAOLesson();
        private readonly DAOQuiz daoQuiz = new DAOQuiz();
        public async Task<ResponseDTO<Dictionary<string, object>?>> Index(Guid LessonID, Guid CourseID, Guid CreatorID)
        {
            try
            {
                Course? course = await daoCourse.getCourse(CourseID, CreatorID);
                if (course == null)
                {
                    return new ResponseDTO<Dictionary<string, object>?>(null, "Not found course", (int)HttpStatusCode.NotFound);
                }
                Lesson? lesson = await daoLesson.getLesson(LessonID);
                if (lesson == null || lesson.CourseId != CourseID)
                {
                    return new ResponseDTO<Dictionary<string, object>?>(null, "Not found lesson", (int)HttpStatusCode.NotFound);
                }
                Dictionary<string, object> dic = new Dictionary<string, object>();
                List<Quiz> list = await daoQuiz.getList(LessonID);
                dic["LessonID"] = LessonID;
                dic["CourseID"] = CourseID;
                dic["list"] = list;
                return new ResponseDTO<Dictionary<string, object>?>(dic, string.Empty);
            }
            catch (Exception ex)
            {
                return new ResponseDTO<Dictionary<string, object>?>(null, ex + " " + ex.Message, (int) HttpStatusCode.InternalServerError);
            }
        }

        public async Task<ResponseDTO<Dictionary<string, object>?>> Update(Guid QuestionID, Guid LessonID, Guid CourseID, Guid CreatorID)
        {
            try
            {
                Course? course = await daoCourse.getCourse(CourseID, CreatorID);
                if (course == null)
                {
                    return new ResponseDTO<Dictionary<string, object>?>(null, "Not found course", (int)HttpStatusCode.NotFound);
                }
                Lesson? lesson = await daoLesson.getLesson(LessonID);
                if (lesson == null || lesson.CourseId != CourseID)
                {
                    return new ResponseDTO<Dictionary<string, object>?>(null, "Not found lesson", (int)HttpStatusCode.NotFound);
                }
                Quiz? quiz = await daoQuiz.getQuiz(QuestionID, LessonID);
                if(quiz == null)
                {
                    return new ResponseDTO<Dictionary<string, object>?>(null, "Not found quiz", (int)HttpStatusCode.NotFound);
                }
                Dictionary<string, object> dic = new Dictionary<string, object>();
                List<Quiz> list = await daoQuiz.getList(LessonID);
                dic["LessonID"] = LessonID;
                dic["CourseID"] = CourseID;
                dic["quiz"] = quiz;
                return new ResponseDTO<Dictionary<string, object>?>(dic, string.Empty);
            }
            catch (Exception ex)
            {
                return new ResponseDTO<Dictionary<string, object>?>(null, ex + " " + ex.Message, (int)HttpStatusCode.InternalServerError);
            }
        }

    }
}
