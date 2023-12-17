using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DataAccess.Entity
{
    [Table("Start_Quiz")]
    public partial class Start_Quiz : CommonEntity
    {
        [Key]
        [Column("StartID")]
        public Guid StartId { get; set; }

        [ForeignKey("Quiz")]
        [Column("QuestionID")]
        public Guid QuestionId { get; set; }

        [ForeignKey("User")]
        [Column("StudentID")]
        public Guid StudentId { get; set; }
        public int ChosenAnwser { get; set; }
        [Column("isFinish")]
        public bool IsFinish { get; set; }
        public virtual Quiz Question { get; set; } = null!;
        public virtual User Student { get; set; } = null!;
    }
}
