using System.ComponentModel.DataAnnotations.Schema;

namespace DataAccess.Entity
{
    [Table("EnrollCourse")]
    public partial class EnrollCourse : CommonEntity
    {
        [ForeignKey("Course")]
        [Column("CourseID")]
        public Guid CourseId { get; set; }

        [ForeignKey("User")]
        [Column("StudentID")]
        public Guid StudentId { get; set; }
        public virtual Course Course { get; set; } = null!;
        public virtual User Student { get; set; } = null!;
    }
}
