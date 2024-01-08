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
                return new ResponseDTO<Dictionary<string, object>?>(null, ex + " " + ex.Message, (int)HttpStatusCode.InternalServerError);
            }
        }
        public async Task<ResponseDTO<Dictionary<string, object>?>> Detail(Guid QuestionID, Guid LessonID, Guid CourseID, Guid CreatorID)
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
                if (quiz == null)
                {
                    return new ResponseDTO<Dictionary<string, object>?>(null, "Not found quiz", (int)HttpStatusCode.NotFound);
                }
                Dictionary<string, object> dic = new Dictionary<string, object>();
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
        public async Task<ResponseDTO<Dictionary<string, Guid>?>> Create(Guid LessonID, Guid CourseID, Guid CreatorID)
        {
            try
            {
                Course? course = await daoCourse.getCourse(CourseID, CreatorID);
                if (course == null)
                {
                    return new ResponseDTO<Dictionary<string, Guid>?>(null, "Not found course", (int)HttpStatusCode.NotFound);
                }
                Lesson? lesson = await daoLesson.getLesson(LessonID);
                if (lesson == null || lesson.CourseId != CourseID)
                {
                    return new ResponseDTO<Dictionary<string, Guid>?>(null, "Not found lesson", (int)HttpStatusCode.NotFound);
                }
                Dictionary<string, Guid> dic = new Dictionary<string, Guid>();
                dic["LessonID"] = LessonID;
                dic["CourseID"] = CourseID;
                return new ResponseDTO<Dictionary<string, Guid>?>(dic, string.Empty);
            }
            catch (Exception ex)
            {
                return new ResponseDTO<Dictionary<string, Guid>?>(null, ex + " " + ex.Message, (int)HttpStatusCode.InternalServerError);
            }
        }
        public async Task<ResponseDTO<Dictionary<string, Guid>?>> Create(Quiz create, Guid LessonID, Guid CourseID, Guid CreatorID)
        {
            try
            {
                Course? course = await daoCourse.getCourse(CourseID, CreatorID);
                if (course == null)
                {
                    return new ResponseDTO<Dictionary<string, Guid>?>(null, "Not found course", (int)HttpStatusCode.NotFound);
                }
                Lesson? lesson = await daoLesson.getLesson(LessonID);
                if (lesson == null || lesson.CourseId != CourseID)
                {
                    return new ResponseDTO<Dictionary<string, Guid>?>(null, "Not found lesson", (int)HttpStatusCode.NotFound);
                }
                Dictionary<string, Guid> dic = new Dictionary<string, Guid>();
                dic["LessonID"] = LessonID;
                dic["CourseID"] = CourseID;
                if (create.Answer3 == null && create.Answer4 == null && create.AnswerCorrect > 2)
                {
                    return new ResponseDTO<Dictionary<string, Guid>?>(dic, "The question has 2 answers. The correct answer must be 1 or 2", (int)HttpStatusCode.Conflict);
                }
                if (create.Answer3 != null && create.Answer4 == null && create.AnswerCorrect > 3)
                {
                    return new ResponseDTO<Dictionary<string, Guid>?>(dic, "The question has 3 answers. The correct answer must be 1,2 or 3", (int)HttpStatusCode.Conflict);
                }
                create.Question = create.Question.Trim();
                create.Answer1 = create.Answer1.Trim();
                create.Answer2 = create.Answer2.Trim();
                create.Answer3 = create.Answer3 == null ? null : create.Answer3.Trim();
                create.Answer4 = create.Answer4 == null ? null : create.Answer4.Trim();
                create.QuestionId = Guid.NewGuid();
                create.CreatedAt = DateTime.Now;
                create.UpdateAt = DateTime.Now;
                create.IsDeleted = false;
                await daoQuiz.CreateQuiz(create);
                return new ResponseDTO<Dictionary<string, Guid>?>(dic, "Create successful");
            }
            catch (Exception ex)
            {
                return new ResponseDTO<Dictionary<string, Guid>?>(null, ex + " " + ex.Message, (int)HttpStatusCode.InternalServerError);
            }
        }
        public async Task<ResponseDTO<Dictionary<string, object>?>> Update(Guid QuestionID, Quiz obj, Guid LessonID, Guid CourseID, Guid CreatorID)
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
                if (quiz == null)
                {
                    return new ResponseDTO<Dictionary<string, object>?>(null, "Not found quiz", (int)HttpStatusCode.NotFound);
                }
                Dictionary<string, object> dic = new Dictionary<string, object>();
                dic["LessonID"] = LessonID;
                dic["CourseID"] = CourseID;
                quiz.Question = obj.Question.Trim();
                quiz.Answer1 = obj.Answer1.Trim();
                quiz.Answer2 = obj.Answer2.Trim();
                quiz.Answer3 = obj.Answer3 == null ? null : obj.Answer3.Trim();
                quiz.Answer4 = obj.Answer4 == null ? null : obj.Answer4.Trim();
                quiz.AnswerCorrect = obj.AnswerCorrect;
                dic["quiz"] = quiz;
                if (obj.Answer3 == null && obj.Answer4 == null && obj.AnswerCorrect > 2)
                {
                    return new ResponseDTO<Dictionary<string, object>?>(dic, "The question has 2 answers. The correct answer must be 1 or 2", (int)HttpStatusCode.Conflict);
                }
                if (obj.Answer3 != null && obj.Answer4 == null && obj.AnswerCorrect > 3)
                {
                    return new ResponseDTO<Dictionary<string, object>?>(dic, "The question has 3 answers. The correct answer must be 1,2 or 3", (int)HttpStatusCode.Conflict);
                }
                Console.WriteLine(obj.Question);
                quiz.UpdateAt = DateTime.Now;
                await daoQuiz.UpdateQuiz(quiz);
                dic["quiz"] = quiz;
                return new ResponseDTO<Dictionary<string, object>?>(dic, "Update successful");
            }
            catch (Exception ex)
            {
                return new ResponseDTO<Dictionary<string, object>?>(null, ex + " " + ex.Message, (int)HttpStatusCode.InternalServerError);
            }
        }
        public async Task<ResponseDTO<Dictionary<string, object>?>> Delete(Guid QuestionID, Guid LessonID, Guid CourseID, Guid CreatorID)
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
                if (quiz == null)
                {
                    return new ResponseDTO<Dictionary<string, object>?>(null, "Not found quiz", (int)HttpStatusCode.NotFound);
                }
                await daoQuiz.DeleteQuiz(quiz);
                Dictionary<string, object> dic = new Dictionary<string, object>();
                List<Quiz> list = await daoQuiz.getList(LessonID);
                dic["LessonID"] = LessonID;
                dic["CourseID"] = CourseID;
                dic["list"] = list;
                return new ResponseDTO<Dictionary<string, object>?>(dic, "Delete successful");
            }
            catch (Exception ex)
            {
                return new ResponseDTO<Dictionary<string, object>?>(null, ex + " " + ex.Message, (int)HttpStatusCode.InternalServerError);
            }
        }
    }
}
