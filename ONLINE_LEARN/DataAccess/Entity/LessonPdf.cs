using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DataAccess.Entity
{
    [Table("LessonPDF")]
    public partial class LessonPdf : CommonEntity
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Column("PDFID")]
        public int Pdfid { get; set; }
        [Column("PDFName")]
        public string Pdfname { get; set; } = null!;
        [Column("FilePDF")]
        public string FilePdf { get; set; } = null!;
        [ForeignKey("Lesson")]
        [Column("LessonID")]
        public Guid LessonId { get; set; }
        public virtual Lesson Lesson { get; set; } = null!;
    }
}
