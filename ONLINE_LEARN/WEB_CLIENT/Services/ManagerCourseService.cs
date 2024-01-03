using DataAccess.DTO;
using DataAccess.Entity;
using System.Net;
using WEB_CLIENT.Model.DAO;

namespace WEB_CLIENT.Services
{
    public class ManagerCourseService
    {
        private readonly DAOCourse daoCourse = new DAOCourse();
        private readonly DAOCategory daoCategory = new DAOCategory();
        public async Task<ResponseDTO<PagedResultDTO<Course>?>> Index(int? page, Guid TeacherID)
        {
            int pageSelected = page == null ? 1 : page.Value;
            int prePage = pageSelected - 1;
            int nextPage = pageSelected + 1;
            string preURL = "/ManagerCourse?page=" + prePage;
            string nextURL = "/ManagerCourse?page=" + nextPage;
            try
            {
                List<Course> list = await daoCourse.getList(null, null, null, pageSelected, TeacherID);
                int numberPage = await daoCourse.getNumberPage(null, null, null, TeacherID);
                PagedResultDTO<Course> result = new PagedResultDTO<Course>()
                {
                    PageSelected = pageSelected,
                    Results = list,
                    NumberPage = numberPage,
                    PRE_URL = preURL,
                    NEXT_URL = nextURL,
                };
                return new ResponseDTO<PagedResultDTO<Course>?>(result, string.Empty);
            }
            catch (Exception ex)
            {
                return new ResponseDTO<PagedResultDTO<Course>?>(null, ex.Message + " " + ex, (int)HttpStatusCode.InternalServerError);
            }
        }
        public async Task<ResponseDTO<List<Category>?>> Create()
        {
            try
            {
                List<Category> list = await daoCategory.getList();
                return new ResponseDTO<List<Category>?>(list, string.Empty);
            }
            catch (Exception ex)
            {
                return new ResponseDTO<List<Category>?>(null, ex.Message + " " + ex, (int)HttpStatusCode.InternalServerError);
            }
        }
        public async Task<ResponseDTO<List<Category>?>> Create(Course course, Guid CreatorID)
        {
            try
            {
                List<Category> list = await daoCategory.getList();
                if (await daoCourse.isExist(course.CourseName.Trim(), course.CategoryId))
                {
                    return new ResponseDTO<List<Category>?>(list, "Course existed", (int)HttpStatusCode.Conflict);
                }
                course.CourseId = Guid.NewGuid();
                course.CreatorId = CreatorID;
                course.Image = course.Image.Trim();
                course.Description = course.Description == null || course.Description.Trim().Length == 0 ? null : course.Description.Trim();
                course.CreatedAt = DateTime.Now;
                course.UpdateAt = DateTime.Now;
                course.IsDeleted = false;
                await daoCourse.CreateCourse(course);
                return new ResponseDTO<List<Category>?>(list, "Create successful");
            }
            catch (Exception ex)
            {
                return new ResponseDTO<List<Category>?>(null, ex.Message + " " + ex, (int)HttpStatusCode.InternalServerError);
            }
        }
        public async Task<ResponseDTO<Dictionary<string, object>?>> Update(Guid CourseID, Guid CreatorID)
        {
            try
            {
                Course? course = await daoCourse.getCourse(CourseID, CreatorID);
                if (course == null)
                {
                    return new ResponseDTO<Dictionary<string, object>?>(null, string.Empty, (int)HttpStatusCode.NotFound);
                }
                List<Category> list = await daoCategory.getList();
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
        public async Task<ResponseDTO<Dictionary<string, object>?>> Update(Guid CourseID, Course course)
        {
            try
            {
                Course? update = await daoCourse.getCourse(CourseID);
                if (update == null)
                {
                    return new ResponseDTO<Dictionary<string, object>?>(null, "Not found course", (int)HttpStatusCode.NotFound);
                }
                update.CourseName = course.CourseName.Trim();
                update.CategoryId = course.CategoryId;
                update.Image = course.Image.Trim();
                update.Description = course.Description == null || course.Description.Trim().Length == 0 ? null : course.Description.Trim();
                List<Category> list = await daoCategory.getList();
                Dictionary<string, object> dic = new Dictionary<string, object>();
                dic["course"] = update;
                dic["list"] = list;
                if (await daoCourse.isExist(course.CourseName.Trim(), course.CategoryId, CourseID))
                {
                    return new ResponseDTO<Dictionary<string, object>?>(dic, "Course existed", (int)HttpStatusCode.Conflict);
                }
                update.UpdateAt = DateTime.Now;
                await daoCourse.UpdateCourse(update);
                dic["course"] = update;
                return new ResponseDTO<Dictionary<string, object>?>(dic, "Update successful");
            }
            catch (Exception ex)
            {
                return new ResponseDTO<Dictionary<string, object>?>(null, ex.Message + " " + ex, (int)HttpStatusCode.InternalServerError);
            }
        }
        public async Task<ResponseDTO<PagedResultDTO<Course>?>> Delete(Guid CourseID, Guid CreatorID)
        {
            try
            {
                Course? course = await daoCourse.getCourse(CourseID, CreatorID);
                if (course == null)
                {
                    return new ResponseDTO<PagedResultDTO<Course>?>(null, string.Empty, (int)HttpStatusCode.NotFound);
                }
                ResponseDTO<PagedResultDTO<Course>?> result = await Index(null, CreatorID);
                if (result.Data == null)
                {
                    return new ResponseDTO<PagedResultDTO<Course>?>(null, "Get data failed", (int)HttpStatusCode.Conflict);
                }
                await daoCourse.DeleteCourse(course);
                return new ResponseDTO<PagedResultDTO<Course>?>(result.Data, "Course name : " + course.CourseName + " deleted successfully");
            }
            catch (Exception ex)
            {
                return new ResponseDTO<PagedResultDTO<Course>?>(null, ex.Message + " " + ex, (int)HttpStatusCode.InternalServerError);
            }
        }
    }
}
