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
    }
}
