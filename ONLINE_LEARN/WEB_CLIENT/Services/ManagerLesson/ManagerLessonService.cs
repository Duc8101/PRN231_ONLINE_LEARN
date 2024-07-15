using AutoMapper;
using Common.Base;
using Common.Const;
using Common.DTO.LessonDTO;
using Common.Entity;
using DataAccess.Model.DAO;
using System.Net;
using WEB_CLIENT.Services.Base;

namespace WEB_CLIENT.Services.ManagerLesson
{
    public class ManagerLessonService : BaseService, IManagerLessonService
    {
        private readonly DAOCourse _daoCourse;
        private readonly DAOLesson _daoLesson;
        private readonly DAOLessonPDF _daoPDF;
        private readonly DAOLessonVideo _daoVideo;

        public ManagerLessonService(IMapper mapper, DAOCourse daoCourse, DAOLesson daoLesson, DAOLessonPDF daoPDF
            , DAOLessonVideo daoVideo) : base(mapper)
        {
            _daoCourse = daoCourse;
            _daoLesson = daoLesson;
            _daoPDF = daoPDF;
            _daoVideo = daoVideo;
        }

        public ResponseBase<Dictionary<string, object>?> Index(Guid courseId, Guid creatorId, string? video /* file video */, string? name /*video name or pdf name*/, string? PDF /*file PDF */, Guid? lessonId)
        {
            try
            {
                Course? course = _daoCourse.getCourse(courseId, creatorId);
                if (course == null)
                {
                    return new ResponseBase<Dictionary<string, object>?>("Not found course", (int)HttpStatusCode.NotFound);
                }
                List<Lesson> listLesson = _daoLesson.getListLesson(courseId);
                List<LessonPdf> listPDF = _daoPDF.getListPDF();
                List<LessonVideo> listVideo = _daoVideo.getListVideo();
                // if start to learn course
                if (video == null && PDF == null)
                {
                    LessonVideo? lessonVideo = _daoVideo.getVideo(courseId);
                    if (lessonVideo != null)
                    {
                        video = lessonVideo.FileVideo;
                        name = lessonVideo.VideoName;
                        lessonId = lessonVideo.LessonId;
                    }
                }
                Dictionary<string, object> data = new Dictionary<string, object>();
                data["listLesson"] = listLesson;
                data["listPDF"] = listPDF;
                data["listVideo"] = listVideo;
                data["video"] = video == null ? "" : video;
                data["PDF"] = PDF == null ? "" : PDF;
                data["name"] = name == null ? "" : name;
                data["lId"] = lessonId == null ? Guid.NewGuid() : lessonId;
                data["courseId"] = courseId;
                return new ResponseBase<Dictionary<string, object>?>(data);
            }
            catch (Exception ex)
            {
                return new ResponseBase<Dictionary<string, object>?>(ex.Message + " " + ex, (int)HttpStatusCode.InternalServerError);
            }
        }

        public ResponseBase<Dictionary<string, object>?> Create(LessonCreateUpdateDTO DTO, Guid creatorId)
        {
            try
            {
                ResponseBase<Dictionary<string, object>?> response = Index(DTO.CourseId, creatorId, null, null, null, null);
                if (response.Data == null)
                {
                    return new ResponseBase<Dictionary<string, object>?>(null, response.Message, response.Code);
                }
                if (DTO.LessonName == null || DTO.LessonName.Trim().Length == 0)
                {
                    return new ResponseBase<Dictionary<string, object>?>(response.Data, "You have to input lesson name", (int)HttpStatusCode.Conflict);
                }
                if (DTO.LessonName.Trim().Length > (int)LessonConst.Lesson_Name)
                {
                    return new ResponseBase<Dictionary<string, object>?>(response.Data, LessonConst.Lesson_Name.ToString(), (int)HttpStatusCode.Conflict);
                }
                if (_daoLesson.isExist(DTO))
                {
                    return new ResponseBase<Dictionary<string, object>?>(response.Data, "Lesson existed", (int)HttpStatusCode.Conflict);
                }
                List<Lesson> list = _daoLesson.getListLesson(DTO.CourseId);
                Lesson lesson = _mapper.Map<Lesson>(DTO);
                lesson.LessonId = Guid.NewGuid();
                lesson.LessonNo = list.Count + 1;
                lesson.CreatedAt = DateTime.Now;
                lesson.UpdateAt = DateTime.Now;
                lesson.IsDeleted = false;
                _daoLesson.CreateLesson(lesson);
                list = _daoLesson.getListLesson(DTO.CourseId);
                response.Data["listLesson"] = list;
                return new ResponseBase<Dictionary<string, object>?>(response.Data, "Create successful");
            }
            catch (Exception ex)
            {
                return new ResponseBase<Dictionary<string, object>?>(ex.Message + " " + ex, (int)HttpStatusCode.InternalServerError);
            }
        }

        public ResponseBase<Dictionary<string, object>?> Update(Guid lessonId, LessonCreateUpdateDTO DTO, Guid creatorId)
        {
            try
            {
                ResponseBase<Dictionary<string, object>?> response = Index(DTO.CourseId, creatorId, null, null, null, null);
                if (response.Data == null)
                {
                    return new ResponseBase<Dictionary<string, object>?>(response.Message, response.Code);
                }
                if (DTO.LessonName == null || DTO.LessonName.Trim().Length == 0)
                {
                    return new ResponseBase<Dictionary<string, object>?>(response.Data, "You have to input lesson name", (int)HttpStatusCode.Conflict);
                }
                if (DTO.LessonName.Trim().Length > (int)LessonConst.Lesson_Name)
                {
                    return new ResponseBase<Dictionary<string, object>?>(response.Data, LessonConst.Lesson_Name.ToString(), (int)HttpStatusCode.Conflict);
                }
                Lesson? lesson = _daoLesson.getLesson(lessonId, DTO.CourseId);
                if (lesson == null)
                {
                    return new ResponseBase<Dictionary<string, object>?>("Not found lesson", (int)HttpStatusCode.NotFound);
                }
                if (_daoLesson.isExist(lessonId, DTO))
                {
                    return new ResponseBase<Dictionary<string, object>?>(response.Data, "Lesson existed", (int)HttpStatusCode.Conflict);
                }
                lesson.LessonName = DTO.LessonName.Trim();
                lesson.UpdateAt = DateTime.Now;
                _daoLesson.UpdateLesson(lesson);
                List<Lesson> list = _daoLesson.getListLesson(DTO.CourseId);
                response.Data["listLesson"] = list;
                return new ResponseBase<Dictionary<string, object>?>(response.Data, "Update successful");
            }
            catch (Exception ex)
            {
                return new ResponseBase<Dictionary<string, object>?>(ex.Message + " " + ex, (int)HttpStatusCode.InternalServerError);
            }
        }

        public ResponseBase<Dictionary<string, object>?> Delete(Guid lessonId, Guid courseId, Guid creatorId)
        {
            try
            {
                ResponseBase<Dictionary<string, object>?> response = Index(courseId, creatorId, null, null, null, null);
                if (response.Data == null)
                {
                    return new ResponseBase<Dictionary<string, object>?>(response.Message, response.Code);
                }
                Lesson? lesson = _daoLesson.getLesson(lessonId, courseId);
                if (lesson == null)
                {
                    return new ResponseBase<Dictionary<string, object>?>("Not found lesson", (int)HttpStatusCode.NotFound);
                }
                List<Lesson> list = _daoLesson.getListLesson(courseId);
                foreach (Lesson less in list)
                {
                    if (less.LessonNo > lesson.LessonNo)
                    {
                        less.LessonNo--;
                        less.UpdateAt = DateTime.Now;
                        _daoLesson.UpdateLesson(lesson);
                    }
                }
                lesson.IsDeleted = true;
                _daoLesson.UpdateLesson(lesson);
                list = _daoLesson.getListLesson(courseId);
                response.Data["listLesson"] = list;
                return new ResponseBase<Dictionary<string, object>?>(response.Data, "Delete successful");
            }
            catch (Exception ex)
            {
                return new ResponseBase<Dictionary<string, object>?>(null, ex.Message + " " + ex, (int)HttpStatusCode.InternalServerError);
            }
        }

    }
}
