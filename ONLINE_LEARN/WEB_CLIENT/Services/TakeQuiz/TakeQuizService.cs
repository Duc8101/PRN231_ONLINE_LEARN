using Common.Base;
using Common.Entity;
using DataAccess.Model.DAO;
using System.Net;

namespace WEB_CLIENT.Services.TakeQuiz
{
    public class TakeQuizService : ITakeQuizService
    {
        private readonly DAOLesson _daoLesson;
        private readonly DAOQuiz _daoQuiz;
        private readonly DAOResult _daoResult;
        public TakeQuizService(DAOLesson daoLesson, DAOQuiz daoQuiz, DAOResult daoResult)
        {
            _daoLesson = daoLesson;
            _daoQuiz = daoQuiz;
            _daoResult = daoResult;
        }

        public ResponseBase<Dictionary<string, object?>?> Index(Guid lessonId, Guid studentId)
        {
            try
            {
                Lesson? lesson = _daoLesson.getLesson(lessonId);
                if (lesson == null)
                {
                    return new ResponseBase<Dictionary<string, object?>?>("Not found lesson", (int)HttpStatusCode.NotFound);
                }
                List<Quiz> list = _daoQuiz.getListQuiz(lessonId);
                Result? result = _daoResult.getResult(lessonId, studentId);
                if (list.Count == 0)
                {
                    return new ResponseBase<Dictionary<string, object?>?>("Not found quiz", (int)HttpStatusCode.NotFound);
                }
                Dictionary<string, object?> dic = new Dictionary<string, object?>();
                dic["quiz"] = list[0];
                dic["lessonId"] = lessonId;
                dic["minutes"] = 4;
                dic["seconds"] = 59;
                dic["question_no"] = 1;
                dic["button"] = list.Count == 1 ? "Finish" : "Next";
                dic["result"] = result;
                return new ResponseBase<Dictionary<string, object?>?>(dic);
            }
            catch (Exception ex)
            {
                return new ResponseBase<Dictionary<string, object?>?>(ex.Message + " " + ex, (int)HttpStatusCode.InternalServerError);
            }
        }

        public ResponseBase<List<Quiz>?> Finish(Guid lessonId)
        {
            try
            {
                Lesson? lesson = _daoLesson.getLesson(lessonId);
                if (lesson == null)
                {
                    return new ResponseBase<List<Quiz>?>("Not found lesson", (int)HttpStatusCode.NotFound);
                }
                List<Quiz> list = _daoQuiz.getListQuiz(lessonId);
                if (list.Count == 0)
                {
                    return new ResponseBase<List<Quiz>?>("Not found quiz", (int)HttpStatusCode.NotFound);
                }
                return new ResponseBase<List<Quiz>?>(list);
            }
            catch (Exception ex)
            {
                return new ResponseBase<List<Quiz>?>(ex.Message + " " + ex, (int)HttpStatusCode.InternalServerError);
            }

        }

        public ResponseBase<Result?> Finish(Guid lessonId, Guid studentId, float score, string status)
        {
            try
            {
                Result? result = _daoResult.getResult(lessonId, studentId);
                if (result == null)
                {
                    result = new Result()
                    {
                        LessonId = lessonId,
                        StudentId = studentId,
                        Score = (decimal)score,
                        Status = status,
                        CreatedAt = DateTime.Now,
                        UpdateAt = DateTime.Now,
                        IsDeleted = false
                    };
                    _daoResult.CreateResult(result);
                }
                else
                {
                    result.UpdateAt = DateTime.Now;
                    _daoResult.UpdateResult(result);
                }
                return new ResponseBase<Result?>(result);
            }
            catch (Exception ex)
            {
                return new ResponseBase<Result?>(ex.Message + " " + ex, (int)HttpStatusCode.InternalServerError);
            }
        }

        public ResponseBase<Dictionary<string, object?>?> Index(Guid lessonId, Guid studentId, string? button, int minutes, int question_no, int seconds)
        {
            try
            {
                Lesson? lesson = _daoLesson.getLesson(lessonId);
                if (lesson == null)
                {
                    return new ResponseBase<Dictionary<string, object?>?>("Not found lesson", (int)HttpStatusCode.NotFound);
                }
                List<Quiz> list = _daoQuiz.getListQuiz(lessonId);
                Result? result = _daoResult.getResult(lessonId, studentId);
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
                dic["lessonId"] = lessonId;
                dic["minutes"] = minutes;
                dic["seconds"] = seconds;
                dic["result"] = result;
                return new ResponseBase<Dictionary<string, object?>?>(dic);
            }
            catch (Exception ex)
            {
                return new ResponseBase<Dictionary<string, object?>?>(ex.Message + " " + ex, (int)HttpStatusCode.InternalServerError);
            }
        }
    }
}
