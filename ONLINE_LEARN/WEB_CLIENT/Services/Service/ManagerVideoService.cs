using Common.Base;
using Common.Entity;
using DataAccess.Model.IDAO;
using Microsoft.EntityFrameworkCore;
using System.Net;
using WEB_CLIENT.Services.IService;

namespace WEB_CLIENT.Services.Service
{
    public class ManagerVideoService : IManagerVideoService
    {
        private readonly ICommonDAO<Course> _daoCourse;
        private readonly ICommonDAO<Lesson> _daoLesson;
        private readonly ICommonDAO<LessonPdf> _daoPDF;
        private readonly ICommonDAO<LessonVideo> _daoVideo;
        public ManagerVideoService(ICommonDAO<Course> daoCourse, ICommonDAO<Lesson> daoLesson, ICommonDAO<LessonPdf> daoPDF
            , ICommonDAO<LessonVideo> daoVideo)
        {
            _daoCourse = daoCourse;
            _daoLesson = daoLesson;
            _daoPDF = daoPDF;
            _daoVideo = daoVideo;
        }

        private async Task<ResponseBase<Dictionary<string, object>?>> getResult(Guid CourseID, Guid CreatorID)
        {
            try
            {
                Course? course = await _daoCourse.FindAll(c => c.CourseId == CourseID && c.IsDeleted == false && c.CreatorId == CreatorID)
                    .Include(c => c.Creator).FirstOrDefaultAsync();
                if (course == null)
                {
                    return new ResponseBase<Dictionary<string, object>?>(null, "Not found course", (int)HttpStatusCode.NotFound);
                }
                List<Lesson> listLesson = await _daoLesson.FindAll(l => l.IsDeleted == false && l.CourseId == CourseID)
                    .OrderBy(l => l.LessonNo).ToListAsync();
                List<LessonPdf> listPDF = await _daoPDF.FindAll(p => p.IsDeleted == false).ToListAsync(); ;
                List<LessonVideo> listVideo = await _daoVideo.FindAll(v => v.IsDeleted == false).ToListAsync();
                LessonVideo? lessonVideo = await _daoVideo.Get(v => v.IsDeleted == false && v.Lesson.IsDeleted == false && v.Lesson.LessonNo == 1
                       && v.Lesson.CourseId == CourseID);
                string video = string.Empty;
                string name = string.Empty;
                Guid LessonID = Guid.NewGuid();
                if (lessonVideo != null)
                {
                    video = lessonVideo.FileVideo;
                    name = lessonVideo.VideoName;
                    LessonID = lessonVideo.LessonId;
                }
                Dictionary<string, object> result = new Dictionary<string, object>();
                result["listLesson"] = listLesson;
                result["listPDF"] = listPDF;
                result["listVideo"] = listVideo;
                result["video"] = video;
                result["PDF"] = string.Empty;
                result["name"] = name;
                result["lID"] = LessonID;
                result["CourseID"] = CourseID;
                return new ResponseBase<Dictionary<string, object>?>(result, string.Empty);
            }
            catch (Exception ex)
            {
                return new ResponseBase<Dictionary<string, object>?>(null, ex.Message + " " + ex, (int)HttpStatusCode.InternalServerError);
            }
        }
        public async Task<ResponseBase<Dictionary<string, object>?>> Create(LessonVideo create, Guid CourseID, Guid CreatorID)
        {
            try
            {
                ResponseBase<Dictionary<string, object>?> result = await getResult(CourseID, CreatorID);
                if (result.Data == null)
                {
                    return new ResponseBase<Dictionary<string, object>?>(null, result.Message, result.Code);
                }
                Lesson? lesson = await _daoLesson.Get(l => l.IsDeleted == false && l.LessonId == create.LessonId
                && l.Course.IsDeleted == false && l.CourseId == CourseID);
                if (lesson == null)
                {
                    return new ResponseBase<Dictionary<string, object>?>(null, "Not found lesson", (int)HttpStatusCode.NotFound);
                }
                if (create.VideoName == null || create.VideoName.Trim().Length == 0)
                {
                    return new ResponseBase<Dictionary<string, object>?>(result.Data, "You have to input video name", (int)HttpStatusCode.Conflict);
                }
                create.VideoName = create.VideoName.Trim();
                create.CreatedAt = DateTime.Now;
                create.UpdateAt = DateTime.Now;
                create.IsDeleted = false;
                await _daoVideo.Create(create);
                await _daoVideo.Save();
                List<LessonVideo> listVideo = await _daoVideo.FindAll(v => v.IsDeleted == false).ToListAsync();
                result.Data["listVideo"] = listVideo;
                return new ResponseBase<Dictionary<string, object>?>(result.Data, "Video " + create.VideoName + " was added successfully!");
            }
            catch (Exception ex)
            {
                return new ResponseBase<Dictionary<string, object>?>(null, ex.Message + " " + ex, (int)HttpStatusCode.InternalServerError);
            }
        }
        public async Task<ResponseBase<Dictionary<string, object>?>> Update(int VideoID, LessonVideo obj, Guid CourseID, Guid CreatorID)
        {
            try
            {
                ResponseBase<Dictionary<string, object>?> result = await getResult(CourseID, CreatorID);
                if (result.Data == null)
                {
                    return new ResponseBase<Dictionary<string, object>?>(null, result.Message, result.Code);
                }
                Lesson? lesson = await _daoLesson.Get(l => l.IsDeleted == false && l.LessonId == obj.LessonId
                && l.Course.IsDeleted == false && l.CourseId == CourseID);
                if (lesson == null)
                {
                    return new ResponseBase<Dictionary<string, object>?>(null, "Not found lesson", (int)HttpStatusCode.NotFound);
                }
                LessonVideo? video = await _daoVideo.Get(v => v.VideoId == VideoID && v.IsDeleted == false && v.LessonId == obj.LessonId);
                if (video == null)
                {
                    return new ResponseBase<Dictionary<string, object>?>(null, "Not found video", (int)HttpStatusCode.NotFound);
                }
                if (obj.VideoName == null || obj.VideoName.Trim().Length == 0)
                {
                    return new ResponseBase<Dictionary<string, object>?>(result.Data, "You have to input video name", (int)HttpStatusCode.Conflict);
                }
                video.VideoName = obj.VideoName.Trim();
                if (obj.FileVideo != null && obj.FileVideo.Length > 0)
                {
                    video.FileVideo = obj.FileVideo;
                }
                video.UpdateAt = DateTime.Now;
                await _daoVideo.Update(video);
                await _daoVideo.Save();
                List<LessonVideo> listVideo = await _daoVideo.FindAll(v => v.IsDeleted == false).ToListAsync();
                result.Data["listVideo"] = listVideo;
                return new ResponseBase<Dictionary<string, object>?>(result.Data, "Update successful");
            }
            catch (Exception ex)
            {
                return new ResponseBase<Dictionary<string, object>?>(null, ex.Message + " " + ex, (int)HttpStatusCode.InternalServerError);
            }
        }
        public async Task<ResponseBase<Dictionary<string, object>?>> Delete(int VideoID, Guid LessonID, Guid CourseID, Guid CreatorID)
        {
            try
            {
                ResponseBase<Dictionary<string, object>?> result = await getResult(CourseID, CreatorID);
                if (result.Data == null)
                {
                    return new ResponseBase<Dictionary<string, object>?>(null, result.Message, result.Code);
                }
                Lesson? lesson = await _daoLesson.Get(l => l.IsDeleted == false && l.LessonId == LessonID
                && l.Course.IsDeleted == false && l.CourseId == CourseID);
                if (lesson == null)
                {
                    return new ResponseBase<Dictionary<string, object>?>(null, "Not found lesson", (int)HttpStatusCode.NotFound);
                }
                LessonVideo? video = await _daoVideo.Get(v => v.VideoId == VideoID && v.IsDeleted == false && v.LessonId == LessonID);
                if (video == null)
                {
                    return new ResponseBase<Dictionary<string, object>?>(null, "Not found video", (int)HttpStatusCode.NotFound);
                }
                video.IsDeleted = true;
                await _daoVideo.Update(video);
                await _daoVideo.Save();
                List<LessonVideo> listVideo = await _daoVideo.FindAll(v => v.IsDeleted == false).ToListAsync();
                result.Data["listVideo"] = listVideo;
                return new ResponseBase<Dictionary<string, object>?>(result.Data, "Delete successful");
            }
            catch (Exception ex)
            {
                return new ResponseBase<Dictionary<string, object>?>(null, ex.Message + " " + ex, (int)HttpStatusCode.InternalServerError);
            }
        }
    }
}
