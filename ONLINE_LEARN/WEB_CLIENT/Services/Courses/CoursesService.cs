using Common.Base;
using Common.Entity;
using Common.Paginations;
using DataAccess.Model.DAO;
using System.Net;

namespace WEB_CLIENT.Services.Courses
{
    public class CoursesService : ICoursesService
    {
        private readonly DAOCourse _daoCourse;
        private readonly DAOCategory _daoCategory;
        private readonly DAOEnrollCourse _daoEnroll;
        private readonly DAOLesson _daoLesson;
        private readonly DAOLessonPDF _daoPDF;
        private readonly DAOLessonVideo _daoVideo;
        public CoursesService(DAOCourse daoCourse, DAOCategory daoCategory, DAOEnrollCourse daoEnroll, DAOLesson daoLesson
            , DAOLessonPDF daoPDF, DAOLessonVideo daoVideo)
        {
            _daoCourse = daoCourse;
            _daoCategory = daoCategory;
            _daoEnroll = daoEnroll;
            _daoLesson = daoLesson;
            _daoPDF = daoPDF;
            _daoVideo = daoVideo;
        }

        public ResponseBase<Dictionary<string, object?>?> Index(int? categoryId, string? properties, bool? asc, int? page, string? userId)
        {
            int pageSelected = page == null ? 1 : page.Value;
            int prePage = pageSelected - 1;
            int nextPage = pageSelected + 1;
            string preURL;
            string nextURL;
            // if not sort
            if (properties == null && asc == null)
            {
                // if not choose category
                if (categoryId == null)
                {
                    preURL = "/Courses" + "?page=" + prePage;
                    nextURL = "/Courses" + "?page=" + nextPage;
                }
                else
                {
                    preURL = "/Courses" + "?categoryId=" + categoryId + "&page=" + prePage;
                    nextURL = "/Courses" + "?categoryId=" + categoryId + "&page=" + nextPage;
                }
            }
            else
            {
                // if not choose category
                if (categoryId == null)
                {
                    preURL = "/Courses" + "?properties=" + properties + "&asc=" + asc + "&page=" + prePage;
                    nextURL = "/Courses" + "?properties=" + properties + "&asc=" + asc + "&page=" + nextPage;
                }
                else
                {
                    preURL = "/Courses" + "?categoryId=" + categoryId + "&properties=" + properties + "&asc=" + asc + "&page=" + prePage;
                    nextURL = "/Courses" + "?categoryId=" + categoryId + "&properties=" + properties + "&asc=" + asc + "&page=" + nextPage;
                }
            }

            try
            {
                List<Course> listCourse = _daoCourse.getListCourse(categoryId, properties, asc, null, pageSelected);
                List<Category> listCategory = _daoCategory.getListCategory();
                int numberPage = _daoCourse.getNumberPage(categoryId, properties, asc, null);
                Pagination<Course> result = new Pagination<Course>()
                {
                    PageSelected = pageSelected,
                    List = listCourse,
                    NumberPage = numberPage,
                    PRE_URL = preURL,
                    NEXT_URL = nextURL,
                };
                List<List<Lesson>> listListLesson = new List<List<Lesson>>();
                foreach (Course course in listCourse)
                {
                    List<Lesson> listLesson = _daoLesson.getListLesson(course.CourseId);
                    listListLesson.Add(listLesson);
                }
                Dictionary<string, object?> data = new Dictionary<string, object?>();
                data["result"] = result;
                data["properties"] = properties == null ? "" : properties;
                data["asc"] = asc;
                data["categoryId"] = categoryId == null ? 0 : categoryId;
                data["listCategory"] = listCategory;
                data["listListLesson"] = listListLesson;
                if (userId == null)
                {
                    data["listEnroll"] = null;
                }
                else
                {
                    List<EnrollCourse> listEnroll = _daoEnroll.getListEnrollCourse(Guid.Parse(userId));
                    data["listEnroll"] = listEnroll;
                }
                return new ResponseBase<Dictionary<string, object?>?>(data);
            }
            catch (Exception ex)
            {
                return new ResponseBase<Dictionary<string, object?>?>(ex.Message + " " + ex, (int)HttpStatusCode.InternalServerError);
            }
        }
        public ResponseBase<Dictionary<string, object?>?> Detail(Guid courseId, string? userId)
        {
            try
            {
                Course? course = _daoCourse.getCourse(courseId);
                if (course == null)
                {
                    return new ResponseBase<Dictionary<string, object?>?>("Not found course", (int)HttpStatusCode.NotFound);
                }
                List<Lesson> list = _daoLesson.getListLesson(courseId);
                Dictionary<string, object?> dic = new Dictionary<string, object?>();
                dic["course"] = course;
                dic["list"] = list;
                if (userId == null)
                {
                    dic["listEnroll"] = null;
                }
                else
                {
                    dic["listEnroll"] = _daoEnroll.getListEnrollCourse(Guid.Parse(userId));
                }
                return new ResponseBase<Dictionary<string, object?>?>(dic);
            }
            catch (Exception ex)
            {
                return new ResponseBase<Dictionary<string, object?>?>(ex.Message + " " + ex, (int)HttpStatusCode.InternalServerError);
            }
        }
        public ResponseBase<Dictionary<string, object?>?> EnrollCourse(Guid courseId, Guid userId)
        {
            try
            {
                Course? course = _daoCourse.getCourse(courseId);
                if (course == null)
                {
                    return new ResponseBase<Dictionary<string, object?>?>("Not found course", (int)HttpStatusCode.NotFound);
                }
                ResponseBase<Dictionary<string, object?>?> result = Index(null, null, null, null, userId.ToString());
                // if get result failed
                if (result.Data == null)
                {
                    return new ResponseBase<Dictionary<string, object?>?>(result.Message, result.Code);
                }
                // if already enroll course
                if (_daoEnroll.isExist(courseId, userId))
                {
                    return new ResponseBase<Dictionary<string, object?>?>(result.Data, "You have enrolled this course", (int)HttpStatusCode.Conflict);
                }
                EnrollCourse enroll = new EnrollCourse()
                {
                    CourseId = courseId,
                    StudentId = userId,
                    CreatedAt = DateTime.Now,
                    UpdateAt = DateTime.Now,
                    IsDeleted = false,
                };
                _daoEnroll.CreateEnrollCourse(enroll);
                return new ResponseBase<Dictionary<string, object?>?>(result.Data, "Enroll successful");
            }
            catch (Exception ex)
            {
                return new ResponseBase<Dictionary<string, object?>?>(ex.Message + " " + ex, (int)HttpStatusCode.InternalServerError);
            }
        }
        public ResponseBase<Dictionary<string, object>?> LearnCourse(Guid courseId, Guid userId, string? video /* file video */, string? name /*video name or pdf name*/, string? PDF /*file PDF */, Guid? lessonId, int? videoId, int? PDFId)
        {
            try
            {
                Course? course = _daoCourse.getCourse(courseId);
                if (course == null)
                {
                    return new ResponseBase<Dictionary<string, object>?>("Not found course", (int)HttpStatusCode.NotFound);
                }
                // if enroll course
                if (_daoEnroll.isExist(courseId, userId))
                {
                    List<Lesson> listLessonCourse = _daoLesson.getListLesson(courseId);
                    List<LessonPdf> listPDF = _daoPDF.getListPDF();
                    List<Lesson> listLessonQuiz = _daoLesson.getListLesson();
                    List<LessonVideo> listVideo = _daoVideo.getListVideo();
                    // if start to learn course
                    if (video == null && PDF == null)
                    {
                        LessonVideo? lessonVideo = _daoVideo.getVideo(courseId);
                        if (lessonVideo != null)
                        {
                            video = lessonVideo.FileVideo;
                            name = lessonVideo.VideoName;
                            videoId = lessonVideo.VideoId;
                            lessonId = lessonVideo.LessonId;
                        }
                    }
                    Dictionary<string, object> result = new Dictionary<string, object>();
                    result["listLessonCourse"] = listLessonCourse;
                    result["listPDF"] = listPDF;
                    result["listLessonQuiz"] = listLessonQuiz;
                    result["listVideo"] = listVideo;
                    result["video"] = video == null ? "" : video;
                    result["PDF"] = PDF == null ? "" : PDF;
                    result["name"] = name == null ? "" : name;
                    result["vId"] = videoId == null ? 0 : videoId;
                    result["lId"] = lessonId == null ? Guid.NewGuid() : lessonId;
                    result["pId"] = PDFId == null ? 0 : PDFId;
                    return new ResponseBase<Dictionary<string, object>?>(result);
                }
                return new ResponseBase<Dictionary<string, object>?>(string.Empty, (int)HttpStatusCode.Conflict);
            }
            catch (Exception ex)
            {
                return new ResponseBase<Dictionary<string, object>?>(ex.Message + " " + ex, (int)HttpStatusCode.InternalServerError);
            }
        }
    }
}
