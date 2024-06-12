using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Common.Entity
{
    [Table("Category")]
    public partial class Category : CommonEntity
    {
        public Category()
        {
            Courses = new HashSet<Course>();
        }
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Column("ID")]
        public int Id { get; set; }
        [MaxLength(255)]
        public string Name { get; set; } = null!;
        public virtual ICollection<Course> Courses { get; set; }
    }
}
