using DataAccess.Base;
using DataAccess.Entity;
using DataAccess.Model.IDAO;
using Microsoft.EntityFrameworkCore;
using System.Net;
using WEB_CLIENT.Services.IService;

namespace WEB_CLIENT.Services.Service
{
    public class TakeQuizService : ITakeQuizService
    {
        private readonly ICommonDAO<Lesson> _daoLesson;
        private readonly ICommonDAO<Quiz> _daoQuiz;
        private readonly ICommonDAO<Result> _daoResult;
        public TakeQuizService(ICommonDAO<Lesson> daoLesson, ICommonDAO<Quiz> daoQuiz, ICommonDAO<Result> daoResult)
        {
            _daoLesson = daoLesson;
            _daoQuiz = daoQuiz;
            _daoResult = daoResult;
        }

        public async Task<ResponseBase<Dictionary<string, object?>?>> Index(Guid LessonID, Guid StudentID)
        {
            try
            {
                Lesson? lesson = await _daoLesson.Get(l => l.IsDeleted == false && l.LessonId == LessonID && l.Course.IsDeleted == false);
                if (lesson == null)
                {
                    return new ResponseBase<Dictionary<string, object?>?>(null, "Not found lesson", (int)HttpStatusCode.NotFound);
                }
                List<Quiz> list = await _daoQuiz.FindAll(q => q.IsDeleted == false && q.LessonId == LessonID).ToListAsync();
                Result? result = await _daoResult.Get(r => r.LessonId == LessonID && r.StudentId == StudentID);
                if (list.Count == 0)
                {
                    return new ResponseBase<Dictionary<string, object?>?>(null, "Not found quiz", (int)HttpStatusCode.NotFound);
                }
                Dictionary<string, object?> dic = new Dictionary<string, object?>();
                dic["quiz"] = list[0];
                dic["LessonID"] = LessonID;
                dic["minutes"] = 4;
                dic["seconds"] = 59;
                dic["question_no"] = 1;
                dic["button"] = list.Count == 1 ? "Finish" : "Next";
                dic["result"] = result;
                return new ResponseBase<Dictionary<string, object?>?>(dic, "");
            }
            catch (Exception ex)
            {
                return new ResponseBase<Dictionary<string, object?>?>(null, ex.Message + " " + ex, (int)HttpStatusCode.InternalServerError);
            }
        }

        public async Task<ResponseBase<List<Quiz>?>> Finish(Guid LessonID)
        {
            try
            {
                Lesson? lesson = await _daoLesson.Get(l => l.IsDeleted == false && l.LessonId == LessonID && l.Course.IsDeleted == false);
                if (lesson == null)
                {
                    return new ResponseBase<List<Quiz>?>(null, "Not found lesson", (int)HttpStatusCode.NotFound);
                }
                List<Quiz> list = await _daoQuiz.FindAll(q => q.IsDeleted == false && q.LessonId == LessonID).ToListAsync();
                if (list.Count == 0)
                {
                    return new ResponseBase<List<Quiz>?>(null, "Not found quiz", (int)HttpStatusCode.NotFound);
                }
                return new ResponseBase<List<Quiz>?>(list, "");
            }
            catch (Exception ex)
            {
                return new ResponseBase<List<Quiz>?>(null, ex.Message + " " + ex, (int)HttpStatusCode.InternalServerError);
            }

        }

        public async Task<ResponseBase<Result?>> Finish(Guid LessonID, Guid StudentID, float score, string status)
        {
            try
            {
                Result? result = await _daoResult.Get(r => r.LessonId == LessonID && r.StudentId == StudentID);
                if (result == null)
                {
                    result = new Result()
                    {
                        LessonId = LessonID,
                        StudentId = StudentID,
                        Score = (decimal)score,
                        Status = status,
                        CreatedAt = DateTime.Now,
                        UpdateAt = DateTime.Now,
                        IsDeleted = false
                    };
                    await _daoResult.Create(result);
                }
                else
                {
                    result.UpdateAt = DateTime.Now;
                    await _daoResult.Update(result);
                }
                await _daoResult.Save();
                return new ResponseBase<Result?>(result, string.Empty);
            }
            catch (Exception ex)
            {
                return new ResponseBase<Result?>(null, ex.Message + " " + ex, (int)HttpStatusCode.InternalServerError);
            }
        }

        public async Task<ResponseBase<Dictionary<string, object?>?>> Index(Guid LessonID, Guid StudentID, string? button, int minutes, int question_no, int seconds)
        {
            try
            {
                Lesson? lesson = await _daoLesson.Get(l => l.IsDeleted == false && l.LessonId == LessonID && l.Course.IsDeleted == false);
                if (lesson == null)
                {
                    return new ResponseBase<Dictionary<string, object?>?>(null, "Not found lesson", (int)HttpStatusCode.NotFound);
                }
                List<Quiz> list = await _daoQuiz.FindAll(q => q.IsDeleted == false && q.LessonId == LessonID).ToListAsync();
                Result? result = await _daoResult.Get(r => r.LessonId == LessonID && r.StudentId == StudentID);
                Dictionary<string, object?> dic = new Dictionary<string, object?>();
                // if move to previous question
                if (button != null && button.Equals("Back"))
                {
                    dic["quiz"] = list[question_no - 2];
                    dic["question_no"] = question_no - 1;
                    dic["button"] = "Next";
                    // if move to next question
                }
                else if (button != null && button.Equals("Next"))
                {
                    dic["quiz"] = list[question_no];
                    dic["question_no"] = question_no + 1;
                    dic["button"] = question_no + 1 == list.Count ? "Finish" : "Next";
                }
                dic["LessonID"] = LessonID;
                dic["minutes"] = minutes;
                dic["seconds"] = seconds;
                dic["result"] = result;
                return new ResponseBase<Dictionary<string, object?>?>(dic, "");
            }
            catch (Exception ex)
            {
                return new ResponseBase<Dictionary<string, object?>?>(null, ex.Message + " " + ex, (int)HttpStatusCode.InternalServerError);
            }
        }
    }
}
