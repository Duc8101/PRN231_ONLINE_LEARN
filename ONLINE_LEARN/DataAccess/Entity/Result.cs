using System.ComponentModel.DataAnnotations.Schema;

namespace DataAccess.Entity
{
    [Table("Result")]
    public partial class Result : CommonEntity
    {
        [ForeignKey("Lesson")]
        [Column("LessonID")]
        public Guid LessonId { get; set; }
        [ForeignKey("User")]
        [Column("StudentID")]
        public Guid StudentId { get; set; }
        [Column("score")]
        public decimal Score { get; set; }
        [Column("status")]
        public string Status { get; set; } = null!;
        public virtual Lesson Lesson { get; set; } = null!;
        public virtual User Student { get; set; } = null!;
    }
}
