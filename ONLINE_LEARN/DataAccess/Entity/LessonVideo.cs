using DataAccess.Const;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DataAccess.Entity
{
    [Table("LessonVideo")]
    public partial class LessonVideo : CommonEntity
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Column("VideoID")]
        public int VideoId { get; set; }
        [MaxLength(LessonVideoConst.MAX_LENGTH_LESSON_VIDEO_NAME)]
        public string VideoName { get; set; } = null!;
        public string FileVideo { get; set; } = null!;

        [Column("LessonID")]
        public Guid LessonId { get; set; }
        public virtual Lesson Lesson { get; set; } = null!;
    }
}
