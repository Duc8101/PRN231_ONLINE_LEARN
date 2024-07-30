using AutoMapper;
using Common.Base;
using Common.DTO.LessonVideoDTO;
using Common.Entity;
using DataAccess.Model.DAO;
using System.Net;
using WEB_CLIENT.Services.Base;

namespace WEB_CLIENT.Services.ManagerVideo
{
    public class ManagerVideoService : BaseService, IManagerVideoService
    {
        private readonly DAOCourse _daoCourse;
        private readonly DAOLesson _daoLesson;
        private readonly DAOLessonPDF _daoPDF;
        private readonly DAOLessonVideo _daoVideo;
        public ManagerVideoService(IMapper mapper, DAOCourse daoCourse, DAOLesson daoLesson, DAOLessonPDF daoPDF
            , DAOLessonVideo daoVideo) : base(mapper)
        {
            _daoCourse = daoCourse;
            _daoLesson = daoLesson;
            _daoPDF = daoPDF;
            _daoVideo = daoVideo;
        }

        private ResponseBase<Dictionary<string, object>?> getResponse(Guid courseId, Guid creatorId)
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
                LessonVideo? lessonVideo = _daoVideo.getVideo(courseId);
                string video = string.Empty;
                string name = string.Empty;
                Guid lessonId = Guid.NewGuid();
                if (lessonVideo != null)
                {
                    video = lessonVideo.FileVideo;
                    name = lessonVideo.VideoName;
                    lessonId = lessonVideo.LessonId;
                }
                Dictionary<string, object> data = new Dictionary<string, object>()
                {
                    {"listLesson", listLesson},
                    {"listPDF", listPDF},
                    {"listVideo", listVideo },
                    {"video",  video},
                    {"PDF", string.Empty},
                    {"name",  name},
                    {"lId",  lessonId},
                    {"courseId",  courseId},
                };
                return new ResponseBase<Dictionary<string, object>?>(data);
            }
            catch (Exception ex)
            {
                return new ResponseBase<Dictionary<string, object>?>(ex.Message + " " + ex, (int)HttpStatusCode.InternalServerError);
            }
        }

        public ResponseBase<Dictionary<string, object>?> Create(LessonVideoCreateUpdateDTO DTO, Guid courseId, Guid creatorId)
        {
            try
            {
                ResponseBase<Dictionary<string, object>?> response = getResponse(courseId, creatorId);
                if (response.Data == null)
                {
                    return new ResponseBase<Dictionary<string, object>?>(response.Message, response.Code);
                }
                Lesson? lesson = _daoLesson.getLesson(DTO.LessonId, courseId);
                if (lesson == null)
                {
                    return new ResponseBase<Dictionary<string, object>?>("Not found lesson", (int)HttpStatusCode.NotFound);
                }
                if (DTO.VideoName == null || DTO.VideoName.Trim().Length == 0)
                {
                    return new ResponseBase<Dictionary<string, object>?>(response.Data, "You have to input video name", (int)HttpStatusCode.Conflict);
                }
                LessonVideo video = _mapper.Map<LessonVideo>(DTO);
                video.CreatedAt = DateTime.Now;
                video.UpdateAt = DateTime.Now;
                video.IsDeleted = false;
                _daoVideo.CreateVideo(video);
                List<LessonVideo> listVideo = _daoVideo.getListVideo();
                response.Data["listVideo"] = listVideo;
                return new ResponseBase<Dictionary<string, object>?>(response.Data, "Video " + DTO.VideoName + " was added successfully!");
            }
            catch (Exception ex)
            {
                return new ResponseBase<Dictionary<string, object>?>(ex.Message + " " + ex, (int)HttpStatusCode.InternalServerError);
            }
        }

        public ResponseBase<Dictionary<string, object>?> Update(int videoId, LessonVideoCreateUpdateDTO DTO
            , Guid courseId, Guid creatorId)
        {
            try
            {
                ResponseBase<Dictionary<string, object>?> response = getResponse(courseId, creatorId);
                if (response.Data == null)
                {
                    return new ResponseBase<Dictionary<string, object>?>(response.Message, response.Code);
                }
                Lesson? lesson = _daoLesson.getLesson(DTO.LessonId, courseId);
                if (lesson == null)
                {
                    return new ResponseBase<Dictionary<string, object>?>("Not found lesson", (int)HttpStatusCode.NotFound);
                }
                LessonVideo? video = _daoVideo.getVideo(videoId, DTO.LessonId);
                if (video == null)
                {
                    return new ResponseBase<Dictionary<string, object>?>("Not found video", (int)HttpStatusCode.NotFound);
                }
                if (DTO.VideoName == null || DTO.VideoName.Trim().Length == 0)
                {
                    return new ResponseBase<Dictionary<string, object>?>(response.Data, "You have to input video name", (int)HttpStatusCode.Conflict);
                }
                video.VideoName = DTO.VideoName.Trim();
                if (DTO.FileVideo != null && DTO.FileVideo.Length > 0)
                {
                    video.FileVideo = DTO.FileVideo;
                }
                video.UpdateAt = DateTime.Now;
                _daoVideo.UpdateVideo(video);
                List<LessonVideo> listVideo = _daoVideo.getListVideo();
                response.Data["listVideo"] = listVideo;
                return new ResponseBase<Dictionary<string, object>?>(response.Data, "Update successful");
            }
            catch (Exception ex)
            {
                return new ResponseBase<Dictionary<string, object>?>(ex.Message + " " + ex, (int)HttpStatusCode.InternalServerError);
            }
        }

        public ResponseBase<Dictionary<string, object>?> Delete(int videoId, Guid lessonId, Guid courseId, Guid creatorId)
        {
            try
            {
                ResponseBase<Dictionary<string, object>?> response = getResponse(courseId, creatorId);
                if (response.Data == null)
                {
                    return new ResponseBase<Dictionary<string, object>?>(response.Message, response.Code);
                }
                Lesson? lesson = _daoLesson.getLesson(lessonId, courseId);
                if (lesson == null)
                {
                    return new ResponseBase<Dictionary<string, object>?>("Not found lesson", (int)HttpStatusCode.NotFound);
                }
                LessonVideo? video = _daoVideo.getVideo(videoId, lessonId);
                if (video == null)
                {
                    return new ResponseBase<Dictionary<string, object>?>("Not found video", (int)HttpStatusCode.NotFound);
                }
                video.IsDeleted = true;
                _daoVideo.DeleteVideo(video);
                List<LessonVideo> listVideo = _daoVideo.getListVideo();
                response.Data["listVideo"] = listVideo;
                return new ResponseBase<Dictionary<string, object>?>(response.Data, "Delete successful");
            }
            catch (Exception ex)
            {
                return new ResponseBase<Dictionary<string, object>?>(ex.Message + " " + ex, (int)HttpStatusCode.InternalServerError);
            }
        }
    }
}
