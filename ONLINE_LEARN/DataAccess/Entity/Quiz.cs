using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DataAccess.Entity
{
    [Table("Quiz")]
    public partial class Quiz : CommonEntity
    {
        [Key]
        [Column("QuestionID")]
        public Guid QuestionId { get; set; }
        [Column("Question")]
        public string Question { get; set; } = null!;
        [ForeignKey("Lesson")]
        [Column("LessonID")]
        public Guid LessonId { get; set; }
        public string Answer1 { get; set; } = null!;
        public string Answer2 { get; set; } = null!;
        public string? Answer3 { get; set; }
        public string? Answer4 { get; set; }
        public int AnswerCorrect { get; set; }
        public virtual Lesson Lesson { get; set; } = null!;
    }
}
