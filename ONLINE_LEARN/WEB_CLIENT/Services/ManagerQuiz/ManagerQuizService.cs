using AutoMapper;
using Common.Base;
using Common.DTO.QuizDTO;
using Common.Entity;
using DataAccess.Model.DAO;
using System.Net;
using WEB_CLIENT.Services.Base;

namespace WEB_CLIENT.Services.ManagerQuiz
{
    public class ManagerQuizService : BaseService, IManagerQuizService
    {
        private readonly DAOCourse _daoCourse;
        private readonly DAOLesson _daoLesson;
        private readonly DAOQuiz _daoQuiz;

        public ManagerQuizService(IMapper mapper, DAOCourse daoCourse, DAOLesson daoLesson, DAOQuiz daoQuiz) : base(mapper)
        {
            _daoCourse = daoCourse;
            _daoLesson = daoLesson;
            _daoQuiz = daoQuiz;
        }

        public ResponseBase<Dictionary<string, object>?> Index(Guid lessonId, Guid courseId, Guid creatorId)
        {
            try
            {
                Course? course = _daoCourse.getCourse(courseId, creatorId);
                if (course == null)
                {
                    return new ResponseBase<Dictionary<string, object>?>("Not found course", (int)HttpStatusCode.NotFound);
                }
                Lesson? lesson = _daoLesson.getLesson(lessonId, courseId);
                if (lesson == null)
                {
                    return new ResponseBase<Dictionary<string, object>?>("Not found lesson", (int)HttpStatusCode.NotFound);
                }
                List<Quiz> list = _daoQuiz.getListQuiz(lessonId);
                Dictionary<string, object> data = new Dictionary<string, object>()
                {
                    {"lessonId", lessonId },
                    {"courseId", courseId },
                    {"list", list },
                };
                return new ResponseBase<Dictionary<string, object>?>(data);
            }
            catch (Exception ex)
            {
                return new ResponseBase<Dictionary<string, object>?>(ex + " " + ex.Message, (int)HttpStatusCode.InternalServerError);
            }
        }

        public ResponseBase<Dictionary<string, object>?> Detail(Guid questionId, Guid lessonId, Guid courseId, Guid creatorId)
        {
            try
            {
                Course? course = _daoCourse.getCourse(courseId, creatorId);
                if (course == null)
                {
                    return new ResponseBase<Dictionary<string, object>?>("Not found course", (int)HttpStatusCode.NotFound);
                }
                Lesson? lesson = _daoLesson.getLesson(lessonId, courseId);
                if (lesson == null)
                {
                    return new ResponseBase<Dictionary<string, object>?>("Not found lesson", (int)HttpStatusCode.NotFound);
                }
                Quiz? quiz = _daoQuiz.getQuiz(questionId, lessonId);
                if (quiz == null)
                {
                    return new ResponseBase<Dictionary<string, object>?>("Not found quiz", (int)HttpStatusCode.NotFound);
                }
                Dictionary<string, object> data = new Dictionary<string, object>()
                {
                    {"lessonId", lessonId },
                    {"courseId", courseId },
                    {"quiz", quiz },
                };
                return new ResponseBase<Dictionary<string, object>?>(data);
            }
            catch (Exception ex)
            {
                return new ResponseBase<Dictionary<string, object>?>(ex + " " + ex.Message, (int)HttpStatusCode.InternalServerError);
            }
        }

        public ResponseBase<Dictionary<string, Guid>?> Create(Guid lessonId, Guid courseId, Guid creatorId)
        {
            try
            {
                Course? course = _daoCourse.getCourse(courseId, creatorId);
                if (course == null)
                {
                    return new ResponseBase<Dictionary<string, Guid>?>("Not found course", (int)HttpStatusCode.NotFound);
                }
                Lesson? lesson = _daoLesson.getLesson(lessonId, courseId);
                if (lesson == null)
                {
                    return new ResponseBase<Dictionary<string, Guid>?>("Not found lesson", (int)HttpStatusCode.NotFound);
                }
                Dictionary<string, Guid> data = new Dictionary<string, Guid>()
                {
                    {"lessonId", lessonId },
                    {"courseId", courseId },
                };
                return new ResponseBase<Dictionary<string, Guid>?>(data);
            }
            catch (Exception ex)
            {
                return new ResponseBase<Dictionary<string, Guid>?>(ex + " " + ex.Message, (int)HttpStatusCode.InternalServerError);
            }
        }

        public ResponseBase<Dictionary<string, Guid>?> Create(QuizCreateUpdateDTO DTO, Guid courseId, Guid creatorId)
        {
            try
            {
                Course? course = _daoCourse.getCourse(courseId, creatorId);
                if (course == null)
                {
                    return new ResponseBase<Dictionary<string, Guid>?>("Not found course", (int)HttpStatusCode.NotFound);
                }
                Lesson? lesson = _daoLesson.getLesson(DTO.LessonId, courseId);
                if (lesson == null)
                {
                    return new ResponseBase<Dictionary<string, Guid>?>("Not found lesson", (int)HttpStatusCode.NotFound);
                }
                Dictionary<string, Guid> data = new Dictionary<string, Guid>()
                {
                    {"lessonId", DTO.LessonId },
                    {"courseId", courseId },
                };
                if (DTO.Answer3 == null && DTO.Answer4 == null && DTO.AnswerCorrect > 2)
                {
                    return new ResponseBase<Dictionary<string, Guid>?>(data, "The question has 2 answers. The correct answer must be 1 or 2", (int)HttpStatusCode.Conflict);
                }
                if (DTO.Answer3 != null && DTO.Answer4 == null && DTO.AnswerCorrect > 3)
                {
                    return new ResponseBase<Dictionary<string, Guid>?>(data, "The question has 3 answers. The correct answer must be 1,2 or 3", (int)HttpStatusCode.Conflict);
                }
                Quiz quiz = _mapper.Map<Quiz>(DTO);
                quiz.QuestionId = Guid.NewGuid();
                quiz.CreatedAt = DateTime.Now;
                quiz.UpdateAt = DateTime.Now;
                quiz.IsDeleted = false;
                _daoQuiz.CreateQuiz(quiz);
                return new ResponseBase<Dictionary<string, Guid>?>(data, "Create successful");
            }
            catch (Exception ex)
            {
                return new ResponseBase<Dictionary<string, Guid>?>(ex + " " + ex.Message, (int)HttpStatusCode.InternalServerError);
            }
        }

        public ResponseBase<Dictionary<string, object>?> Update(Guid questionId, QuizCreateUpdateDTO DTO, Guid courseId, Guid creatorId)
        {
            try
            {
                Course? course = _daoCourse.getCourse(courseId, creatorId);
                if (course == null)
                {
                    return new ResponseBase<Dictionary<string, object>?>("Not found course", (int)HttpStatusCode.NotFound);
                }
                Lesson? lesson = _daoLesson.getLesson(DTO.LessonId, courseId);
                if (lesson == null)
                {
                    return new ResponseBase<Dictionary<string, object>?>("Not found lesson", (int)HttpStatusCode.NotFound);
                }
                Quiz? quiz = _daoQuiz.getQuiz(questionId, DTO.LessonId);
                if (quiz == null)
                {
                    return new ResponseBase<Dictionary<string, object>?>("Not found quiz", (int)HttpStatusCode.NotFound);
                }
                Dictionary<string, object> data = new Dictionary<string, object>()
                {
                    {"lessonId", DTO.LessonId },
                    {"courseId", courseId },
                };
                quiz.Question = DTO.Question.Trim();
                quiz.Answer1 = DTO.Answer1.Trim();
                quiz.Answer2 = DTO.Answer2.Trim();
                quiz.Answer3 = DTO.Answer3 == null ? null : DTO.Answer3.Trim();
                quiz.Answer4 = DTO.Answer4 == null ? null : DTO.Answer4.Trim();
                quiz.AnswerCorrect = DTO.AnswerCorrect;
                data["quiz"] = quiz;
                if (DTO.Answer3 == null && DTO.Answer4 == null && DTO.AnswerCorrect > 2)
                {
                    return new ResponseBase<Dictionary<string, object>?>(data, "The question has 2 answers. The correct answer must be 1 or 2", (int)HttpStatusCode.Conflict);
                }
                if (DTO.Answer3 != null && DTO.Answer4 == null && DTO.AnswerCorrect > 3)
                {
                    return new ResponseBase<Dictionary<string, object>?>(data, "The question has 3 answers. The correct answer must be 1,2 or 3", (int)HttpStatusCode.Conflict);
                }
                quiz.UpdateAt = DateTime.Now;
                _daoQuiz.UpdateQuiz(quiz);
                data["quiz"] = quiz;
                return new ResponseBase<Dictionary<string, object>?>(data, "Update successful");
            }
            catch (Exception ex)
            {
                return new ResponseBase<Dictionary<string, object>?>(ex + " " + ex.Message, (int)HttpStatusCode.InternalServerError);
            }
        }

        public ResponseBase<Dictionary<string, object>?> Delete(Guid questionId, Guid lessonId, Guid courseId, Guid creatorId)
        {
            try
            {
                Course? course = _daoCourse.getCourse(courseId, creatorId);
                if (course == null)
                {
                    return new ResponseBase<Dictionary<string, object>?>("Not found course", (int)HttpStatusCode.NotFound);
                }
                Lesson? lesson = _daoLesson.getLesson(lessonId, courseId);
                if (lesson == null)
                {
                    return new ResponseBase<Dictionary<string, object>?>("Not found lesson", (int)HttpStatusCode.NotFound);
                }
                Quiz? quiz = _daoQuiz.getQuiz(questionId, lessonId);
                if (quiz == null)
                {
                    return new ResponseBase<Dictionary<string, object>?>("Not found quiz", (int)HttpStatusCode.NotFound);
                }
                _daoQuiz.DeleteQuiz(quiz);
                List<Quiz> list = _daoQuiz.getListQuiz(lessonId);
                Dictionary<string, object> data = new Dictionary<string, object>()
                {
                    {"lessonId", lessonId },
                    {"courseId", courseId },
                    {"list", list },
                };
                return new ResponseBase<Dictionary<string, object>?>(data, "Delete successful");
            }
            catch (Exception ex)
            {
                return new ResponseBase<Dictionary<string, object>?>(ex + " " + ex.Message, (int)HttpStatusCode.InternalServerError);
            }
        }
    }
}
