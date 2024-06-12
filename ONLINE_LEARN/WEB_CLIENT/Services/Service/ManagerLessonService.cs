using Common.Base;
using Common.Const;
using Common.Entity;
using DataAccess.Model.IDAO;
using Microsoft.EntityFrameworkCore;
using System.Net;
using WEB_CLIENT.Services.IService;

namespace WEB_CLIENT.Services.Service
{
    public class ManagerLessonService : IManagerLessonService
    {
        private readonly ICommonDAO<Course> _daoCourse;
        private readonly ICommonDAO<Lesson> _daoLesson;
        private readonly ICommonDAO<LessonPdf> _daoPDF;
        private readonly ICommonDAO<LessonVideo> _daoVideo;
        public ManagerLessonService(ICommonDAO<Course> daoCourse, ICommonDAO<Lesson> daoLesson, ICommonDAO<LessonPdf> daoPDF
            , ICommonDAO<LessonVideo> daoVideo)
        {
            _daoCourse = daoCourse;
            _daoLesson = daoLesson;
            _daoPDF = daoPDF;
            _daoVideo = daoVideo;
        }

        public async Task<ResponseBase<Dictionary<string, object>?>> Index(Guid CourseID, Guid CreatorID, string? video /* file video */, string? name /*video name or pdf name*/, string? PDF /*file PDF */, Guid? LessonID)
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
                // if start to learn course
                if (video == null && PDF == null)
                {
                    LessonVideo? lessonVideo = await _daoVideo.Get(v => v.IsDeleted == false && v.Lesson.IsDeleted == false && v.Lesson.LessonNo == 1
                        && v.Lesson.CourseId == CourseID);
                    if (lessonVideo != null)
                    {
                        video = lessonVideo.FileVideo;
                        name = lessonVideo.VideoName;
                        LessonID = lessonVideo.LessonId;
                    }
                }
                Dictionary<string, object> result = new Dictionary<string, object>();
                result["listLesson"] = listLesson;
                result["listPDF"] = listPDF;
                result["listVideo"] = listVideo;
                result["video"] = video == null ? "" : video;
                result["PDF"] = PDF == null ? "" : PDF;
                result["name"] = name == null ? "" : name;
                result["lID"] = LessonID == null ? Guid.NewGuid() : LessonID;
                result["CourseID"] = CourseID;
                return new ResponseBase<Dictionary<string, object>?>(result, string.Empty);
            }
            catch (Exception ex)
            {
                return new ResponseBase<Dictionary<string, object>?>(null, ex.Message + " " + ex, (int)HttpStatusCode.InternalServerError);
            }
        }
        public async Task<ResponseBase<Dictionary<string, object>?>> Create(string? LessonName, Guid CourseID, Guid CreatorID)
        {
            try
            {
                ResponseBase<Dictionary<string, object>?> response = await Index(CourseID, CreatorID, null, null, null, null);
                if (response.Data == null)
                {
                    return new ResponseBase<Dictionary<string, object>?>(null, response.Message, response.Code);
                }
                if (LessonName == null)
                {
                    return new ResponseBase<Dictionary<string, object>?>(response.Data, "You have to input lesson name", (int)HttpStatusCode.Conflict);
                }
                if (LessonName.Trim().Length > LessonConst.MAX_LENGTH_LESSON_NAME)
                {
                    return new ResponseBase<Dictionary<string, object>?>(response.Data, "Lesson name max " + LessonConst.MAX_LENGTH_LESSON_NAME + " characters", (int)HttpStatusCode.Conflict);
                }
                if (await _daoLesson.Any(l => l.LessonName == LessonName.Trim() && l.CourseId == CourseID && l.IsDeleted == false))
                {
                    return new ResponseBase<Dictionary<string, object>?>(response.Data, "Lesson existed", (int)HttpStatusCode.Conflict);
                }
                List<Lesson> list = await _daoLesson.FindAll(l => l.IsDeleted == false && l.CourseId == CourseID)
                    .OrderBy(l => l.LessonNo).ToListAsync();
                Lesson lesson = new Lesson()
                {
                    LessonId = Guid.NewGuid(),
                    LessonName = LessonName.Trim(),
                    CourseId = CourseID,
                    LessonNo = list.Count + 1,
                    CreatedAt = DateTime.Now,
                    UpdateAt = DateTime.Now,
                    IsDeleted = false,
                };
                await _daoLesson.Create(lesson);
                await _daoLesson.Save();
                list = await _daoLesson.FindAll(l => l.IsDeleted == false && l.CourseId == CourseID)
                    .OrderBy(l => l.LessonNo).ToListAsync();
                response.Data["listLesson"] = list;
                return new ResponseBase<Dictionary<string, object>?>(response.Data, "Create successful");
            }
            catch (Exception ex)
            {
                return new ResponseBase<Dictionary<string, object>?>(null, ex.Message + " " + ex, (int)HttpStatusCode.InternalServerError);
            }
        }
        public async Task<ResponseBase<Dictionary<string, object>?>> Update(Guid LessonID, string? LessonName, Guid CourseID, Guid CreatorID)
        {
            try
            {
                ResponseBase<Dictionary<string, object>?> response = await Index(CourseID, CreatorID, null, null, null, null);
                if (response.Data == null)
                {
                    return new ResponseBase<Dictionary<string, object>?>(null, response.Message, response.Code);
                }
                if (LessonName == null)
                {
                    return new ResponseBase<Dictionary<string, object>?>(response.Data, "You have to input lesson name", (int)HttpStatusCode.Conflict);
                }
                if (LessonName.Trim().Length > LessonConst.MAX_LENGTH_LESSON_NAME)
                {
                    return new ResponseBase<Dictionary<string, object>?>(response.Data, "Lesson name max " + LessonConst.MAX_LENGTH_LESSON_NAME + " characters", (int)HttpStatusCode.Conflict);
                }
                Lesson? lesson = await _daoLesson.Get(l => l.IsDeleted == false && l.LessonId == LessonID
                && l.Course.IsDeleted == false && l.CourseId == CourseID);
                if (lesson == null)
                {
                    return new ResponseBase<Dictionary<string, object>?>(null, "Not found lesson", (int)HttpStatusCode.NotFound);
                }
                if (await _daoLesson.Any(l => l.LessonName == LessonName.Trim() && l.CourseId == CourseID && l.IsDeleted == false && l.LessonId != LessonID))
                {
                    return new ResponseBase<Dictionary<string, object>?>(response.Data, "Lesson existed", (int)HttpStatusCode.Conflict);
                }
                lesson.LessonName = LessonName.Trim();
                lesson.UpdateAt = DateTime.Now;
                await _daoLesson.Update(lesson);
                await _daoLesson.Save();
                List<Lesson> list = await _daoLesson.FindAll(l => l.IsDeleted == false && l.CourseId == CourseID)
                    .OrderBy(l => l.LessonNo).ToListAsync();
                response.Data["listLesson"] = list;
                return new ResponseBase<Dictionary<string, object>?>(response.Data, "Update successful");
            }
            catch (Exception ex)
            {
                return new ResponseBase<Dictionary<string, object>?>(null, ex.Message + " " + ex, (int)HttpStatusCode.InternalServerError);
            }
        }
        public async Task<ResponseBase<Dictionary<string, object>?>> Delete(Guid LessonID, Guid CourseID, Guid CreatorID)
        {
            try
            {
                ResponseBase<Dictionary<string, object>?> response = await Index(CourseID, CreatorID, null, null, null, null);
                if (response.Data == null)
                {
                    return new ResponseBase<Dictionary<string, object>?>(null, response.Message, response.Code);
                }
                Lesson? lesson = await _daoLesson.Get(l => l.IsDeleted == false && l.LessonId == LessonID
                && l.Course.IsDeleted == false && l.CourseId == CourseID);
                if (lesson == null)
                {
                    return new ResponseBase<Dictionary<string, object>?>(null, "Not found lesson", (int)HttpStatusCode.NotFound);
                }
                List<Lesson> list = await _daoLesson.FindAll(l => l.IsDeleted == false && l.CourseId == CourseID)
                    .OrderBy(l => l.LessonNo).ToListAsync();
                foreach (Lesson less in list)
                {
                    if (less.LessonNo > lesson.LessonNo)
                    {
                        less.LessonNo--;
                        less.UpdateAt = DateTime.Now;
                        await _daoLesson.Update(less);
                        await _daoLesson.Save();
                    }
                }
                lesson.IsDeleted = true;
                await _daoLesson.Update(lesson);
                await _daoLesson.Save();
                list = await _daoLesson.FindAll(l => l.IsDeleted == false && l.CourseId == CourseID)
                    .OrderBy(l => l.LessonNo).ToListAsync();
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
