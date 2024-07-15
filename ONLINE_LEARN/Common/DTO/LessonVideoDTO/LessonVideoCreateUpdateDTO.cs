namespace Common.DTO.LessonVideoDTO
{
    public class LessonVideoCreateUpdateDTO
    {
        public string VideoName { get; set; } = string.Empty;
        public string FileVideo { get; set; } = string.Empty;
        public Guid LessonId { get; set; }
    }
}
