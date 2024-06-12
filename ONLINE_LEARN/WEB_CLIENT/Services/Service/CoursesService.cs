using Common.Base;
using Common.Consts;
using Common.Entity;
using Common.Pagination;
using DataAccess.Model.IDAO;
using Microsoft.EntityFrameworkCore;
using System.Net;
using WEB_CLIENT.Services.IService;

namespace WEB_CLIENT.Services.Service
{
    public class CoursesService : ICoursesService
    {
        private readonly ICommonDAO<Course> _daoCourse;
        private readonly ICommonDAO<Category> _daoCategory;
        private readonly ICommonDAO<EnrollCourse> _daoEnroll;
        private readonly ICommonDAO<Lesson> _daoLesson;
        private readonly ICommonDAO<LessonPdf> _daoPDF;
        private readonly ICommonDAO<Quiz> _daoQuiz;
        private readonly ICommonDAO<LessonVideo> _daoVideo;
        public CoursesService(ICommonDAO<Course> daoCourse, ICommonDAO<Category> daoCategory, ICommonDAO<EnrollCourse> daoEnroll
            , ICommonDAO<Lesson> daoLesson, ICommonDAO<LessonPdf> daoPDF, ICommonDAO<Quiz> daoQuiz, ICommonDAO<LessonVideo> daoVideo)
        {
            _daoCourse = daoCourse;
            _daoCategory = daoCategory;
            _daoEnroll = daoEnroll;
            _daoLesson = daoLesson;
            _daoPDF = daoPDF;
            _daoQuiz = daoQuiz;
            _daoVideo = daoVideo;
        }

        private IQueryable<Course> getQuery(int? CategoryID, string? properties, string? flow)
        {
            IQueryable<Course> query = _daoCourse.FindAll(c => c.IsDeleted == false).Include(c => c.Creator);
            if (CategoryID.HasValue)
            {
                query = query.Where(c => c.CategoryId == CategoryID);
            }

            if (properties == null)
            {
                query = query.OrderByDescending(c => c.UpdateAt);
            }
            else if (flow != null)
            {
                if (flow == "asc")
                {
                    query = query.OrderBy(c => c.CourseName);
                }
                else if (flow == "desc")
                {
                    query = query.OrderByDescending(c => c.CourseName);
                }
            }
            return query;
        }
        public async Task<ResponseBase<Dictionary<string, object?>?>> Index(int? CategoryID, string? properties, string? flow, int? page, string? userId)
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
                IQueryable<Course> query = getQuery(CategoryID, properties, flow);
                List<Course> listCourse = await query.Skip(PageSizeConst.MAX_COURSE_IN_PAGE * (pageSelected - 1))
                    .Take(PageSizeConst.MAX_COURSE_IN_PAGE).ToListAsync();
                List<Category> listCategory = await _daoCategory.FindAll().ToListAsync();
                int count = await query.CountAsync();
                int numberPage = (int)Math.Ceiling((double)count / PageSizeConst.MAX_COURSE_IN_PAGE);
                PagedResult<Course> result = new PagedResult<Course>()
                {
                    PageSelected = pageSelected,
                    Results = listCourse,
                    NumberPage = numberPage,
                    PRE_URL = preURL,
                    NEXT_URL = nextURL,
                };
                List<List<Lesson>> listListLesson = new List<List<Lesson>>();
                foreach(Course course in listCourse)
                {
                    List<Lesson> listLesson = await _daoLesson.FindAll(l => l.IsDeleted == false && l.CourseId == course.CourseId).ToListAsync();
                    listListLesson.Add(listLesson);
                }          
                Dictionary<string, object?> dic = new Dictionary<string, object?>();
                dic["result"] = result;
                dic["properties"] = properties == null ? "" : properties;
                dic["flow"] = flow == null ? "" : flow;
                dic["CategoryID"] = CategoryID == null ? 0 : CategoryID;
                dic["listCategory"] = listCategory;
                dic["listListLesson"] = listListLesson;
                if (userId == null)
                {
                    dic["listEnroll"] = null;
                }
                else
                {
                    List<EnrollCourse> listEnroll = await _daoEnroll.FindAll(e => e.StudentId == Guid.Parse(userId) && e.Course.IsDeleted == false)
                        .Include(e => e.Course).ThenInclude(e => e.Creator).ToListAsync();
                    dic["listEnroll"] = listEnroll;
                }
                return new ResponseBase<Dictionary<string, object?>?>(dic, string.Empty);
            }
            catch (Exception ex)
            {
                return new ResponseBase<Dictionary<string, object?>?>(null, ex.Message + " " + ex, (int)HttpStatusCode.InternalServerError);
            }
        }
        public async Task<ResponseBase<Dictionary<string, object?>?>> Detail(Guid CourseID, string? userId)
        {
            try
            {
                Course? course = await _daoCourse.FindAll(c => c.CourseId == CourseID).Include(c => c.Creator).FirstOrDefaultAsync();
                if (course == null)
                {
                    return new ResponseBase<Dictionary<string, object?>?>(null, "Not found course", (int)HttpStatusCode.NotFound);
                }
                List<Lesson> list = await _daoLesson.FindAll(l => l.IsDeleted == false && l.CourseId == CourseID)
                    .OrderBy(l => l.LessonNo).ToListAsync();
                Dictionary<string, object?> dic = new Dictionary<string, object?>();
                dic["course"] = course;
                dic["list"] = list;
                if (userId == null)
                {
                    dic["listEnroll"] = null;
                }
                else
                {
                    dic["listEnroll"] = _daoEnroll.FindAll(e => e.StudentId == Guid.Parse(userId) && e.Course.IsDeleted == false)
                        .Include(e => e.Course).ThenInclude(e => e.Creator);
                }
                return new ResponseBase<Dictionary<string, object?>?>(dic, string.Empty);
            }
            catch (Exception ex)
            {
                return new ResponseBase<Dictionary<string, object?>?>(null, ex.Message + " " + ex, (int)HttpStatusCode.InternalServerError);
            }
        }
        public async Task<ResponseBase<Dictionary<string, object?>?>> EnrollCourse(Guid CourseID, Guid UserID)
        {
            try
            {
                Course? course = await _daoCourse.FindAll(c => c.CourseId == CourseID).Include(c => c.Creator).FirstOrDefaultAsync();
                if (course == null)
                {
                    return new ResponseBase<Dictionary<string, object?>?>(null, "Not found course", (int)HttpStatusCode.NotFound);
                }
                ResponseBase<Dictionary<string, object?>?> result = await Index(null, null, null, null ,UserID.ToString());
                // if get result failed
                if (result.Data == null)
                {
                    return new ResponseBase<Dictionary<string, object?>?>(null, result.Message, result.Code);
                }
                // if already enroll course
                if (await _daoEnroll.Any(e => e.CourseId == CourseID && e.StudentId == UserID))
                {
                    return new ResponseBase<Dictionary<string, object?>?>(result.Data, "You have enrolled this course", (int)HttpStatusCode.Conflict);
                }
                EnrollCourse enroll = new EnrollCourse()
                {
                    CourseId = CourseID,
                    StudentId = UserID,
                    CreatedAt = DateTime.Now,
                    UpdateAt = DateTime.Now,
                    IsDeleted = false,
                };
                await _daoEnroll.Create(enroll);
                await _daoEnroll.Save();
                return new ResponseBase<Dictionary<string, object?>?>(result.Data, "Enroll successful");
            }
            catch (Exception ex)
            {
                return new ResponseBase<Dictionary<string, object?>?>(null, ex.Message + " " + ex, (int)HttpStatusCode.InternalServerError);
            }
        }
        public async Task<ResponseBase<Dictionary<string, object>?>> LearnCourse(Guid CourseID, Guid UserID, string? video /* file video */, string? name /*video name or pdf name*/, string? PDF /*file PDF */, Guid? LessonID, int? VideoID, int? PDFID)
        {
            try
            {
                Course? course = await _daoCourse.FindAll(c => c.CourseId == CourseID).Include(c => c.Creator).FirstOrDefaultAsync();
                if (course == null)
                {
                    return new ResponseBase<Dictionary<string, object>?>(null, "Not found course", (int)HttpStatusCode.NotFound);
                }
                // if enroll course
                if (await _daoEnroll.Any(e => e.CourseId == CourseID && e.StudentId == UserID))
                {
                    List<Lesson> listLesson = await _daoLesson.FindAll(l => l.IsDeleted == false && l.CourseId == CourseID)
                        .OrderBy(l => l.LessonNo).ToListAsync();
                    List<LessonPdf> listPDF = await _daoPDF.FindAll(p => p.IsDeleted == false).ToListAsync();
                    List<Guid> listLessonQuiz = await _daoQuiz.FindAll(q => q.IsDeleted == false).Select(q => q.LessonId).Distinct().ToListAsync();
                    List<LessonVideo> listVideo = await _daoVideo.FindAll(v => v.IsDeleted == false).ToListAsync();
                    // if start to learn course
                    if (video == null && PDF == null)
                    {
                        LessonVideo? lessonVideo = await _daoVideo.Get(v => v.IsDeleted == false && v.Lesson.IsDeleted == false && v.Lesson.LessonNo == 1
                            && v.Lesson.CourseId == CourseID);
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
                    return new ResponseBase<Dictionary<string, object>?>(result, string.Empty);
                }
                return new ResponseBase<Dictionary<string, object>?>(null, string.Empty, (int)HttpStatusCode.Conflict);
            }
            catch (Exception ex)
            {
                return new ResponseBase<Dictionary<string, object>?>(null, ex.Message + " " + ex, (int)HttpStatusCode.InternalServerError);
            }
        }
    }
}
