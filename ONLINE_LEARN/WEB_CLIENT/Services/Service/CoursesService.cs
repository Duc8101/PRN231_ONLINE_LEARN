using DataAccess.DTO;
using DataAccess.Entity;
using System.Net;
using WEB_CLIENT.Model.DAO;
using WEB_CLIENT.Services.IService;

namespace WEB_CLIENT.Services.Service
{
    public class CoursesService : ICoursesService
    {
        private readonly DAOCourse daoCourse = new DAOCourse();
        private readonly DAOCategory daoCategory = new DAOCategory();
        private readonly DAOLesson daoLesson = new DAOLesson();
        private readonly DAOEnrollCourse daoEnroll = new DAOEnrollCourse();
        private readonly DAOLessonPDF daoPDF = new DAOLessonPDF();
        private readonly DAOQuiz daoQuiz = new DAOQuiz();
        private readonly DAOLessonVideo daoVideo = new DAOLessonVideo();
        public async Task<ResponseDTO<Dictionary<string, object>?>> Index(int? CategoryID, string? properties, string? flow, int? page, Guid? CreatorID)
        { 
            int pageSelected = page == null ? 1 : page.Value;
            int prePage = pageSelected - 1;
            int nextPage = pageSelected + 1;
            string preURL;
            string nextURL;
            // if not sort
            if (properties == null && flow == null)
            {
                // if not choose category
                if (CategoryID == null)
                {
                    preURL = "/Courses" + "?page=" + prePage;
                    nextURL = "/Courses" + "?page=" + nextPage;
                }
                else
                {
                    preURL = "/Courses" + "?CategoryID=" + CategoryID + "&page=" + prePage;
                    nextURL = "/Courses" + "?CategoryID=" + CategoryID + "&page=" + nextPage;
                }
            }
            else
            {
                // if not choose category
                if (CategoryID == null)
                {
                    preURL = "/Courses" + "?properties=" + properties + "&flow=" + flow + "&page=" + prePage;
                    nextURL = "/Courses" + "?properties=" + properties + "&flow=" + flow + "&page=" + nextPage;
                }
                else
                {
                    preURL = "/Courses" + "?CategoryID=" + CategoryID + "&properties=" + properties + "&flow=" + flow + "&page=" + prePage;
                    nextURL = "/Courses" + "?CategoryID=" + CategoryID + "&properties=" + properties + "&flow=" + flow + "&page=" + nextPage;
                }
            }
            try
            {
                List<Course> listCourse = await daoCourse.getList(CategoryID, properties, flow, pageSelected, CreatorID);
                List<Category> listCategory = await daoCategory.getList();
                int numberPage = await daoCourse.getNumberPage(CategoryID, properties, flow, CreatorID);
                PagedResultDTO<Course> result = new PagedResultDTO<Course>()
                {
                    PageSelected = pageSelected,
                    Results = listCourse,
                    NumberPage = numberPage,
                    PRE_URL = preURL,
                    NEXT_URL = nextURL,
                };
                Dictionary<string, object> dic = new Dictionary<string, object>();
                dic["result"] = result;
                dic["properties"] = properties == null ? "" : properties;
                dic["flow"] = flow == null ? "" : flow;
                dic["CategoryID"] = CategoryID == null ? 0 : CategoryID;
                dic["list"] = listCategory;
                return new ResponseDTO<Dictionary<string, object>?>(dic, string.Empty);
            }
            catch (Exception ex)
            {
                return new ResponseDTO<Dictionary<string, object>?>(null, ex.Message + " " + ex, (int) HttpStatusCode.InternalServerError);
            }
        }
        public async Task<ResponseDTO<Dictionary<string, object>?>> Detail(Guid CourseID)
        {
            try
            {
                Course? course = await daoCourse.getCourse(CourseID);
                if (course == null)
                {
                    return new ResponseDTO<Dictionary<string, object>?>(null, "Not found course", (int) HttpStatusCode.NotFound);
                }
                List<Lesson> list = await daoLesson.getList(CourseID);
                Dictionary<string, object> dic = new Dictionary<string, object>();
                dic["course"] = course;
                dic["list"] = list;
                return new ResponseDTO<Dictionary<string, object>?>(dic, string.Empty);
            }
            catch (Exception ex)
            {
                return new ResponseDTO<Dictionary<string, object>?>(null, ex.Message + " " + ex, (int)HttpStatusCode.InternalServerError);
            }            
        }
        public async Task<ResponseDTO<Dictionary<string, object>?>> EnrollCourse(Guid CourseID, Guid UserID)
        {
            try
            {
                Course? course = await daoCourse.getCourse(CourseID);
                if (course == null)
                {
                    return new ResponseDTO<Dictionary<string, object>?>(null, "Not found course", (int)HttpStatusCode.NotFound);
                }
                ResponseDTO<Dictionary<string, object>?> result = await Index(null, null, null, null, null);
                // if get result failed
                if(result.Data == null)
                {
                    return new ResponseDTO<Dictionary<string, object>?>(null, result.Message, result.Code);
                }
                // if already enroll course
                if(await daoEnroll.isExist(CourseID, UserID))
                {
                    return new ResponseDTO<Dictionary<string, object>?>(result.Data, "You have enrolled this course", (int) HttpStatusCode.Conflict);
                }
                EnrollCourse enroll = new EnrollCourse()
                {
                    CourseId = CourseID,
                    StudentId = UserID,
                    CreatedAt = DateTime.Now,
                    UpdateAt = DateTime.Now,
                    IsDeleted = false,
                };
                await daoEnroll.CreateEnrollCourse(enroll);
                return new ResponseDTO<Dictionary<string, object>?>(result.Data, "Enroll successful");
            }
            catch (Exception ex)
            {
                return new ResponseDTO<Dictionary<string, object>?>(null, ex.Message + " " + ex, (int) HttpStatusCode.InternalServerError);
            }
        }
        public async Task<ResponseDTO<Dictionary<string, object>?>> LearnCourse(Guid CourseID, Guid UserID, string? video /* file video */, string? name /*video name or pdf name*/, string? PDF /*file PDF */, Guid? LessonID, int? VideoID, int? PDFID)
        {
            try
            {
                Course? course = await daoCourse.getCourse(CourseID);
                if (course == null)
                {
                    return new ResponseDTO<Dictionary<string, object>?>(null, "Not found course", (int) HttpStatusCode.NotFound);
                }
                // if enroll course
                if (await daoEnroll.isExist(CourseID, UserID))
                {
                    List<Lesson> listLesson = await daoLesson.getList(CourseID);
                    List<LessonPdf> listPDF = await daoPDF.getList();
                    List<Guid> listLessonQuiz = await daoQuiz.getListLesson();
                    List<LessonVideo> listVideo = await daoVideo.getList();      
                    // if start to learn course
                    if (video == null && PDF == null)
                    {
                        LessonVideo? lessonVideo = await daoVideo.getFirstVideo(CourseID);
                        if (lessonVideo != null)
                        {
                            video = lessonVideo.FileVideo;
                            name = lessonVideo.VideoName;
                            VideoID = lessonVideo.VideoId;
                            LessonID = lessonVideo.LessonId;
                        }
                    }
                    Dictionary<string, object> result = new Dictionary<string, object>();
                    result["listLesson"] = listLesson;
                    result["listPDF"] = listPDF;
                    result["listLessonQuiz"] = listLessonQuiz;
                    result["listVideo"] = listVideo;
                    result["video"] = video == null ? "" : video;
                    result["PDF"] = PDF == null ? "" : PDF;
                    result["name"] = name == null ? "" : name;
                    result["vID"] = VideoID == null ? 0 : VideoID;
                    result["lID"] = LessonID == null ? Guid.NewGuid() : LessonID;
                    result["pID"] = PDFID == null ? 0 : PDFID;
                    return new ResponseDTO<Dictionary<string, object>?>(result, string.Empty);
                }
                return new ResponseDTO<Dictionary<string, object>?>(null, string.Empty, (int) HttpStatusCode.Conflict);
            }
            catch (Exception ex)
            {
                return new ResponseDTO<Dictionary<string, object>?>(null, ex.Message + " " + ex, (int)HttpStatusCode.InternalServerError);
            }
        }
    }
}
