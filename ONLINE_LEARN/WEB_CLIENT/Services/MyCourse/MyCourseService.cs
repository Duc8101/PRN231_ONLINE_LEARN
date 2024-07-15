using Common.Base;
using Common.Entity;
using Common.Paginations;
using DataAccess.Model.DAO;
using System.Net;

namespace WEB_CLIENT.Services.MyCourse
{
    public class MyCourseService : IMyCourseService
    {
        private readonly DAOEnrollCourse _daoEnroll;
        public MyCourseService(DAOEnrollCourse daoEnroll)
        {
            _daoEnroll = daoEnroll;
        }

        public ResponseBase<Pagination<Course>?> Index(Guid studentId, int page)
        {
            try
            {
                List<Course> list = _daoEnroll.getListCourse(studentId, page);
                int number = _daoEnroll.getNumberPage(studentId);
                Pagination<Course> data = new Pagination<Course>()
                {
                    PageSelected = page,
                    List = list,
                    NumberPage = number,
                    PRE_URL = "/MyCourse?page=" + (page - 1),
                    NEXT_URL = "/MyCourse?page=" + (page + 1),
                };
                return new ResponseBase<Pagination<Course>?>(data);
            }
            catch (Exception ex)
            {
                return new ResponseBase<Pagination<Course>?>(ex.Message + " " + ex, (int)HttpStatusCode.InternalServerError);
            }
        }
    }
}
