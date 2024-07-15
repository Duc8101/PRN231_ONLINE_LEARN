using Common.Entity;
using DataAccess.Model.DBContext;

namespace DataAccess.Model.DAO
{
    public class DAOLessonVideo : BaseDAO
    {
        public DAOLessonVideo(MyDbContext context) : base(context)
        {

        }

        public List<LessonVideo> getListVideo()
        {
            return _context.LessonVideos.Where(v => v.IsDeleted == false).ToList();
        }

        public LessonVideo? getVideo(Guid courseId)
        {
            return _context.LessonVideos.FirstOrDefault(v => v.IsDeleted == false && v.Lesson.IsDeleted == false && v.Lesson.LessonNo == 1
            && v.Lesson.CourseId == courseId);
        }

        public void CreateVideo(LessonVideo video)
        {
            _context.LessonVideos.Add(video);
            _context.SaveChanges();
        }

        public LessonVideo? getVideo(int videoId, Guid lessonId)
        {
            return _context.LessonVideos.SingleOrDefault(v => v.VideoId == videoId && v.IsDeleted == false && v.LessonId == lessonId);
        }

        public void UpdateVideo(LessonVideo video)
        {
            _context.LessonVideos.Update(video);
            _context.SaveChanges();
        }

        public void DeleteVideo(LessonVideo video)
        {
            video.IsDeleted = true;
            UpdateVideo(video);
        }

    }
}
