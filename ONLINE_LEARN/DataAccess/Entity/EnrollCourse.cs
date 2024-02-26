using System.ComponentModel.DataAnnotations.Schema;

namespace DataAccess.Entity
{
    [Table("EnrollCourse")]
    public partial class EnrollCourse : CommonEntity
    {
        [Column("CourseID")]
        public Guid CourseId { get; set; }

        [Column("StudentID")]
        public Guid StudentId { get; set; }
        public virtual Course Course { get; set; } = null!;
        public virtual User Student { get; set; } = null!;
    }
}
