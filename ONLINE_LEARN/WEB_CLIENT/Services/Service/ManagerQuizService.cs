using Common.Base;
using Common.Entity;
using DataAccess.Model.IDAO;
using Microsoft.EntityFrameworkCore;
using System.Net;
using WEB_CLIENT.Services.IService;

namespace WEB_CLIENT.Services.Service
{
    public class ManagerQuizService : IManagerQuizService
    {
        private readonly ICommonDAO<Course> _daoCourse;
        private readonly ICommonDAO<Lesson> _daoLesson;
        private readonly ICommonDAO<Quiz> _daoQuiz;
        public ManagerQuizService(ICommonDAO<Course> daoCourse, ICommonDAO<Lesson> daoLesson, ICommonDAO<Quiz> daoQuiz)
        {
            _daoCourse = daoCourse;
            _daoLesson = daoLesson;
            _daoQuiz = daoQuiz;
        }

        public async Task<ResponseBase<Dictionary<string, object>?>> Index(Guid LessonID, Guid CourseID, Guid CreatorID)
        {
            try
            {
                Course? course = await _daoCourse.FindAll(c => c.CourseId == CourseID && c.IsDeleted == false && c.CreatorId == CreatorID)
                   .Include(c => c.Creator).FirstOrDefaultAsync();
                if (course == null)
                {
                    return new ResponseBase<Dictionary<string, object>?>(null, "Not found course", (int)HttpStatusCode.NotFound);
                }
                Lesson? lesson = await _daoLesson.Get(l => l.IsDeleted == false && l.LessonId == LessonID
                && l.Course.IsDeleted == false && l.CourseId == CourseID);
                if (lesson == null)
                {
                    return new ResponseBase<Dictionary<string, object>?>(null, "Not found lesson", (int)HttpStatusCode.NotFound);
                }
                Dictionary<string, object> dic = new Dictionary<string, object>();
                List<Quiz> list = await _daoQuiz.FindAll(q => q.IsDeleted == false && q.LessonId == LessonID).ToListAsync();
                dic["LessonID"] = LessonID;
                dic["CourseID"] = CourseID;
                dic["list"] = list;
                return new ResponseBase<Dictionary<string, object>?>(dic, string.Empty);
            }
            catch (Exception ex)
            {
                return new ResponseBase<Dictionary<string, object>?>(null, ex + " " + ex.Message, (int)HttpStatusCode.InternalServerError);
            }
        }
        public async Task<ResponseBase<Dictionary<string, object>?>> Detail(Guid QuestionID, Guid LessonID, Guid CourseID, Guid CreatorID)
        {
            try
            {
                Course? course = await _daoCourse.FindAll(c => c.CourseId == CourseID && c.IsDeleted == false && c.CreatorId == CreatorID)
                   .Include(c => c.Creator).FirstOrDefaultAsync();
                if (course == null)
                {
                    return new ResponseBase<Dictionary<string, object>?>(null, "Not found course", (int)HttpStatusCode.NotFound);
                }
                Lesson? lesson = await _daoLesson.Get(l => l.IsDeleted == false && l.LessonId == LessonID
                && l.Course.IsDeleted == false && l.CourseId == CourseID);
                if (lesson == null)
                {
                    return new ResponseBase<Dictionary<string, object>?>(null, "Not found lesson", (int)HttpStatusCode.NotFound);
                }
                Quiz? quiz = await _daoQuiz.Get(q => q.QuestionId == QuestionID && q.IsDeleted == false && q.LessonId == LessonID);
                if (quiz == null)
                {
                    return new ResponseBase<Dictionary<string, object>?>(null, "Not found quiz", (int)HttpStatusCode.NotFound);
                }
                Dictionary<string, object> dic = new Dictionary<string, object>();
                dic["LessonID"] = LessonID;
                dic["CourseID"] = CourseID;
                dic["quiz"] = quiz;
                return new ResponseBase<Dictionary<string, object>?>(dic, string.Empty);
            }
            catch (Exception ex)
            {
                return new ResponseBase<Dictionary<string, object>?>(null, ex + " " + ex.Message, (int)HttpStatusCode.InternalServerError);
            }
        }
        public async Task<ResponseBase<Dictionary<string, Guid>?>> Create(Guid LessonID, Guid CourseID, Guid CreatorID)
        {
            try
            {
                Course? course = await _daoCourse.FindAll(c => c.CourseId == CourseID && c.IsDeleted == false && c.CreatorId == CreatorID)
                   .Include(c => c.Creator).FirstOrDefaultAsync();
                if (course == null)
                {
                    return new ResponseBase<Dictionary<string, Guid>?>(null, "Not found course", (int)HttpStatusCode.NotFound);
                }
                Lesson? lesson = await _daoLesson.Get(l => l.IsDeleted == false && l.LessonId == LessonID
                && l.Course.IsDeleted == false && l.CourseId == CourseID);
                if (lesson == null)
                {
                    return new ResponseBase<Dictionary<string, Guid>?>(null, "Not found lesson", (int)HttpStatusCode.NotFound);
                }
                Dictionary<string, Guid> dic = new Dictionary<string, Guid>();
                dic["LessonID"] = LessonID;
                dic["CourseID"] = CourseID;
                return new ResponseBase<Dictionary<string, Guid>?>(dic, string.Empty);
            }
            catch (Exception ex)
            {
                return new ResponseBase<Dictionary<string, Guid>?>(null, ex + " " + ex.Message, (int)HttpStatusCode.InternalServerError);
            }
        }
        public async Task<ResponseBase<Dictionary<string, Guid>?>> Create(Quiz create, Guid LessonID, Guid CourseID, Guid CreatorID)
        {
            try
            {
                Course? course = await _daoCourse.FindAll(c => c.CourseId == CourseID && c.IsDeleted == false && c.CreatorId == CreatorID)
                  .Include(c => c.Creator).FirstOrDefaultAsync();
                if (course == null)
                {
                    return new ResponseBase<Dictionary<string, Guid>?>(null, "Not found course", (int)HttpStatusCode.NotFound);
                }
                Lesson? lesson = await _daoLesson.Get(l => l.IsDeleted == false && l.LessonId == LessonID
                && l.Course.IsDeleted == false && l.CourseId == CourseID);
                if (lesson == null)
                {
                    return new ResponseBase<Dictionary<string, Guid>?>(null, "Not found lesson", (int)HttpStatusCode.NotFound);
                }
                Dictionary<string, Guid> dic = new Dictionary<string, Guid>();
                dic["LessonID"] = LessonID;
                dic["CourseID"] = CourseID;
                if (create.Answer3 == null && create.Answer4 == null && create.AnswerCorrect > 2)
                {
                    return new ResponseBase<Dictionary<string, Guid>?>(dic, "The question has 2 answers. The correct answer must be 1 or 2", (int)HttpStatusCode.Conflict);
                }
                if (create.Answer3 != null && create.Answer4 == null && create.AnswerCorrect > 3)
                {
                    return new ResponseBase<Dictionary<string, Guid>?>(dic, "The question has 3 answers. The correct answer must be 1,2 or 3", (int)HttpStatusCode.Conflict);
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
                await _daoQuiz.Create(create);
                await _daoQuiz.Save();
                return new ResponseBase<Dictionary<string, Guid>?>(dic, "Create successful");
            }
            catch (Exception ex)
            {
                return new ResponseBase<Dictionary<string, Guid>?>(null, ex + " " + ex.Message, (int)HttpStatusCode.InternalServerError);
            }
        }
        public async Task<ResponseBase<Dictionary<string, object>?>> Update(Guid QuestionID, Quiz obj, Guid LessonID, Guid CourseID, Guid CreatorID)
        {
            try
            {
                Course? course = await _daoCourse.FindAll(c => c.CourseId == CourseID && c.IsDeleted == false && c.CreatorId == CreatorID)
                  .Include(c => c.Creator).FirstOrDefaultAsync();
                if (course == null)
                {
                    return new ResponseBase<Dictionary<string, object>?>(null, "Not found course", (int)HttpStatusCode.NotFound);
                }
                Lesson? lesson = await _daoLesson.Get(l => l.IsDeleted == false && l.LessonId == LessonID
                && l.Course.IsDeleted == false && l.CourseId == CourseID);
                if (lesson == null)
                {
                    return new ResponseBase<Dictionary<string, object>?>(null, "Not found lesson", (int)HttpStatusCode.NotFound);
                }
                Quiz? quiz = await _daoQuiz.Get(q => q.QuestionId == QuestionID && q.IsDeleted == false && q.LessonId == LessonID);
                if (quiz == null)
                {
                    return new ResponseBase<Dictionary<string, object>?>(null, "Not found quiz", (int)HttpStatusCode.NotFound);
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
                    return new ResponseBase<Dictionary<string, object>?>(dic, "The question has 2 answers. The correct answer must be 1 or 2", (int)HttpStatusCode.Conflict);
                }
                if (obj.Answer3 != null && obj.Answer4 == null && obj.AnswerCorrect > 3)
                {
                    return new ResponseBase<Dictionary<string, object>?>(dic, "The question has 3 answers. The correct answer must be 1,2 or 3", (int)HttpStatusCode.Conflict);
                }
                quiz.UpdateAt = DateTime.Now;
                await _daoQuiz.Update(quiz);
                await _daoQuiz.Save();
                dic["quiz"] = quiz;
                return new ResponseBase<Dictionary<string, object>?>(dic, "Update successful");
            }
            catch (Exception ex)
            {
                return new ResponseBase<Dictionary<string, object>?>(null, ex + " " + ex.Message, (int)HttpStatusCode.InternalServerError);
            }
        }
        public async Task<ResponseBase<Dictionary<string, object>?>> Delete(Guid QuestionID, Guid LessonID, Guid CourseID, Guid CreatorID)
        {
            try
            {
                Course? course = await _daoCourse.FindAll(c => c.CourseId == CourseID && c.IsDeleted == false && c.CreatorId == CreatorID)
                   .Include(c => c.Creator).FirstOrDefaultAsync();
                if (course == null)
                {
                    return new ResponseBase<Dictionary<string, object>?>(null, "Not found course", (int)HttpStatusCode.NotFound);
                }
                Lesson? lesson = await _daoLesson.Get(l => l.IsDeleted == false && l.LessonId == LessonID
                && l.Course.IsDeleted == false && l.CourseId == CourseID);
                if (lesson == null)
                {
                    return new ResponseBase<Dictionary<string, object>?>(null, "Not found lesson", (int)HttpStatusCode.NotFound);
                }
                Quiz? quiz = await _daoQuiz.Get(q => q.QuestionId == QuestionID && q.IsDeleted == false && q.LessonId == LessonID);
                if (quiz == null)
                {
                    return new ResponseBase<Dictionary<string, object>?>(null, "Not found quiz", (int)HttpStatusCode.NotFound);
                }
                quiz.IsDeleted = true;
                await _daoQuiz.Update(quiz);
                await _daoQuiz.Save();
                Dictionary<string, object> dic = new Dictionary<string, object>();
                List<Quiz> list = await _daoQuiz.FindAll(q => q.IsDeleted == false && q.LessonId == LessonID).ToListAsync();
                dic["LessonID"] = LessonID;
                dic["CourseID"] = CourseID;
                dic["list"] = list;
                return new ResponseBase<Dictionary<string, object>?>(dic, "Delete successful");
            }
            catch (Exception ex)
            {
                return new ResponseBase<Dictionary<string, object>?>(null, ex + " " + ex.Message, (int)HttpStatusCode.InternalServerError);
            }
        }
    }
}
