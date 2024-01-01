using DataAccess.DTO;
using DataAccess.Entity;
using System.Net;
using WEB_CLIENT.Model.DAO;
using WEB_CLIENT.Services.IService;

namespace WEB_CLIENT.Services.Service
{
    public class ManagerCourseService : IManagerCourseService
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
                if(await daoCourse.isExist(course.CourseName.Trim(), course.CategoryId))
                {
                    return new ResponseDTO<List<Category>?>(list, "Course existed", (int) HttpStatusCode.Conflict);
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
    }
}
