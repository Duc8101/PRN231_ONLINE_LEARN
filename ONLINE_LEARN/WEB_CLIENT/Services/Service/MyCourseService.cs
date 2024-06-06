using DataAccess.Base;
using DataAccess.Const;
using DataAccess.Entity;
using DataAccess.Model.IDAO;
using DataAccess.Pagination;
using Microsoft.EntityFrameworkCore;
using System.Net;
using WEB_CLIENT.Services.IService;

namespace WEB_CLIENT.Services.Service
{
    public class MyCourseService : IMyCourseService
    {
        private readonly ICommonDAO<EnrollCourse> _daoEnroll;
        public MyCourseService(ICommonDAO<EnrollCourse> daoEnroll)
        {
            _daoEnroll = daoEnroll;
        }
        public async Task<ResponseBase<PagedResult<Course>?>> Index(Guid StudentID, int page)
        {
            try
            {
                IQueryable<EnrollCourse> query = _daoEnroll.FindAll(e => e.StudentId == StudentID && e.Course.IsDeleted == false)
                    .Include(e => e.Course).ThenInclude(e => e.Creator);
                List<Course> list = await query.OrderByDescending(e => e.Course.UpdateAt).Skip(PageSizeConst.MAX_COURSE_IN_PAGE * (page - 1))
                    .Take(PageSizeConst.MAX_COURSE_IN_PAGE).Select(e => e.Course).ToListAsync();
                int count = await query.CountAsync();
                int number = (int)Math.Ceiling((double)count / PageSizeConst.MAX_COURSE_IN_PAGE);
                PagedResult<Course> result = new PagedResult<Course>()
                {
                    PageSelected = page,
                    Results = list,
                    NumberPage = number,
                    PRE_URL = "/MyCourse?page=" + (page - 1),
                    NEXT_URL = "/MyCourse?page=" + (page + 1),
                };
                return new ResponseBase<PagedResult<Course>?>(result, string.Empty);
            }
            catch (Exception ex)
            {
                return new ResponseBase<PagedResult<Course>?>(null, ex.Message + " " + ex, (int)HttpStatusCode.InternalServerError);

            }
        }
    }
}
