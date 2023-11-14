using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
