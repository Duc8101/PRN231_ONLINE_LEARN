namespace Common.DTO.LessonPdfDTO
{
    public class LessonPdfCreateUpdateDTO
    {
        public string Pdfname { get; set; } = string.Empty;

        public string FilePdf { get; set; } = string.Empty;

        public Guid LessonId { get; set; }
    }
}
