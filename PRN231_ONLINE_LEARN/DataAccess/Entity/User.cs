using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Entity
{
    [Table("User")]
    public partial class User : CommonEntity
    {
        public User()
        {
            Courses = new HashSet<Course>();
            EnrollCourses = new HashSet<EnrollCourse>();
            Results = new HashSet<Result>();
        }
        [Key]
        [Column("ID")]
        public Guid Id { get; set; }
        public string FullName { get; set; } = null!;
        [Column("phone")]
        [MaxLength(10)]
        public string? Phone { get; set; }
        [Column("address")]
        [MaxLength(100)]
        public string? Address { get; set; }
        [Column("email")]
        [MaxLength(200)]
        public string Email { get; set; } = null!;
        [Column("gender")]
        [MaxLength(30)]
        public string Gender { get; set; } = null!;
        [Column("username")]
        [MaxLength(50)]
        public string Username { get; set; } = null!;
        [Column("password")]
        [MaxLength(50)]
        public string Password { get; set; } = null!;
        [MaxLength(50)]
        public string RoleName { get; set; } = null!;

        public virtual ICollection<Course> Courses { get; set; }
        public virtual ICollection<EnrollCourse> EnrollCourses { get; set; }
        public virtual ICollection<Result> Results { get; set; }
    }
}
