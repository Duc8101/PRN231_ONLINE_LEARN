using AutoMapper;
using Common.Base;
using Common.DTO.CourseDTO;
using Common.Entity;
using Common.Paginations;
using DataAccess.Model.DAO;
using System.Net;
using WEB_CLIENT.Services.Base;

namespace WEB_CLIENT.Services.ManagerCourse
{
    public class ManagerCourseService : BaseService, IManagerCourseService
    {
        private readonly DAOCourse _daoCourse;
        private readonly DAOCategory _daoCategory;

        public ManagerCourseService(IMapper mapper, DAOCourse daoCourse, DAOCategory daoCategory) : base(mapper)
        {
            _daoCourse = daoCourse;
            _daoCategory = daoCategory;
        }

        public ResponseBase<Pagination<Course>?> Index(int? page, Guid teacherId)
        {
            int pageSelected = page == null ? 1 : page.Value;
            int prePage = pageSelected - 1;
            int nextPage = pageSelected + 1;
            string preURL = "/ManagerCourse?page=" + prePage;
            string nextURL = "/ManagerCourse?page=" + nextPage;
            try
            {
                List<Course> list = _daoCourse.getListCourse(null, null, null, teacherId, pageSelected);
                int numberPage = _daoCourse.getNumberPage(null, null, null, teacherId);
                Pagination<Course> result = new Pagination<Course>()
                {
                    PageSelected = pageSelected,
                    List = list,
                    NumberPage = numberPage,
                    PRE_URL = preURL,
                    NEXT_URL = nextURL,
                };
                return new ResponseBase<Pagination<Course>?>(result);
            }
            catch (Exception ex)
            {
                return new ResponseBase<Pagination<Course>?>(ex.Message + " " + ex, (int)HttpStatusCode.InternalServerError);
            }
        }

        public ResponseBase<List<Category>?> Create()
        {
            try
            {
                List<Category> list = _daoCategory.getListCategory();
                return new ResponseBase<List<Category>?>(list);
            }
            catch (Exception ex)
            {
                return new ResponseBase<List<Category>?>(ex.Message + " " + ex, (int)HttpStatusCode.InternalServerError);
            }
        }

        public ResponseBase<List<Category>?> Create(CourseCreateUpdateDTO DTO, Guid creatorId)
        {
            try
            {
                List<Category> list = _daoCategory.getListCategory();
                if (_daoCourse.isExist(DTO.CourseName, DTO.CategoryId))
                {
                    return new ResponseBase<List<Category>?>(list, "Course existed", (int)HttpStatusCode.Conflict);
                }
                Course course = _mapper.Map<Course>(DTO);
                course.CourseId = Guid.NewGuid();
                course.CreatorId = creatorId;
                course.CreatedAt = DateTime.Now;
                course.UpdateAt = DateTime.Now;
                course.IsDeleted = false;
                _daoCourse.CreateCourse(course);
                return new ResponseBase<List<Category>?>(list, "Create successful");
            }
            catch (Exception ex)
            {
                return new ResponseBase<List<Category>?>(ex.Message + " " + ex, (int)HttpStatusCode.InternalServerError);
            }
        }

        public ResponseBase<Dictionary<string, object>?> Update(Guid courseId, Guid creatorId)
        {
            try
            {
                Course? course = _daoCourse.getCourse(courseId, creatorId);
                if (course == null)
                {
                    return new ResponseBase<Dictionary<string, object>?>(string.Empty, (int)HttpStatusCode.NotFound);
                }
                List<Category> list = _daoCategory.getListCategory();
                Dictionary<string, object> data = new Dictionary<string, object>();
                data["course"] = course;
                data["list"] = list;
                return new ResponseBase<Dictionary<string, object>?>(data);
            }
            catch (Exception ex)
            {
                return new ResponseBase<Dictionary<string, object>?>(ex.Message + " " + ex, (int)HttpStatusCode.InternalServerError);
            }
        }

        public ResponseBase<Dictionary<string, object>?> Update(Guid courseId, CourseCreateUpdateDTO DTO)
        {
            try
            {
                Course? course = _daoCourse.getCourse(courseId);
                if (course == null)
                {
                    return new ResponseBase<Dictionary<string, object>?>(null, "Not found course", (int)HttpStatusCode.NotFound);
                }
                course.CourseName = DTO.CourseName.Trim();
                course.CategoryId = DTO.CategoryId;
                course.Image = DTO.Image.Trim();
                course.Description = DTO.Description == null || DTO.Description.Trim().Length == 0 ? null : DTO.Description.Trim();
                List<Category> list = _daoCategory.getListCategory();
                Dictionary<string, object> data = new Dictionary<string, object>();
                data["course"] = course;
                data["list"] = list;
                if (_daoCourse.isExist(DTO.CourseName, DTO.CategoryId, courseId))
                {
                    return new ResponseBase<Dictionary<string, object>?>(data, "Course existed", (int)HttpStatusCode.Conflict);
                }
                course.UpdateAt = DateTime.Now;
                _daoCourse.UpdateCourse(course);
                data["course"] = course;
                return new ResponseBase<Dictionary<string, object>?>(data, "Update successful");
            }
            catch (Exception ex)
            {
                return new ResponseBase<Dictionary<string, object>?>(ex.Message + " " + ex, (int)HttpStatusCode.InternalServerError);
            }
        }

        public ResponseBase<bool> Delete(Guid courseId, Guid creatorId)
        {
            try
            {
                Course? course = _daoCourse.getCourse(courseId, creatorId);
                if (course == null)
                {
                    return new ResponseBase<bool>(false, string.Empty, (int)HttpStatusCode.NotFound);
                }
                _daoCourse.DeleteCourse(course);
                return new ResponseBase<bool>(true);
            }
            catch (Exception ex)
            {
                return new ResponseBase<bool>(false, ex.Message + " " + ex, (int)HttpStatusCode.InternalServerError);
            }
        }
    }
}
