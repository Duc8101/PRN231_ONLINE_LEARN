using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Entity
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
        [MaxLength(200)]
        public string LessonName { get; set; } = null!;
        [ForeignKey("Course")]
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
