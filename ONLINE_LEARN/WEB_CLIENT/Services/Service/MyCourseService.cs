using DataAccess.DTO;
using DataAccess.Entity;
using System.Net;
using WEB_CLIENT.Model.DAO;
using WEB_CLIENT.Services.IService;

namespace WEB_CLIENT.Services.Service
{
    public class MyCourseService : IMyCourseService
    {
        private readonly DAOEnrollCourse dao = new DAOEnrollCourse();
        public async Task<ResponseDTO<PagedResultDTO<Course>?>> Index(Guid StudentID, int page)
        {
            try
            {
                List<Course> list = await dao.getList(StudentID, page);
                int number = await dao.getNumberPage(StudentID);
                PagedResultDTO<Course> result = new PagedResultDTO<Course>()
                {
                    PageSelected = page,
                    Results = list,
                    NumberPage = number,
                    PRE_URL = "/MyCourse?page=" + (page - 1),
                    NEXT_URL = "/MyCourse?page=" + (page + 1),
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
