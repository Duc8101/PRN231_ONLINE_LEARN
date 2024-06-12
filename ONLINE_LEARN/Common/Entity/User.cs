using Common.Consts;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Common.Entity
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
        [MaxLength(UserConst.PHONE_LENGTH)]
        public string? Phone { get; set; }
        [Column("image")]
        public string Image { get; set; } = null!;

        [Column("address")]
        [MaxLength(UserConst.MAX_ADDRESS_LENGTH)]
        public string? Address { get; set; }
        [Column("email")]
        [MaxLength(200)]
        public string Email { get; set; } = null!;
        [Column("gender")]
        [MaxLength(30)]
        public string Gender { get; set; } = null!;
        [Column("username")]
        [MaxLength(UserConst.MAX_LENGTH_USERNAME)]
        public string Username { get; set; } = null!;
        [Column("password")]
        public string Password { get; set; } = null!;
        [MaxLength(50)]
        public string RoleName { get; set; } = null!;

        public virtual ICollection<Course> Courses { get; set; }
        public virtual ICollection<EnrollCourse> EnrollCourses { get; set; }
        public virtual ICollection<Result> Results { get; set; }
    }
}
