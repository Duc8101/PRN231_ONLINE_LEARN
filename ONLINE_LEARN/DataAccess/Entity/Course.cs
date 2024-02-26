using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DataAccess.Entity
{
    [Table("Course")]
    public partial class Course : CommonEntity
    {
        public Course()
        {
            EnrollCourses = new HashSet<EnrollCourse>();
            Lessons = new HashSet<Lesson>();
        }
        [Key]
        [Column("CourseID")]
        public Guid CourseId { get; set; }
        public string CourseName { get; set; } = null!;
        [Column("image")]
        public string Image { get; set; } = null!;
        [Column("CategoryID")]
        public int CategoryId { get; set; }

        [Column("CreatorID")]
        public Guid CreatorId { get; set; }
        [Column("description")]
        public string? Description { get; set; }
        public virtual Category Category { get; set; } = null!;
        public virtual User Creator { get; set; } = null!;
        public virtual ICollection<EnrollCourse> EnrollCourses { get; set; }
        public virtual ICollection<Lesson> Lessons { get; set; }
    }
}
