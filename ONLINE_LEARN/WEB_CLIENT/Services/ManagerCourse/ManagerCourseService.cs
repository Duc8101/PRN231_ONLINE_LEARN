using Common.Base;
using Common.Const;
using Common.Entity;
using Common.Pagination;
using DataAccess.Model.IDAO;
using Microsoft.EntityFrameworkCore;
using System.Net;

namespace WEB_CLIENT.Services.ManagerCourse
{
    public class ManagerCourseService : IManagerCourseService
    {
        private readonly ICommonDAO<Course> _daoCourse;
        private readonly ICommonDAO<Category> _daoCategory;
        public ManagerCourseService(ICommonDAO<Course> daoCourse, ICommonDAO<Category> daoCategory)
        {
            _daoCourse = daoCourse;
            _daoCategory = daoCategory;
        }

        public async Task<ResponseBase<PagedResult<Course>?>> Index(int? page, Guid TeacherID)
        {
            int pageSelected = page == null ? 1 : page.Value;
            int prePage = pageSelected - 1;
            int nextPage = pageSelected + 1;
            string preURL = "/ManagerCourse?page=" + prePage;
            string nextURL = "/ManagerCourse?page=" + nextPage;
            try
            {
                IQueryable<Course> query = _daoCourse.FindAll(c => c.IsDeleted == false, c => c.CreatorId == TeacherID);
                List<Course> list = await query.Skip(PageSizeConst.MAX_COURSE_IN_PAGE * (pageSelected - 1))
                    .Take(PageSizeConst.MAX_COURSE_IN_PAGE).ToListAsync();
                int count = await query.CountAsync();
                int numberPage = (int)Math.Ceiling((double)count / PageSizeConst.MAX_COURSE_IN_PAGE);
                PagedResult<Course> result = new PagedResult<Course>()
                {
                    PageSelected = pageSelected,
                    Results = list,
                    NumberPage = numberPage,
                    PRE_URL = preURL,
                    NEXT_URL = nextURL,
                };
                return new ResponseBase<PagedResult<Course>?>(result, string.Empty);
            }
            catch (Exception ex)
            {
                return new ResponseBase<PagedResult<Course>?>(null, ex.Message + " " + ex, (int)HttpStatusCode.InternalServerError);
            }
        }
        public async Task<ResponseBase<List<Category>?>> Create()
        {
            try
            {
                List<Category> list = await _daoCategory.FindAll().ToListAsync();
                return new ResponseBase<List<Category>?>(list, string.Empty);
            }
            catch (Exception ex)
            {
                return new ResponseBase<List<Category>?>(null, ex.Message + " " + ex, (int)HttpStatusCode.InternalServerError);
            }
        }
        public async Task<ResponseBase<List<Category>?>> Create(Course course, Guid CreatorID)
        {
            try
            {
                List<Category> list = await _daoCategory.FindAll().ToListAsync();
                if (await _daoCourse.Any(c => c.CourseName == course.CourseName.Trim() && c.CategoryId == course.CategoryId && c.IsDeleted == false))
                {
                    return new ResponseBase<List<Category>?>(list, "Course existed", (int)HttpStatusCode.Conflict);
                }
                course.CourseId = Guid.NewGuid();
                course.CreatorId = CreatorID;
                course.Image = course.Image.Trim();
                course.Description = course.Description == null || course.Description.Trim().Length == 0 ? null : course.Description.Trim();
                course.CreatedAt = DateTime.Now;
                course.UpdateAt = DateTime.Now;
                course.IsDeleted = false;
                await _daoCourse.Create(course);
                await _daoCourse.Save();
                return new ResponseBase<List<Category>?>(list, "Create successful");
            }
            catch (Exception ex)
            {
                return new ResponseBase<List<Category>?>(null, ex.Message + " " + ex, (int)HttpStatusCode.InternalServerError);
            }
        }
        public async Task<ResponseBase<Dictionary<string, object>?>> Update(Guid CourseID, Guid CreatorID)
        {
            try
            {
                Course? course = await _daoCourse.FindAll(c => c.CourseId == CourseID && c.IsDeleted == false && c.CreatorId == CreatorID)
                    .Include(c => c.Creator).FirstOrDefaultAsync();
                if (course == null)
                {
                    return new ResponseBase<Dictionary<string, object>?>(null, string.Empty, (int)HttpStatusCode.NotFound);
                }
                List<Category> list = await _daoCategory.FindAll().ToListAsync();
                Dictionary<string, object> dic = new Dictionary<string, object>();
                dic["course"] = course;
                dic["list"] = list;
                return new ResponseBase<Dictionary<string, object>?>(dic, string.Empty);
            }
            catch (Exception ex)
            {
                return new ResponseBase<Dictionary<string, object>?>(null, ex.Message + " " + ex, (int)HttpStatusCode.InternalServerError);
            }
        }
        public async Task<ResponseBase<Dictionary<string, object>?>> Update(Guid CourseID, Course course)
        {
            try
            {
                Course? update = await _daoCourse.FindAll(c => c.CourseId == CourseID && c.IsDeleted == false)
                    .Include(c => c.Creator).FirstOrDefaultAsync(); ;
                if (update == null)
                {
                    return new ResponseBase<Dictionary<string, object>?>(null, "Not found course", (int)HttpStatusCode.NotFound);
                }
                update.CourseName = course.CourseName.Trim();
                update.CategoryId = course.CategoryId;
                update.Image = course.Image.Trim();
                update.Description = course.Description == null || course.Description.Trim().Length == 0 ? null : course.Description.Trim();
                List<Category> list = await _daoCategory.FindAll().ToListAsync();
                Dictionary<string, object> dic = new Dictionary<string, object>();
                dic["course"] = update;
                dic["list"] = list;
                if (await _daoCourse.Any(c => c.CourseName == course.CourseName.Trim() && c.CategoryId == course.CategoryId && c.IsDeleted == false && c.CourseId != CourseID))
                {
                    return new ResponseBase<Dictionary<string, object>?>(dic, "Course existed", (int)HttpStatusCode.Conflict);
                }
                update.UpdateAt = DateTime.Now;
                await _daoCourse.Update(update);
                await _daoCourse.Save();
                dic["course"] = update;
                return new ResponseBase<Dictionary<string, object>?>(dic, "Update successful");
            }
            catch (Exception ex)
            {
                return new ResponseBase<Dictionary<string, object>?>(null, ex.Message + " " + ex, (int)HttpStatusCode.InternalServerError);
            }
        }
        public async Task<ResponseBase<PagedResult<Course>?>> Delete(Guid CourseID, Guid CreatorID)
        {
            try
            {
                Course? course = await _daoCourse.FindAll(c => c.CourseId == CourseID && c.IsDeleted == false && c.CreatorId == CreatorID)
                    .Include(c => c.Creator).FirstOrDefaultAsync();
                if (course == null)
                {
                    return new ResponseBase<PagedResult<Course>?>(null, string.Empty, (int)HttpStatusCode.NotFound);
                }
                ResponseBase<PagedResult<Course>?> result = await Index(null, CreatorID);
                if (result.Data == null)
                {
                    return new ResponseBase<PagedResult<Course>?>(null, "Get data failed", (int)HttpStatusCode.Conflict);
                }
                course.IsDeleted = true;
                await _daoCourse.Update(course);
                await _daoCourse.Save();
                return new ResponseBase<PagedResult<Course>?>(result.Data, "Course name : " + course.CourseName + " deleted successfully");
            }
            catch (Exception ex)
            {
                return new ResponseBase<PagedResult<Course>?>(null, ex.Message + " " + ex, (int)HttpStatusCode.InternalServerError);
            }
        }
    }
}
