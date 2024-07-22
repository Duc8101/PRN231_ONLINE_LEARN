using Common.Enums;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Common.Entity
{
    [Table("Lesson")]
    public class Lesson : CommonEntity
    {
        public Lesson()
        {
            LessonPdfs = new HashSet<LessonPdf>();
            LessonVideos = new HashSet<LessonVideo>();
            Quizzes = new HashSet<Quiz>();
            Results = new HashSet<Result>();
        }
        [Key]
        [Column("LessonID")]
        public Guid LessonId { get; set; }
        [MaxLength((int)Lessons.Lesson_Name)]
        public string LessonName { get; set; } = null!;

        [Column("CourseID")]
        public Guid CourseId { get; set; }
        public int LessonNo { get; set; }
        public virtual Course Course { get; set; } = null!;
        public virtual ICollection<LessonPdf> LessonPdfs { get; set; }
        public virtual ICollection<LessonVideo> LessonVideos { get; set; }
        public virtual ICollection<Quiz> Quizzes { get; set; }
        public virtual ICollection<Result> Results { get; set; }
    }
}
