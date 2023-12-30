using DataAccess.DTO;
using DataAccess.Entity;
using System.Net;
using WEB_CLIENT.Model.DAO;
using WEB_CLIENT.Services.IService;

namespace WEB_CLIENT.Services.Service
{
    public class TakeQuizService : ITakeQuizService
    {
        private readonly DAOLesson daoLesson = new DAOLesson();
        private readonly DAOQuiz daoQuiz = new DAOQuiz();
        private readonly DAOResult daoResult = new DAOResult();

        public async Task<ResponseDTO<Dictionary<string, object?>?>> Index(Guid LessonID, Guid StudentID)
        {
            try
            {
                Lesson? lesson = await daoLesson.getLesson(LessonID);
                if(lesson == null)
                {
                    return new ResponseDTO<Dictionary<string, object?>?>(null, "Not found lesson", (int)HttpStatusCode.NotFound);
                }
                List<Quiz> list = await daoQuiz.getList(LessonID);
                Result? result = await daoResult.getResult(LessonID, StudentID);
                if(list.Count == 0)
                {
                    return new ResponseDTO<Dictionary<string, object?>?>(null, "Not found quiz", (int)HttpStatusCode.NotFound);
                }
                Dictionary<string, object?> dic = new Dictionary<string, object?>();
                dic["quiz"] = list[0];
                dic["LessonID"] = LessonID;
                dic["minutes"] = 4;
                dic["seconds"] = 59;
                dic["question_no"] = 1;
                dic["button"] = list.Count == 1 ? "Finish" : "Next";
                dic["result"] = result;
                return new ResponseDTO<Dictionary<string, object?>?>(dic, "");
            }
            catch (Exception ex)
            {
                return new ResponseDTO<Dictionary<string, object?>?>(null, ex.Message + " " + ex, (int)HttpStatusCode.InternalServerError);
            }
        }

        public async Task<ResponseDTO<List<Quiz>?>> Finish(Guid LessonID)
        {
            try
            {
                Lesson? lesson = await daoLesson.getLesson(LessonID);
                if (lesson == null)
                {
                    return new ResponseDTO<List<Quiz>?>(null, "Not found lesson", (int)HttpStatusCode.NotFound);
                }
                List<Quiz> list = await daoQuiz.getList(LessonID);
                if (list.Count == 0)
                {
                    return new ResponseDTO<List<Quiz>?>(null, "Not found quiz", (int)HttpStatusCode.NotFound);
                }
                return new ResponseDTO<List<Quiz>?>(list, "");
            }
            catch (Exception ex)
            {
                return new ResponseDTO<List<Quiz>?>(null, ex.Message + " " + ex, (int)HttpStatusCode.InternalServerError);
            }

        }

        public async Task<ResponseDTO<Result?>> Finish(Guid LessonID, Guid StudentID, float score, string status)
        {
            try
            {
                Result? result = await daoResult.getResult(LessonID, StudentID);
                if(result == null)
                {
                    result = new Result()
                    {
                        LessonId = LessonID,
                        StudentId = StudentID,
                        Score = (decimal) score,
                        Status = status,
                        CreatedAt = DateTime.Now,
                        UpdateAt = DateTime.Now,
                        IsDeleted = false
                    };
                    await daoResult.CreateResult(result);
                }
                else
                {
                    result.UpdateAt = DateTime.Now;
                    await daoResult.UpdateResult(result);
                }
                return new ResponseDTO<Result?>(result, string.Empty);
            }
            catch (Exception ex)
            {
                return new ResponseDTO<Result?>(null, ex.Message + " " + ex, (int)HttpStatusCode.InternalServerError);
            }
        }

        public async Task<ResponseDTO<Dictionary<string, object?>?>> Index(Guid LessonID, Guid StudentID, string? button, int minutes, int question_no, int seconds)
        {
            try
            {
                Lesson? lesson = await daoLesson.getLesson(LessonID);
                if (lesson == null)
                {
                    return new ResponseDTO<Dictionary<string, object?>?>(null, "Not found lesson", (int)HttpStatusCode.NotFound);
                }
                List<Quiz> list = await daoQuiz.getList(LessonID);
                Result? result = await daoResult.getResult(LessonID, StudentID);
                if (list.Count == 0)
                {
                    return new ResponseDTO<Dictionary<string, object?>?>(null, "Not found quiz", (int)HttpStatusCode.NotFound);
                }
                Dictionary<string, object?> dic = new Dictionary<string, object?>();
                // if move to previous question
                if (button != null && button.Equals("Back"))
                {
                    dic["quiz"] = list[question_no - 2];
                    dic["question_no"] = question_no - 1;
                    dic["button"] = "Next";
                    // if move to next question
                }else if (button != null && button.Equals("Next"))
                {
                    dic["quiz"] = list[question_no];
                    dic["question_no"] = question_no + 1;
                    dic["button"] = question_no + 1 == list.Count ? "Finish" : "Next";
                }
                dic["LessonID"] = LessonID;
                dic["minutes"] = minutes;
                dic["seconds"] = seconds;
                dic["result"] = result;
                return new ResponseDTO<Dictionary<string, object?>?>(dic, "");
            }
            catch (Exception ex)
            {
                return new ResponseDTO<Dictionary<string, object?>?>(null, ex.Message + " " + ex, (int)HttpStatusCode.InternalServerError);
            }
        }
    }
}
