using Common.DTO.LessonDTO;
using Common.Entity;
using DataAccess.Model.DBContext;

namespace DataAccess.Model.DAO
{
    public class DAOLesson : BaseDAO
    {
        public DAOLesson(MyDbContext context) : base(context)
        {
        }

        public List<Lesson> getListLesson(Guid courseId)
        {
            return _context.Lessons.Where(l => l.IsDeleted == false && l.CourseId == courseId).OrderBy(l => l.LessonNo)
                .ToList();
        }

        public List<Lesson> getListLesson()
        {
            return _context.Lessons.Where(l => l.IsDeleted == false && l.Quizzes.Any(q => q.IsDeleted == false))
                .ToList();
        }

        public Lesson? getLesson(Guid lessonId)
        {
            return _context.Lessons.SingleOrDefault(l => l.IsDeleted == false && l.LessonId == lessonId && l.Course.IsDeleted == false);
        }

        public bool isExist(LessonCreateUpdateDTO DTO)
        {
            return _context.Lessons.Any(l => l.LessonName == DTO.LessonName.Trim() && l.CourseId == DTO.CourseId 
            && l.IsDeleted == false);
        }

        public void CreateLesson(Lesson lesson)
        {
            _context.Lessons.Add(lesson);
            _context.SaveChanges();
        }

        public Lesson? getLesson(Guid lessonId, Guid courseId)
        {
            return _context.Lessons.SingleOrDefault(l => l.IsDeleted == false && l.LessonId == lessonId && l.Course.IsDeleted == false
            && l.CourseId == courseId);
        }

        public bool isExist(Guid lessonId, LessonCreateUpdateDTO DTO)
        {
            return _context.Lessons.Any(l => l.LessonName == DTO.LessonName.Trim() && l.CourseId == DTO.CourseId 
            && l.IsDeleted == false && l.LessonId != lessonId);
        }

        public void UpdateLesson(Lesson lesson)
        {
            _context.Lessons.Update(lesson);
            _context.SaveChanges();
        }

    }
}
