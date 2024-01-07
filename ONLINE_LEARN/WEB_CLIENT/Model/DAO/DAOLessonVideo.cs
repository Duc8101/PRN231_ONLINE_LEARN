using DataAccess.Entity;
using Microsoft.EntityFrameworkCore;

namespace WEB_CLIENT.Model.DAO
{
    public class DAOLessonVideo : BaseDAO
    {
        public async Task<List<LessonVideo>> getList()
        {
            return await context.LessonVideos.Where(v => v.IsDeleted == false).ToListAsync();
        }

        public async Task<LessonVideo?> getFirstVideo(Guid CourseID)
        {
            return await context.LessonVideos.Where(v => v.IsDeleted == false && v.Lesson.IsDeleted == false && v.Lesson.LessonNo == 1
            && v.Lesson.CourseId == CourseID).FirstOrDefaultAsync();
        }

        public async Task CreateVideo(LessonVideo video)
        {
            await context.LessonVideos.AddAsync(video);
            await context.SaveChangesAsync();
        }

        public async Task<LessonVideo?> getVideo(int VideoID, Guid LessonID)
        {
            return await context.LessonVideos.SingleOrDefaultAsync(v => v.VideoId == VideoID && v.IsDeleted == false && v.LessonId == LessonID);
        }

        public async Task UpdateVideo(LessonVideo video)
        {
            context.LessonVideos.Update(video);
            await context.SaveChangesAsync();
        }

        public async Task DeleteVideo(LessonVideo video)
        {
            video.IsDeleted = true;
            await UpdateVideo(video);
        }
    }
}
