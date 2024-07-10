using Common.Base;
using Common.Const;
using Common.Entity;
using Common.Pagination;
using DataAccess.Model.IDAO;
using Microsoft.EntityFrameworkCore;
using System.Net;

namespace WEB_CLIENT.Services.MyCourse
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
                List<Course> list = await query.OrderByDescending(e => e.Course.UpdateAt).Skip((int)PageSizeConst.Course_Page * (page - 1))
                    .Take((int)PageSizeConst.Course_Page).Select(e => e.Course).ToListAsync();
                int count = await query.CountAsync();
                int number = (int)Math.Ceiling((double)count / (int)PageSizeConst.Course_Page);
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
