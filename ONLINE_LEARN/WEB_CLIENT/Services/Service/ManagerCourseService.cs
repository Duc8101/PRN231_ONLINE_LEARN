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
    }
}
